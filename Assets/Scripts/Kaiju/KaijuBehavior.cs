using System.Collections.Generic;
using UnityEngine;

namespace KaijuCoop.Kaiju
{
    public enum KaijuPhase
    {
        Calm,
        Enraged,
        Critical
    }

    public class KaijuBehavior : MonoBehaviour
    {
        [Header("Meta")]
        public string DisplayName;
        public float MaxHealth = 5000f;
        public float PartBreakThreshold = 750f;

        [Header("Phase Tuning")]
        public List<KaijuPhaseProfile> Phases = new();

        private float currentHealth;
        private KaijuPhase currentPhase;
        private Dictionary<string, float> partDamage = new();

        private void Awake()
        {
            currentHealth = MaxHealth;
            currentPhase = KaijuPhase.Calm;
        }

        public void TakeDamage(float amount, string part = "core")
        {
            currentHealth = Mathf.Max(0f, currentHealth - amount);
            AccumulatePartDamage(part, amount);
            EvaluatePhase();
            if (currentHealth <= 0f)
            {
                Debug.Log($"Kaiju {DisplayName} defeated by hunters!");
            }
        }

        private void AccumulatePartDamage(string part, float amount)
        {
            if (!partDamage.ContainsKey(part))
            {
                partDamage.Add(part, 0f);
            }

            partDamage[part] += amount;
            if (partDamage[part] >= PartBreakThreshold)
            {
                Debug.Log($"{DisplayName}'s {part} has been broken! Exposing weak points.");
                partDamage[part] = 0f;
            }
        }

        private void EvaluatePhase()
        {
            foreach (var profile in Phases)
            {
                if (currentHealth <= profile.HealthThreshold && currentPhase != profile.Phase)
                {
                    currentPhase = profile.Phase;
                    Debug.Log($"{DisplayName} shifts to {currentPhase} phase: {profile.Callout}");
                    profile.OnPhaseEntered?.Invoke(this);
                    break;
                }
            }
        }
    }
}
