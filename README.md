# AlpacaVenture

Playable jam game about quinoa farming and climate shocks.

[![Gameplay banner](docs/Game.gif)](docs/Game.gif)

Playable build: https://rileycn.itch.io/alpacaventure
Research paper: https://doi.org/10.4324/9781003356837-14

## Status
- Made for a 48-hour game jam. Not production-quality, created quickly to deliver a playable experience.
- Unity Editor only — tested with editor version `6000.0.36f1` (see [ProjectSettings/ProjectVersion.txt](ProjectSettings/ProjectVersion.txt)).

## Quick start (Editor)
1. Open this folder in Unity Editor (version ~2025/6000.0.36f1).
2. Open and play the scene: [Assets/Scenes/Tutorial.unity](Assets/Scenes/Tutorial.unity).

Notes: There is a playable build linked above on itch.io; the repository is Editor-only for development.

## One-line pitch
You are a quinoa farmer growing over multiple years: plant, maintain, harvest, and survive climate-driven disasters through short minigames.

## Gameplay summary
- You play up to 3 years (sessions). Each year is represented by a sequence of seasons (Planting → Rainy → Harvest → Dry).
- During Planting you drag seeds/fertilizer onto planting plots. During Rainy you maintain plants with water/fertilizer. During Harvest you harvest crops.
- Random weather disasters (wind, rain, drought) spawn. Each may trigger a short minigame you must complete to avoid losses.
- Between years you visit the shop to buy seed/water/fertilizer with in-game currency.

## Controls
- Mouse: click-and-drag items (seed, water, fertilizer, harvest ring).
- Click/Interact: mouse click to select and drag.

## Scenes (playable)
- [Assets/Scenes/Tutorial.unity](Assets/Scenes/Tutorial.unity) — tutorial & intro dialogue.
- [Assets/Scenes/GameScene.unity](Assets/Scenes/GameScene.unity) — main gameplay (field, disasters, minigames).
- [Assets/shop.unity](Assets/shop.unity) — shop between years.
- [Assets/outro screen.unity](Assets/outro screen.unity) — end of game summary.
- Deprecated story/dialogue scenes (for development purposes): [Assets/Introduction Part 1.unity](Assets/Introduction Part 1.unity), [Assets/Part 3.unity](Assets/Part 3.unity), [Assets/Part 4.unity](Assets/Part 4.unity), [Assets/Part 5.unity](Assets/Part 5.unity), [Assets/Part 6.unity](Assets/Part 6.unity).

## Key scripts (overview)
- [Assets/Scripts/GameManager.cs](Assets/Scripts/GameManager.cs#L1) — game loop, seasons, disaster spawning, shop transitions, UI flow.
- [Assets/Scripts/PlantManager.cs](Assets/Scripts/PlantManager.cs#L1) — individual plant lifecycle (plant, water, fertilize, harvest, die).
- [Assets/Scripts/DraggableObject.cs](Assets/Scripts/DraggableObject.cs#L1) — drag/drop interactions for seeds, water, fertilizer, and harvest.
- [Assets/Scripts/FieldManager.cs](Assets/Scripts/FieldManager.cs#L1) — spawns the plant grid and advances plant phases.
- [Assets/DialogueManager.cs](Assets/DialogueManager.cs#L1) — dialog queue and dialog-driven events.
- [Assets/ShopManagerScript.cs](Assets/ShopManagerScript.cs#L1) — shop UI and buying logic.
- Minigames and effects: [Assets/Scripts/MazeGameManager.cs](Assets/Scripts/MazeGameManager.cs#L1), [Assets/Scripts/WindManager.cs](Assets/Scripts/WindManager.cs#L1), [Assets/Scripts/DryManager.cs](Assets/Scripts/DryManager.cs#L1), [Assets/Scripts/rain_spawner.cs](Assets/Scripts/rain_spawner.cs#L1).

## Assets & credits
- All art and audio created by the AlpacaVenture team for the jam.

## Screenshots

**Hero**

[![Gameplay banner](docs/Game.gif)](docs/Game.gif)

**Gameplay**

[![Field — planting](docs/Gameplay.png)](docs/Gameplay.png)

_Caption: Planting interaction._

**Minigames**

[![Wind minigame](docs/Minigame.png)](docs/Minigame.png)

_Caption: Example minigame encountered during weather events._

**Menu**

[![Main menu](docs/MainMenu.png)](docs/MainMenu.png)

_Caption: Main menu interface._

## Research context
This project was created as part of a research-driven game jam showcasing quinoa farming and climate impacts (see chapter at https://doi.org/10.4324/9781003356837-14). The narrative and design were inspired by this research.

## For maintainers / recruiters
- What to look at first: open [Assets/Scenes/GameScene.unity](Assets/Scenes/GameScene.unity) and then inspect [Assets/Scripts/GameManager.cs](Assets/Scripts/GameManager.cs) and [Assets/Scripts/PlantManager.cs](Assets/Scripts/PlantManager.cs) to understand the game loop and plant lifecycle.
- Notes: project was implemented in a short timeframe (48 hours). The README focuses on design and systems rather than code polish.
