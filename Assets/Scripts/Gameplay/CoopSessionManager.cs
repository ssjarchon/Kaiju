using System.Collections.Generic;
using UnityEngine;
using KaijuCoop.Mecha;
using KaijuCoop.Kaiju;

namespace KaijuCoop.Gameplay
{
    public class CoopSessionManager : MonoBehaviour
    {
        [Header("Session State")]
        public List<MechaChassis> Players = new();
        public KaijuBehavior ActiveKaiju;
        public int DownTokens = 3;

        [Header("Synergy")]
        public float SynergyWindow = 2.5f;
        public float SynergyMultiplier = 1.35f;

        private readonly Queue<AttackEvent> recentAttacks = new();

        private void Update()
        {
            CullExpiredAttacks(Time.time);
        }

        public void RegisterPlayer(MechaChassis chassis)
        {
            if (!Players.Contains(chassis))
            {
                Players.Add(chassis);
            }
        }

        public void OnPlayerDown(MechaChassis chassis)
        {
            DownTokens = Mathf.Max(0, DownTokens - 1);
            Debug.Log($"{chassis.DisplayName} is downed. Team tokens remaining: {DownTokens}");

            if (DownTokens <= 0)
            {
                Debug.Log("Team wiped. Hunt failed.");
            }
        }

        public float ApplySynergy(string weakPoint, MechaChassis attacker)
        {
            var now = Time.time;
            recentAttacks.Enqueue(new AttackEvent
            {
                WeakPoint = weakPoint,
                Timestamp = now,
                Attacker = attacker
            });

            bool synergyAchieved = false;
            foreach (var ev in recentAttacks)
            {
                if (ev.Attacker == attacker)
                {
                    continue;
                }

                if (ev.WeakPoint == weakPoint && now - ev.Timestamp <= SynergyWindow)
                {
                    synergyAchieved = true;
                    break;
                }
            }

            return synergyAchieved ? SynergyMultiplier : 1f;
        }

        private void CullExpiredAttacks(float now)
        {
            while (recentAttacks.Count > 0 && now - recentAttacks.Peek().Timestamp > SynergyWindow)
            {
                recentAttacks.Dequeue();
            }
        }

        private struct AttackEvent
        {
            public string WeakPoint;
            public float Timestamp;
            public MechaChassis Attacker;
        }
    }
}
