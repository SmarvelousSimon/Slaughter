using System;
using System.Collections;
using slaughter.de.Actors.Enemy;
using slaughter.de.Pooling;
using UnityEngine;
namespace slaughter.de.Managers
{
    public class WaveManager : MonoBehaviour
    {

        public float waveDuration = 10f; // Dauer der Welle in Sekunden
        public float nextSpawnTime = 10f;
        [SerializeField]
        float spawnRate;
        [SerializeField]
        Vector3 spawnLocation;
        readonly ControversialThemes[] waveThemes = (ControversialThemes[])Enum.GetValues(typeof(ControversialThemes));

        int currentWaveIndex;

        bool isWaveActive;
        float waveTimer;
        public static WaveManager Instance { get; private set; }


        void Awake()
        {
            MakeSingelton();
        }

        public float GetWaveTimer()
        {
            return waveTimer;
        }

        public void SetWaveTimer(float time)
        {
            waveTimer = time;
        }


        public void StartNextWave()
        {
            if (currentWaveIndex < waveThemes.Length)
            {
                isWaveActive = true; // Welle als aktiv markieren TODO ist durch den gamestate auch einsehbar
                var currentTheme = waveThemes[currentWaveIndex];
                StartCoroutine(SpawnWave(currentTheme));
                currentWaveIndex++;
            }
            else
            {
                Debug.Log("Alle Wellen abgeschlossen");
                // Spielende oder Übergang zu einem neuen Level
            }
        }

        public void StopWave()
        {
            isWaveActive = false; // Welle als inaktiv markieren

            // Gegner entfernen
            var enemies = FindObjectsOfType<EnemyController>();
            foreach (var enemy in enemies)
            {
                EnemyPoolManager.Instance.Return(enemy.gameObject);
            }

            waveTimer = 0f;
        }

        IEnumerator SpawnWave(ControversialThemes theme)
        {
            float startTime = Time.time;
            while (isWaveActive && Time.time - startTime < waveDuration)
            {
                if (Time.time > nextSpawnTime)
                {
                    SpawnEnemies();
                    nextSpawnTime = Time.time + spawnRate;
                }
                yield return null;
            }

            // Logik für das Ende der Welle, wenn die Welle nicht mehr aktiv ist
            if (!isWaveActive)
            {
                // Zusätzliche Logik, wenn die Welle vorzeitig gestoppt wird
            }
        }

        void SpawnEnemies()
        {
            if (Time.time > nextSpawnTime)
            {
                var enemy = EnemyPoolManager.Instance.Get();
                enemy.transform.position = spawnLocation; // Setze die Position des Gegners
                enemy.SetActive(true); // Aktiviere den Gegner

                nextSpawnTime = Time.time + spawnRate;
            }
        }

        void MakeSingelton()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); // Optional: Verhindert das Zerstören beim Laden
            }
            else
            {
                Destroy(gameObject); // Sicherstellen, dass keine Duplikate existieren
            }
        }
    }
}
