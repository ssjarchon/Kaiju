using System.Collections.Generic;
using UnityEngine;

namespace KaijuCoop.Mecha
{
    [System.Serializable]
    public class MechaHardpoint
    {
        public string Id;
        public WeaponType AllowedTypes;
        public Weapon EquippedWeapon;
    }

    public enum ChassisClass
    {
        Light,
        Medium,
        Heavy,
        Aerial
    }

    public class MechaChassis : MonoBehaviour
    {
        [Header("Meta")]
        public string DisplayName;
        public ChassisClass Class;

        [Header("Core Stats")]
        public float MaxHealth = 1200f;
        public float StaggerResistance = 100f;
        public float BoostCapacity = 60f;
        public float FlightDuration = 12f;

        [Header("Mobility")]
        public float WalkSpeed = 8f;
        public float BoostSpeed = 18f;
        public float TurnRate = 180f;

        [Header("Customization")]
        public List<MechaHardpoint> Hardpoints = new();
        public List<Module> Modules = new();

        private float currentHealth;
        private float boostMeter;
        private float flightTimer;
        private bool isFlying;

        private void Awake()
        {
            currentHealth = MaxHealth;
            boostMeter = BoostCapacity;
        }

        public void EquipWeapon(string hardpointId, Weapon weapon)
        {
            var hardpoint = Hardpoints.Find(h => h.Id == hardpointId);
            if (hardpoint == null)
            {
                Debug.LogWarning($"Hardpoint {hardpointId} not found on {DisplayName}");
                return;
            }

            if (!weapon.Type.HasFlag(hardpoint.AllowedTypes))
            {
                Debug.LogWarning($"Weapon {weapon.name} incompatible with hardpoint {hardpointId}");
                return;
            }

            hardpoint.EquippedWeapon = weapon;
        }

        public void ActivateThrusters(float deltaTime)
        {
            if (boostMeter <= 0f)
            {
                return;
            }

            boostMeter = Mathf.Max(0f, boostMeter - deltaTime);
        }

        public void RechargeThrusters(float deltaTime)
        {
            boostMeter = Mathf.Min(BoostCapacity, boostMeter + deltaTime * 5f);
        }

        public void ToggleFlight(bool enabled)
        {
            if (!HasFlightModule())
            {
                return;
            }

            isFlying = enabled;
        }

        public void TickFlight(float deltaTime)
        {
            if (!isFlying)
            {
                flightTimer = Mathf.Min(FlightDuration, flightTimer + deltaTime * 2f);
                return;
            }

            flightTimer = Mathf.Max(0f, flightTimer - deltaTime);
            if (flightTimer <= 0f)
            {
                isFlying = false;
                Debug.Log($"{DisplayName} is forced to land; flight timer depleted.");
            }
        }

        public void TakeDamage(float amount)
        {
            currentHealth = Mathf.Max(0f, currentHealth - amount);
            if (currentHealth <= 0f)
            {
                Debug.Log($"{DisplayName} has been downed.");
            }
        }

        public void ApplyModule(Module module)
        {
            Modules.Add(module);
            module.OnApplied(this);
        }

        public bool HasFlightModule()
        {
            return Modules.Exists(m => m.GrantsFlight);
        }
    }
}
