using Gen;
using Gen.Saves;
using Minecraft.Data.Generated;
using Minecraft.Schemas.Items;
using Minecraft.Schemas.Vec;
using Minecraft.Text;

namespace TestApp;

public static class TestConfig {
    // CONSTANTS
    public static readonly TimeSpan DefaultGenTime = TimeSpan.FromSeconds(16);
    
    // ============= Resources =============
    // Raw
    public static readonly ItemStack Stone = new ItemStack(Item.Cobblestone).WithTag(GenGame.GenItemTag, "stone");
    public static readonly ItemStack Iron = new ItemStack(Item.IronIngot).WithTag(GenGame.GenItemTag, "iron");
    public static readonly ItemStack Diamond = new ItemStack(Item.Diamond).WithTag(GenGame.GenItemTag, "diamond");
    public static readonly ItemStack Netherrack = new ItemStack(Item.Netherrack).WithTag(GenGame.GenItemTag, "netherrack");
    // Compressed
    public static readonly ItemStack Log = new ItemStack(Item.OakLog).WithTag(GenGame.GenItemTag, "wood");
    public static readonly ItemStack CompressedStone = new ItemStack(Item.Stone).WithTag(GenGame.GenItemTag, "compressed_stone");
    public static readonly ItemStack CompressedIron = new ItemStack(Item.IronBlock).WithTag(GenGame.GenItemTag, "compressed_iron");
    public static readonly ItemStack CompressedDiamond = new ItemStack(Item.DiamondBlock).WithTag(GenGame.GenItemTag, "compressed_diamond");
    
    // ============= Axes =============
    // Wood
    public static readonly ItemStack AxeWood1 = new ItemStack(Item.WoodenAxe).WithTag(GenGame.GenItemTag, "wood_axe_1");
    public static readonly ItemStack AxeWood2 = new ItemStack(Item.WoodenAxe).With(DataComponent.Enchantments, [(Enchantment.Efficiency, 1)]).WithTag(GenGame.GenItemTag, "wood_axe_2");
    public static readonly ItemStack AxeWood3 = new ItemStack(Item.WoodenAxe).With(DataComponent.Enchantments, [(Enchantment.Efficiency, 2)]).WithTag(GenGame.GenItemTag, "wood_axe_3");
    public static readonly ItemStack AxeWood4 = new ItemStack(Item.WoodenAxe).With(DataComponent.Enchantments, [(Enchantment.Efficiency, 3)]).WithTag(GenGame.GenItemTag, "wood_axe_4");
    // Stone
    public static readonly ItemStack AxeStone1 = new ItemStack(Item.StoneAxe).WithTag(GenGame.GenItemTag, "stone_axe_1");
    public static readonly ItemStack AxeStone2 = new ItemStack(Item.StoneAxe).With(DataComponent.Enchantments, [(Enchantment.Efficiency, 1)]).WithTag(GenGame.GenItemTag, "stone_axe_2");
    public static readonly ItemStack AxeStone3 = new ItemStack(Item.StoneAxe).With(DataComponent.Enchantments, [(Enchantment.Efficiency, 2)]).WithTag(GenGame.GenItemTag, "stone_axe_3");
    public static readonly ItemStack AxeStone4 = new ItemStack(Item.StoneAxe).With(DataComponent.Enchantments, [(Enchantment.Efficiency, 3)]).WithTag(GenGame.GenItemTag, "stone_axe_4");
    // Iron
    public static readonly ItemStack AxeIron1 = new ItemStack(Item.IronAxe).WithTag(GenGame.GenItemTag, "iron_axe_1");
    public static readonly ItemStack AxeIron2 = new ItemStack(Item.IronAxe).With(DataComponent.Enchantments, [(Enchantment.Efficiency, 1)]).WithTag(GenGame.GenItemTag, "iron_axe_2");
    public static readonly ItemStack AxeIron3 = new ItemStack(Item.IronAxe).With(DataComponent.Enchantments, [(Enchantment.Efficiency, 2)]).WithTag(GenGame.GenItemTag, "iron_axe_3");
    public static readonly ItemStack AxeIron4 = new ItemStack(Item.IronAxe).With(DataComponent.Enchantments, [(Enchantment.Efficiency, 3)]).WithTag(GenGame.GenItemTag, "iron_axe_4");
    // Diamond
    public static readonly ItemStack AxeDiamond1 = new ItemStack(Item.DiamondAxe).WithTag(GenGame.GenItemTag, "diamond_axe_1");
    public static readonly ItemStack AxeDiamond2 = new ItemStack(Item.DiamondAxe).With(DataComponent.Enchantments, [(Enchantment.Efficiency, 1), (Enchantment.Sharpness, 1)]).WithTag(GenGame.GenItemTag, "diamond_axe_2");
    public static readonly ItemStack AxeDiamond3 = new ItemStack(Item.DiamondAxe).With(DataComponent.Enchantments, [(Enchantment.Efficiency, 2), (Enchantment.Sharpness, 2)]).WithTag(GenGame.GenItemTag, "diamond_axe_3");
    public static readonly ItemStack AxeDiamond4 = new ItemStack(Item.DiamondAxe).With(DataComponent.Enchantments, [(Enchantment.Efficiency, 3), (Enchantment.Sharpness, 3)]).WithTag(GenGame.GenItemTag, "diamond_axe_4");
    
