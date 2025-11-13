using Gen.Saves;
using ManagedServer.Entities.Types;

namespace TestApp;

public class DelayedDataManagerWrapper(IGenDataManager child, int ms = 1000) : IGenDataManager {
    
    public void SavePlayerData(PlayerEntity player, PlayerSave data) {
        Console.WriteLine("Saving...");
        Thread.Sleep(ms);
        child.SavePlayerData(player, data);
    }

    public async Task<PlayerSave?> LoadPlayerData(PlayerEntity player) {
        Console.WriteLine("Loading...");
        await Task.Delay(ms);
        return await child.LoadPlayerData(player);
    }
}
