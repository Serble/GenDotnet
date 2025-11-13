using ManagedServer.Entities.Types;

namespace Gen.Saves;

public class MemoryDataManager : IGenDataManager {
    private static readonly Dictionary<Guid, PlayerSave> Data = [];
    
    public void SavePlayerData(PlayerEntity player, PlayerSave data) {
        Data[player.Uuid] = data;
    }

    public Task<PlayerSave?> LoadPlayerData(PlayerEntity player) {
        return Task.FromResult(Data.GetValueOrDefault(player.Uuid));
    }
}
