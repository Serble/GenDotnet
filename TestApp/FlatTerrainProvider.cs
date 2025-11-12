using Minecraft.Data.Blocks;
using Minecraft.Implementations.Server.Terrain.Providers;

namespace TestApp;

public class FlatTerrainProvider(IBlock block) : PerBlockTerrainProvider {
    
    public override uint GetBlock(int x, int y, int z) {
        return y == 64 ? block.StateId : 0;
    }
}
