using Gen.Saves;
using ManagedServer;
using ManagedServer.Entities.Types;
using ManagedServer.Events;
using ManagedServer.Features;
using ManagedServer.Features.Impl;
using ManagedServer.Inventories;
using ManagedServer.Worlds;
using Minecraft;
using Minecraft.Data.Blocks;
using Minecraft.Data.Generated;
using Minecraft.Implementations.Tags;
using Minecraft.Packets.Play.ServerBound;
using Minecraft.Schemas;
using Minecraft.Schemas.Items;
using Minecraft.Schemas.Vec;

namespace Gen;

public class GenGame {
    public static readonly Tag<string> GenItemTag = new("gen:item");
    private static readonly Tag<string> DimensionIdTag = new("gen:dimension_id");
    private static readonly Tag<(ulong StartTick, GenPortal Portal)> PortalTag = new("gen:portal_data");
    
    private readonly ManagedMinecraftServer _server;
    private readonly GenConfig _config;
    private readonly Dictionary<string, ItemStack> _items;
    private readonly IGenDataManager _dataManager;
    
    public Dictionary<string, World> Worlds { get; } = [];

    public GenGame(ManagedMinecraftServer server, GenConfig config) {
        _server = server;
        _config = config;
        _dataManager = config.DataManager ?? new NoOpDataManager();

        _items = [];
        foreach (ItemStack item in config.Items) {
            if (!item.HasTag(GenItemTag)) {
                throw new ArgumentException($"Config item {item.Type.Identifier} does not have the required tag GenGame.GenItemTag.");
            }
            _items[item.GetTag(GenItemTag)] = item;
        }
        
        // Validate config items
        foreach (GenDimension dimension in config.Dimensions) {
            foreach (GenMine mine in dimension.Mines) {
                ValidateItem(mine.Reward);
            }

            foreach (GenShop shop in dimension.Shops) {
                foreach (GenTrade trade in shop.Trades) {
                    ValidateItems(trade.Input1, trade.Input2, trade.Output);
                }
            }
        }
    }
    
    private void ValidateItems(params ItemStack?[] items) {
        foreach (ItemStack? item in items) {
            ValidateItem(item);
        }
    }

    private void ValidateItem(ItemStack? item) {
        if (item == null) {
            return;
        }
        
        if (!item.HasTag(GenItemTag)) {
            throw new ArgumentException($"Item {item.Type.Identifier} does not have the required tag GenGame.GenItemTag.");
        }
        
        string tagValue = item.GetTag(GenItemTag);
        if (!_items.ContainsKey(tagValue)) {
            throw new ArgumentException($"Item with tag value {tagValue} not found in config items.");
        }
    }
    
    public World Initialise() {
        foreach (GenDimension dimension in _config.Dimensions) {
            Dimension dim = new();
            _server.Dimensions.Add("gen:" + dimension.Id, dim);
            
            World world = _server.CreateWorld(dimension.Map, "gen:" + dimension.Id);
            world.SetTag(DimensionIdTag, dimension.Id);
            world.AddFeature(new TradingFeature());
            Worlds[dimension.Id] = world;
            
            foreach (GenShop shop in dimension.Shops) {
                Entity villager = new LivingEntity(EntityType.Villager) {
                    Position = shop.Position,
                    Yaw = shop.Facing
                };
                villager.SetWorld(world);

                world.Events.AddListener<PlayerEntityInteractEvent>(e => {
                    if (e.Type != ServerBoundInteractPacket.InteractType.Interact) {
                        return;
                    }

                    if (e.Target != villager) {
                        return;
                    }

                    TradeInventory inv = new(_server, shop.Trades.Select(t => 
                            new Trade(t.Input1, t.Output, t.Input2 == null ? null : new TradeItem(t.Input2), false,
                                0, 999, 0, 0, 1f, 0)).ToArray(), 
                        MerchantLevel.Expert, 0, true, true);

                    e.Player.OpenInventory = inv;
                });
            }

            foreach (GenMine mine in dimension.Mines) {
                foreach ((Vec3<int> From, Vec3<int> To) area in mine.Area) {
                    Fill(world, area.From, area.To, mine.Block);
                }
            }
            
            world.Events.AddListener<EntityMoveEvent>(e => {
                if (e.Entity is not PlayerEntity player) {
                    return;
                }

                if (e.NewPos.Y <= dimension.DeathHeight) {
                    Respawn(player);
                }
            });

            world.Events.AddListener<ServerTickEvent>(e => {
                
                foreach (PlayerEntity player in world.Players) {
                    // Check if they're in a portal
                    GenPortal? portal = GetPortal(world, player);
                    if (portal == null) {
                        if (player.HasTag(PortalTag)) {
                            player.RemoveTag(PortalTag);
                        }
                        return;
                    }
                    
                    // Okay they're in a portal
                    // were they in one before?
                    if (player.HasTag(PortalTag)) {
                        (ulong StartTick, GenPortal Portal) portalInfo = player.GetTag(PortalTag);
                        
                        // Is it the same one or different?
                        if (portalInfo.Portal.GetHashCode() == portal.GetHashCode()) {
                            // Same portal, if they've been in long enough then TP them
                            if (e.Server.CurrentTick - portalInfo.StartTick >= (ulong)_config.PortalEnterTicks) {
                                player.RemoveTag(PortalTag);
                                World targetWorld = Worlds[portal.TargetDimensionId];
                                Vec3<double> targetPos = portal.TargetPosition ?? _config.Dimensions.First(d => d.Id == portal.TargetDimensionId).Spawn;
                                
                                player.SetWorld(targetWorld);
                                player.Teleport(targetPos);
                            }
                            return;
                        }
                        
                        // Different portal, reset timer
                    }
                    
                    // Not in a portal before, set timer
                    player.SetTag(PortalTag, (e.Server.CurrentTick, portal));
                }
            });
            
            world.Events.AddListener<PlayerPlaceBlockEvent>(e => e.Cancelled = true);
            world.Events.AddListener<PlayerBreakBlockEvent>(ProcessBlockBreak);
            
            world.Events.AddListener<PlayerEnteringWorldEvent>(e => {
                LoadPlayer(e.Player);

                _server.Scheduler.ScheduleTask(1, () => {
                    Respawn(e.Player);
                });
            });

            world.Events.AddListener<PlayerLeavingWorldEvent>(e => {
                SavePlayer(e.Player);
            });
        }

        _server.Events.AddListener<PlayerDisconnectEvent>(e => {
            SavePlayer(e.Player);
            e.Player.World?.RemovePlayer(e.Player);  // This fixes bug in ManagedServer where disconnecting does not remove from world
        });
        
        return Worlds[_config.DefaultDimensionId];
    }
    
