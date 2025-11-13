namespace Gen.Saves;

public record PlayerSave((string, int)?[] Inventory, float Health, float Yaw, float Pitch);
