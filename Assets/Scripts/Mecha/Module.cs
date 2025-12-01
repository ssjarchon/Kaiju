using UnityEngine;

namespace KaijuCoop.Mecha
{
    public abstract class Module : ScriptableObject
    {
        [Header("Meta")]
        public string DisplayName;
        public string Description;
        public bool GrantsFlight;

        public virtual void OnApplied(MechaChassis chassis)
        {
            // Override to modify chassis stats or add behaviors.
        }
    }

    [CreateAssetMenu(menuName = "KaijuCoop/Modules/FlightPack")]
    public class FlightPack : Module
    {
        public float FlightDurationBonus = 6f;
        public float BoostCapacityBonus = 10f;

        public override void OnApplied(MechaChassis chassis)
        {
            chassis.FlightDuration += FlightDurationBonus;
            chassis.BoostCapacity += BoostCapacityBonus;
            GrantsFlight = true;
        }
    }
}
