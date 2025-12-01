# Kaiju Co-op – Game Design Overview

## Vision
Build a cooperative multiplayer hunt game where squads of 2–4 pilots deploy modular mecha against towering Kaiju. Combat should blend high-speed thruster movement and weapon swaps (Armored Core) with readable patterns, targetable limbs, and resource pushes/pulls (Monster Hunter).

## Player Experience Goals
- **Make co-op indispensable:** Kaiju behaviors should encourage splitting roles (stagger, part break, crowd control) and coordinated bursts.
- **Support build experimentation:** chassis stats, thrusters, armor plating, and weapon hardpoints change the feel of each machine.
- **Reward mastery:** perfect parries, boost hops, flight management, and ammo/heat management raise damage uptime.

## Loop
1. Briefing + loadout selection in a hangar.
2. Drop into a large arena with environmental hazards and bonuses.
3. Identify weak points, break armor, and manage Kaiju phases.
4. Collect parts to upgrade and craft new mecha components.

## Mecha Customization
- **Chassis classes:** light (agile), medium (balanced), heavy (tank), and aerial frames with hover/jet boost.
- **Hardpoints:** dual arms + shoulder mounts configurable per chassis. Slots accept melee, ballistic, energy, or support modules.
- **Modules:** heat sinks, capacitor overdrives, shield emitters, deployable drones, and flight packs.
- **Weapons:**
  - *Laser Sword* – high stagger, parry window, chargeable cleave.
  - *Gatling Cannon* – sustained fire, spin-up, prone to overheating.
  - *Missile Rack* – lock-on salvos, vertical and lateral variants.
- **Progression:** blueprint drops per Kaiju + material requirements; affixes for elemental damage and status (corrosion, emp, blaze).

## Kaiju Design
- **Behavior phases:** calm approach → enraged (aggressive) → critical (desperate new patterns). Limb breaks can short-circuit phase progression.
- **Target points:** head/core (high damage), back armor (flight access), limbs (stagger threshold), tail (sweeps), wings (deny flight).
- **Examples:**
  - *Shale Burrower* – tunnels, pops up with AoE, crystal armor that shatters under concussive hits.
  - *Sky Harrier* – aerial predator, wing armor gates ground access to weak spots; uses beam dives.
  - *Tide Leviathan* – arena floods/ebbs, spawns corrosive pools, tail slam exposes core.

## Co-op Systems
- Shared team objectives and part-break trackers.
- Downed/revive loop with limited uses; med-drones for ranged assists.
- Synergy buffs when weapons of complementary types hit the same weak point within a window.
- Voice callouts and marker pings for coordinated strikes.

## Control/Feel References
- Dash-cancelable light attacks; heavy attacks commit but grant big stagger.
- Boost hop + quick turn for aerial adjustments.
- Guard + parry windows on melee weapons; ranged weapons tie into heat and ammo economy.

## Technical Notes
- Favor ScriptableObjects for weapons/parts data; mecha prefabs assemble parts + visuals.
- Networking: authoritative host with client-side prediction for movement and hit validation.
- Animation events drive hitboxes; consider DOTS/Jobs for Kaiju collision checks.
- Use `CoopSessionManager` to coordinate player spawns and Kaiju encounter flow.
