using Gen.Saves;
using ManagedServer.Entities.Types;
using Minecraft.Data.Blocks;
using Minecraft.Implementations.Server.Terrain;
using Minecraft.Schemas.Items;
using Minecraft.Schemas.Shapes;
using Minecraft.Schemas.Vec;
using Minecraft.Text;

namespace Gen;

public record GenConfig(
    string DefaultDimensionId,
    GenDimension[] Dimensions,
    ItemStack[] Items,
    IGenDataManager? DataManager = null,
    int PortalEnterTicks = 60);

public record GenMine(IBlock Block, ItemStack Reward, TimeSpan RespawnTime, params (Vec3<int> From, Vec3<int> To)[] Area);

public record GenShop(Vec3<double> Position, float Facing, TextComponent Name, params GenTrade[] Trades);

public record GenTrade(ItemStack Input1, ItemStack? Input2, ItemStack Output);

public record GenDimension(string Id, ITerrainProvider Map, Vec3<double> Spawn, GenMine[] Mines, GenShop[] Shops, GenPortal[] Portals, int DeathHeight = -10);

public record GenPortal(Vec3<int> From, Vec3<int> To, string TargetDimensionId, Vec3<double>? TargetPosition = null) {
    public Aabb GetAabb() {
        // We need to construct the AABB by getting maxes and mins and adding appropriate offsets
        // the int coordinate of the block is at the neg, neg corner of the block.
        // Aabb takes pos and size
        Vec3<double> min = new(
            Math.Min(From.X, To.X),
            Math.Min(From.Y, To.Y),
            Math.Min(From.Z, To.Z)
        );
        Vec3<double> max = new(
            Math.Max(From.X, To.X),
            Math.Max(From.Y, To.Y),
            Math.Max(From.Z, To.Z)
        );
        return new Aabb(min, max - min + (1, 1, 1));
    }
    
    public bool IsColliding(PlayerEntity player) {
        ICollisionBox playerBounding = new Aabb(new Vec3<double>(player.Type.Width * -0.5, 0.0, player.Type.Width * -0.5),
            new Vec3<double>(player.Type.Width, player.Type.Height, player.Type.Width));
        return playerBounding.Add(player.Position).CollidesWithAabb(GetAabb());
    }
}
