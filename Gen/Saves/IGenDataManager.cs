using ManagedServer.Entities.Types;

namespace Gen.Saves;

public interface IGenDataManager {
    void SavePlayerData(PlayerEntity player, PlayerSave data);
    PlayerSave? LoadPlayerData(PlayerEntity player);
}
