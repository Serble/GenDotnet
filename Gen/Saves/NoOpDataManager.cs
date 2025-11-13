using ManagedServer.Entities.Types;

namespace Gen.Saves;

public class NoOpDataManager : IGenDataManager {
    public void SavePlayerData(PlayerEntity player, PlayerSave data) {
        
    }

    public Task<PlayerSave?> LoadPlayerData(PlayerEntity player) {
        return Task.FromResult<PlayerSave?>(null);
    }
}
