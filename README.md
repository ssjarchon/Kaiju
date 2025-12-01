# Kaiju Co-op

A cooperative multiplayer action game where pilots customize modular mecha and team up to hunt massive Kaiju. Combat mixes the speed and buildcraft of **Armored Core** with the deliberate target-breaking of **Monster Hunter**.

## Core Pillars
- **Co-op first:** drop-in/out 2–4 player hunts with shared objectives, revives, and coordinated weak-point breaks.
- **Buildcraft:** chassis, cores, thrusters, and weapon hardpoints that encourage experimenting with melee, ranged, and support loadouts.
- **Expressive Kaiju:** unique behaviors, elemental phases, and breakable armor that change how teams approach each fight.
- **Spectacle + readability:** cinematic mecha vs. monster clashes that remain clear and tactical.

## Repository Layout
- `Docs/` – design notes and encounter overviews.
- `Assets/Scripts/` – Unity C# scripts for core systems (mecha customization, weapons, Kaiju AI, and session flow).
- `mission_statement.txt` – original project intent.

## Getting Started
1. Create a new Unity project (LTS recommended) and copy the `Assets` folder into it.
2. Add the scripts to a scene (e.g., a `GameManager` object with `CoopSessionManager` and `KaijuEncounterDirector`).
3. Hook up inputs to `MechaChassis` methods (`EquipWeapon`, `ActivateThrusters`, etc.) and drive `KaijuBehavior` states from your AI/animation setup.
4. Flesh out data via ScriptableObjects or prefabs for chassis, weapons, and Kaiju variants.

## Playstyle Targets
- Aerial specialists providing weak-point access while ground heavies stagger limbs.
- Coordinated part breaks to expose cores before unleashing burst damage.
- Teams swapping roles between hunts to leverage new loot and experimental builds.

## Next Steps
- Implement multiplayer networking layer (Netcode for GameObjects or similar) around `CoopSessionManager`.
- Build prototype arenas and 2–3 Kaiju with distinct gimmicks (burrowing, flight, beam zones).
- Integrate progression/loot loop that rewards part breaks and successful captures.
- Playtest frequently to tune mecha mobility, stamina, and weapon timings.
