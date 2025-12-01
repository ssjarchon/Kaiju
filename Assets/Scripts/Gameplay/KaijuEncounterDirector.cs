using System.Collections.Generic;
using UnityEngine;
using KaijuCoop.Mecha;
using KaijuCoop.Kaiju;

namespace KaijuCoop.Gameplay
{
    public class KaijuEncounterDirector : MonoBehaviour
    {
        [Header("Encounter Setup")]
        public CoopSessionManager Session;
        public KaijuBehavior KaijuPrefab;
        public Transform KaijuSpawn;
        public List<Transform> PlayerSpawns = new();
        public List<MechaChassis> PlayerPrefabs = new();

        [Header("Objectives")]
        public float TimeLimitSeconds = 900f;
        public bool AllowCapture = true;

        private float timer;

        private void Start()
        {
            SpawnPlayers();
            SpawnKaiju();
            timer = Time.time + TimeLimitSeconds;
        }

        private void Update()
        {
            if (Time.time > timer)
            {
                Debug.Log("Hunt timed out. Extraction triggered.");
            }
        }

        private void SpawnPlayers()
        {
            for (int i = 0; i < PlayerPrefabs.Count && i < PlayerSpawns.Count; i++)
            {
                var spawn = PlayerSpawns[i];
                var chassis = Instantiate(PlayerPrefabs[i], spawn.position, spawn.rotation);
                Session.RegisterPlayer(chassis);
            }
        }

        private void SpawnKaiju()
        {
            if (KaijuPrefab == null || KaijuSpawn == null)
            {
                Debug.LogWarning("KaijuEncounterDirector is missing prefab or spawn transform.");
                return;
            }

            Session.ActiveKaiju = Instantiate(KaijuPrefab, KaijuSpawn.position, KaijuSpawn.rotation);
        }

        public void OnKaijuCaptured()
        {
            if (!AllowCapture)
            {
                return;
            }

            Debug.Log("Kaiju capture successful! Bonus rewards granted.");
        }
    }
}