    // ============= Pickaxes =============
    // Wood
    public static readonly ItemStack PickaxeWood1 = new ItemStack(Item.WoodenPickaxe).WithTag(GenGame.GenItemTag, "wood_pickaxe_1");
    public static readonly ItemStack PickaxeWood2 = new ItemStack(Item.WoodenPickaxe).With(DataComponent.Enchantments, [(Enchantment.Efficiency, 1)]).WithTag(GenGame.GenItemTag, "wood_pickaxe_2");
    public static readonly ItemStack PickaxeWood3 = new ItemStack(Item.WoodenPickaxe).With(DataComponent.Enchantments, [(Enchantment.Efficiency, 2)]).WithTag(GenGame.GenItemTag, "wood_pickaxe_3");
    public static readonly ItemStack PickaxeWood4 = new ItemStack(Item.WoodenPickaxe).With(DataComponent.Enchantments, [(Enchantment.Efficiency, 3)]).WithTag(GenGame.GenItemTag, "wood_pickaxe_4");
    // Stone
    public static readonly ItemStack PickaxeStone1 = new ItemStack(Item.StonePickaxe).WithTag(GenGame.GenItemTag, "stone_pickaxe_1");
    public static readonly ItemStack PickaxeStone2 = new ItemStack(Item.StonePickaxe).With(DataComponent.Enchantments, [(Enchantment.Efficiency, 1)]).WithTag(GenGame.GenItemTag, "stone_pickaxe_2");
    public static readonly ItemStack PickaxeStone3 = new ItemStack(Item.StonePickaxe).With(DataComponent.Enchantments, [(Enchantment.Efficiency, 2)]).WithTag(GenGame.GenItemTag, "stone_pickaxe_3");
    public static readonly ItemStack PickaxeStone4 = new ItemStack(Item.StonePickaxe).With(DataComponent.Enchantments, [(Enchantment.Efficiency, 3)]).WithTag(GenGame.GenItemTag, "stone_pickaxe_4");
    // Iron
    public static readonly ItemStack PickaxeIron1 = new ItemStack(Item.IronPickaxe).WithTag(GenGame.GenItemTag, "iron_pickaxe_1");
    public static readonly ItemStack PickaxeIron2 = new ItemStack(Item.IronPickaxe).With(DataComponent.Enchantments, [(Enchantment.Efficiency, 1)]).WithTag(GenGame.GenItemTag, "iron_pickaxe_2");
    public static readonly ItemStack PickaxeIron3 = new ItemStack(Item.IronPickaxe).With(DataComponent.Enchantments, [(Enchantment.Efficiency, 2)]).WithTag(GenGame.GenItemTag, "iron_pickaxe_3");
    public static readonly ItemStack PickaxeIron4 = new ItemStack(Item.IronPickaxe).With(DataComponent.Enchantments, [(Enchantment.Efficiency, 3)]).WithTag(GenGame.GenItemTag, "iron_pickaxe_4");
    // Diamond
    public static readonly ItemStack PickaxeDiamond1 = new ItemStack(Item.DiamondPickaxe).WithTag(GenGame.GenItemTag, "diamond_pickaxe_1");
    public static readonly ItemStack PickaxeDiamond2 = new ItemStack(Item.DiamondPickaxe).With(DataComponent.Enchantments, [(Enchantment.Efficiency, 1)]).WithTag(GenGame.GenItemTag, "diamond_pickaxe_2");
    public static readonly ItemStack PickaxeDiamond3 = new ItemStack(Item.DiamondPickaxe).With(DataComponent.Enchantments, [(Enchantment.Efficiency, 2)]).WithTag(GenGame.GenItemTag, "diamond_pickaxe_3");
    public static readonly ItemStack PickaxeDiamond4 = new ItemStack(Item.DiamondPickaxe).With(DataComponent.Enchantments, [(Enchantment.Efficiency, 3)]).WithTag(GenGame.GenItemTag, "diamond_pickaxe_4");
    
