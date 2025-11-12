# Gen
A Minecraft Gen game mode implementation using [Minecraft Dotnet](https://github.com/CoPokBl/MinecraftDotnet).

## Features
- Map loading from `ITerrainProvider` implementations.
- Extendable player save management through `IGenDataManager`.
- Mines that give an item and regenerate.
- Villager shops to trade items.
- Multi dimension support with any non-solid block supported as a portal.
- Void death.
- Fully configurable through the `GenConfig` record.
