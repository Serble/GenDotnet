using ManagedServer.Entities.Types;

namespace Gen.Saves;

public class NoOpDataManager : IGenDataManager {
    public void SavePlayerData(PlayerEntity player, PlayerSave data) {
        
    }

    public PlayerSave? LoadPlayerData(PlayerEntity player) {
        return null;
    }
}