    public static readonly GenConfig Config = new("overworld", [
        new GenDimension("overworld", new FlatTerrainProvider(Block.GrassBlock), new Vec3<double>(0.5, 5, 0.5), 
            [  // Mines
                new GenMine(Block.OakLog, Log, DefaultGenTime, ((-2, 5, -58), (2, 4, -60))),
                new GenMine(Block.Cobblestone, Stone, DefaultGenTime, ((-14, 5, -61), (-17, 4, -63))),
            ], [  // Shops
                // Utility Shops
                new GenShop((-13.5, 4, -1.5), 0, TextComponent.FromLegacyString("Compressor"), [
                    new GenTrade(Stone.WithCount(6), null, CompressedStone),
                    new GenTrade(Iron.WithCount(6), null, CompressedIron),
                    new GenTrade(Diamond.WithCount(6), null, CompressedDiamond)
                ]),
                
                // Axe Shops
                new GenShop((15.5, 4, -1.5), 0, TextComponent.FromLegacyString("Wooden Axe"), [
                    new GenTrade(Log, null, AxeWood1),
                    new GenTrade(Log.WithCount(16), AxeWood1, AxeWood2),
                    new GenTrade(Log.WithCount(32), AxeWood2, AxeWood3),
                    new GenTrade(Log.WithCount(64), AxeWood3, AxeWood4)
                ]),
                new GenShop((17.5, 4, -1.5), 0, TextComponent.FromLegacyString("Stone Axe"), [
                    new GenTrade(CompressedStone, AxeWood4, AxeStone1),
                    new GenTrade(CompressedStone.WithCount(16), AxeStone1, AxeStone2),
                    new GenTrade(CompressedStone.WithCount(32), AxeStone2, AxeStone3),
                    new GenTrade(CompressedStone.WithCount(64), AxeStone3, AxeStone4)
                ]),
                new GenShop((19.5, 4, -1.5), 0, TextComponent.FromLegacyString("Iron Axe"), [
                    new GenTrade(CompressedIron, AxeStone4, AxeIron1),
                    new GenTrade(CompressedIron.WithCount(16), AxeIron1, AxeIron2),
                    new GenTrade(CompressedIron.WithCount(32), AxeIron2, AxeIron3),
                    new GenTrade(CompressedIron.WithCount(64), AxeIron3, AxeIron4)
                ]),
                new GenShop((21.5, 4, -1.5), 0, TextComponent.FromLegacyString("Diamond Axe"), [
                    new GenTrade(CompressedDiamond, AxeIron4, AxeIron1),
                    new GenTrade(CompressedDiamond.WithCount(16), AxeDiamond1, AxeDiamond2),
                    new GenTrade(CompressedDiamond.WithCount(32), AxeDiamond2, AxeDiamond3),
                    new GenTrade(CompressedDiamond.WithCount(64), AxeDiamond3, AxeDiamond4)
                ]),
                
                // Pickaxe Shops
                new GenShop((15.5, 4, 2.5), 180, TextComponent.FromLegacyString("Wooden Pickaxe"), [
                    new GenTrade(Log, null, PickaxeWood1),
                    new GenTrade(Log.WithCount(16), PickaxeWood1, PickaxeWood2),
                    new GenTrade(Log.WithCount(32), PickaxeWood2, PickaxeWood3),
                    new GenTrade(Log.WithCount(64), PickaxeWood3, PickaxeWood4)
                ]),
                new GenShop((17.5, 4, 2.5), 180, TextComponent.FromLegacyString("Stone Pickaxe"), [
                    new GenTrade(CompressedStone, PickaxeWood4, PickaxeStone1),
                    new GenTrade(CompressedStone.WithCount(16), PickaxeStone1, PickaxeStone2),
                    new GenTrade(CompressedStone.WithCount(32), PickaxeStone2, PickaxeStone3),
                    new GenTrade(CompressedStone.WithCount(64), PickaxeStone3, PickaxeStone4)
                ]),
            ], [  // Portals
            new GenPortal((0, 1, 2), (0, 2, 3), "nether")
        ]),
        
        new GenDimension("nether", new FlatTerrainProvider(Block.Obsidian), new Vec3<double>(0.5, 5, 0.5), [
            new GenMine(Block.Netherrack, Netherrack, DefaultGenTime, ((-2, 5, -58), (2, 4, -60)))
        ], [
            new GenShop((15.5, 4, 2.5), 180, TextComponent.FromLegacyString("&c&kWooden Pickaxe"), [
                new GenTrade(PickaxeWood1, Netherrack.WithCount(5), PickaxeDiamond1)
            ])
        ], [])
        ], [
            Stone, Iron, Diamond, Netherrack,
            Log, CompressedStone, CompressedIron, CompressedDiamond,
            AxeWood1, AxeWood2, AxeWood3, AxeWood4,
            AxeStone1, AxeStone2, AxeStone3, AxeStone4,
            AxeIron1, AxeIron2, AxeIron3, AxeIron4,
            AxeDiamond1, AxeDiamond2, AxeDiamond3, AxeDiamond4,
            PickaxeWood1, PickaxeWood2, PickaxeWood3, PickaxeWood4,
            PickaxeStone1, PickaxeStone2, PickaxeStone3, PickaxeStone4,
            PickaxeIron1, PickaxeIron2, PickaxeIron3, PickaxeIron4,
            PickaxeDiamond1, PickaxeDiamond2, PickaxeDiamond3, PickaxeDiamond4
    ], DataManager:new DelayedDataManagerWrapper(new MemoryDataManager()));
}
