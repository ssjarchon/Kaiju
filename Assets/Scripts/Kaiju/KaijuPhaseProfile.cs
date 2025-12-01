using System;
using UnityEngine;

namespace KaijuCoop.Kaiju
{
    [Serializable]
    public class KaijuPhaseProfile
    {
        public string Name;
        public KaijuPhase Phase;
        public float HealthThreshold;
        public string Callout;
        public Action<KaijuBehavior> OnPhaseEntered;

        public void ExecutePhaseAbility(KaijuBehavior behavior)
        {
            Debug.Log($"{behavior.DisplayName} executes {Name} behavior.");
        }
    }
}
