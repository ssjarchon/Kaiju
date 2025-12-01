using UnityEngine;

namespace KaijuCoop.Mecha
{
    [System.Flags]
    public enum WeaponType
    {
        None = 0,
        Melee = 1 << 0,
        Ballistic = 1 << 1,
        Energy = 1 << 2,
        Explosive = 1 << 3
    }

    public abstract class Weapon : ScriptableObject
    {
        [Header("Meta")]
        public string DisplayName;
        public WeaponType Type;
        public float Weight;
        public float Stagger;

        public abstract void TriggerAttack(MechaChassis owner);
    }

    [CreateAssetMenu(menuName = "KaijuCoop/Weapons/LaserSword")]
    public class LaserSword : Weapon
    {
        public float SwingDamage = 180f;
        public float ParryWindow = 0.2f;

        public override void TriggerAttack(MechaChassis owner)
        {
            Debug.Log($"{owner.DisplayName} swings {DisplayName} for {SwingDamage} energy damage. Parry window: {ParryWindow}s");
        }
    }

    [CreateAssetMenu(menuName = "KaijuCoop/Weapons/GatlingCannon")]
    public class GatlingCannon : Weapon
    {
        public float SpinUpTime = 0.6f;
        public float DamagePerBullet = 12f;
        public float OverheatThreshold = 8f;

        public override void TriggerAttack(MechaChassis owner)
        {
            Debug.Log($"{owner.DisplayName} fires {DisplayName} after {SpinUpTime}s spin-up. Tracking overheat after {OverheatThreshold}s.");
        }
    }

    [CreateAssetMenu(menuName = "KaijuCoop/Weapons/MissileRack")]
    public class MissileRack : Weapon
    {
        public int MissileCount = 8;
        public float LockOnTime = 1.25f;
        public bool VerticalLaunch = true;

        public override void TriggerAttack(MechaChassis owner)
        {
            var pattern = VerticalLaunch ? "vertical" : "lateral";
            Debug.Log($"{owner.DisplayName} launches {MissileCount} missiles ({pattern}) after {LockOnTime}s lock.");
        }
    }
}