    private GenPortal? GetPortal(World world, PlayerEntity player) {
        GenDimension dimension = _config.Dimensions.First(d => d.Id == world.GetTag(DimensionIdTag));
        return dimension.Portals.FirstOrDefault(portal => portal.IsColliding(player));
    }

    private static void Fill(World world, Vec3<int> from, Vec3<int> to, IBlock block) {
        for (int x = Math.Min(from.X, to.X); x <= Math.Max(from.X, to.X); x++) {
            for (int y = Math.Min(from.Y, to.Y); y <= Math.Max(from.Y, to.Y); y++) {
                for (int z = Math.Min(from.Z, to.Z); z <= Math.Max(from.Z, to.Z); z++) {
                    Vec3<int> pos = new(x, y, z);
                    world.LoadChunk(World.GetChunkPos(pos)).Wait();
                    world.SetBlock(pos, block);
                }
            }
        }
    }
    
    private void ProcessBlockBreak(PlayerBreakBlockEvent e) {
        PlayerEntity player = e.Player;
        
        GenMine? mine = GetMine(e.World, e.Position);
        if (mine == null) {
            e.Cancelled = true;
            return;
        }
        
        // Cool
        ItemStack? leftover = player.Inventory.AddItem(mine.Reward);
        if (leftover != null) {
            e.World.DropItem(e.Position.BlockPosToDouble(), leftover);
        }

        _server.Scheduler.ScheduleTask((int)(_server.TargetTicksPerSecond * mine.RespawnTime.TotalSeconds), () => {
            e.World.SetBlock(e.Position, mine.Block);
        });
    }

    private GenMine? GetMine(World world, Vec3<int> pos) {
        return GetMine(world.GetTag(DimensionIdTag), pos);
    } 
    
    private GenMine? GetMine(string dimension, Vec3<int> pos) {
        foreach (GenMine mine in _config.Dimensions.First(d => d.Id == dimension).Mines) {
            if (mine.Area.Any(b => IsInRegion(pos, b.From, b.To))) {
                return mine;
            }
        }

        return null;
    }
    
    private static bool IsInRegion(Vec3<int> pos, Vec3<int> corner1, Vec3<int> corner2) {
        return pos.X >= Math.Min(corner1.X, corner2.X) && pos.X <= Math.Max(corner1.X, corner2.X)
               && pos.Y >= Math.Min(corner1.Y, corner2.Y) && pos.Y <= Math.Max(corner1.Y, corner2.Y)
               && pos.Z >= Math.Min(corner1.Z, corner2.Z) && pos.Z <= Math.Max(corner1.Z, corner2.Z);
    }

    public void Respawn(PlayerEntity player) {
        player.Teleport(_config.Dimensions.First(d => d.Id == player.World.ThrowIfNull().GetTag(DimensionIdTag)).Spawn);
    }

    public void LoadPlayer(PlayerEntity player) {
        PlayerSave? save = _dataManager.LoadPlayerData(player);
        if (save == null) {
            return;
        }

        for (int i = 0; i < player.Inventory.Size; i++) {
            string? itemId = save.Inventory[i];
            player.Inventory[i] = itemId == null ? ItemStack.Air : _items[itemId];
        }
        
        player.Health = save.Health;
        player.Yaw = Angle.FromDegrees(save.Yaw);
        player.Pitch = Angle.FromDegrees(save.Pitch);
    }
    
    public void SavePlayer(PlayerEntity player) {
        string?[] inventory = new string?[player.Inventory.Size];
        for (int i = 0; i < player.Inventory.Size; i++) {
            ItemStack item = player.Inventory[i];
            if (item.IsAir()) {
                inventory[i] = null;
            } else {
                inventory[i] = item.GetTag(GenItemTag);
            }
        }

        PlayerSave save = new(
            Inventory: inventory,
            Health: player.Health,
            Yaw: player.Yaw.DegreesF,
            Pitch: player.Pitch.DegreesF
        );

        _dataManager.SavePlayerData(player, save);
    }
}
