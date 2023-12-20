﻿using System;
using System.Collections;
using slaughter.de.Actors.Enemy;
using slaughter.de.Pooling;
using UnityEngine;
namespace slaughter.de.Managers
{
    public class WaveManager : MonoBehaviour
    {
        private Coroutine _waveCoroutine;

        public GameObject player;
        public float waveDuration = 10f; // Dauer der Welle in Sekunden
        public float nextSpawnTime = 10f;
        [SerializeField]
        float spawnRate;
        [SerializeField]
        Vector3 spawnLocation;
        readonly ControversialThemes[] waveThemes = (ControversialThemes[])Enum.GetValues(typeof(ControversialThemes));

        int _currentWaveIndex;

        bool _isWaveActive;
        float _waveTimer;
        public static WaveManager Instance { get; private set; }


        void Awake()
        {
            MakeSingelton();
        }

        public float GetWaveTimer()
        {
            return _waveTimer;
        }

        public void SetWaveTimer(float time)
        {
            _waveTimer = time;
        }


        public void StartNextWave()
        {
            if (_currentWaveIndex < waveThemes.Length)
            {
                _isWaveActive = true;
                _waveCoroutine = StartCoroutine(SpawnWave(waveThemes[_currentWaveIndex]));
                _currentWaveIndex++;
            }
            else
            {
                Debug.Log("Alle Wellen abgeschlossen");
                // Spielende oder Übergang zu einem neuen Level
            }
        }

        public void StopWave()
        {
            _isWaveActive = false;
            if (_waveCoroutine != null)
            {
                StopCoroutine(_waveCoroutine);
                _waveCoroutine = null;
            }
            // Gegner entfernen
            var enemies = FindObjectsOfType<EnemyController>();
            foreach (var enemy in enemies)
            {
                EnemyPoolManager.Instance.Return(enemy.gameObject);
            }

            _waveTimer = 0f;
        }

        public void ResetWaves()
        {
            StopWave(); // Stellt sicher, dass alle aktuellen Aktivitäten gestoppt werden
            _currentWaveIndex = 0; // Setzt den Index der aktuellen Welle zurück
            _waveTimer = waveDuration; // Setzt den Wave-Timer zurück
            // isWaveActive = false; // Setzt den Status der Welle zurück
            // Eventuell weitere zurückzusetzende Variablen
        }


        IEnumerator SpawnWave(ControversialThemes theme)
        {
            float startTime = Time.time;
            nextSpawnTime = Time.time; // Initialisiere nextSpawnTime mit der aktuellen Zeit

            while (_isWaveActive && Time.time - startTime < waveDuration)
            {
                if (Time.time > nextSpawnTime)
                {
                    SpawnGroupOrSingle();
                    nextSpawnTime = Time.time + spawnRate; // Aktualisiere nextSpawnTime für den nächsten Spawn
                }
                yield return null;
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

        void SpawnGroupOrSingle()
        {
            bool spawnGroup = UnityEngine.Random.Range(0, 2) > 0;
            int groupSize = spawnGroup ? UnityEngine.Random.Range(2, 5) : 1;

            for (int i = 0; i < groupSize; i++)
            {
                Vector3 spawnPos = GetRandomSpawnPosition();
                SpawnEnemyAtPosition(spawnPos);
            }
        }

        Vector3 GetRandomSpawnPosition()
        {
            // Implementiere Logik, um eine zufällige Position außerhalb des Sichtfeldes zu wählen
            // Beispiel: Zufällige Position um den Spieler herum
            float distance = 10f; // Außerhalb des Sichtfeldes
            Vector2 randomDirection = UnityEngine.Random.insideUnitCircle.normalized;
            Vector3 spawnPos = player.transform.position + new Vector3(randomDirection.x, randomDirection.y, 0) * distance;
            return spawnPos;
        }

        void SpawnEnemyAtPosition(Vector3 position)
        {
            GameObject enemy = EnemyPoolManager.Instance.Get();
            enemy.transform.position = position;
            enemy.SetActive(true);
        }

        void Update()
        {
            DespawnEnemiesOutOfRange();
        }

        void DespawnEnemiesOutOfRange()
        {
            float despawnDistance = 30f; // Beispielwert für die Entfernung zum Despawnen
            foreach (var enemy in FindObjectsOfType<EnemyController>())
            {
                if (Vector3.Distance(player.transform.position, enemy.transform.position) > despawnDistance)
                {
                    EnemyPoolManager.Instance.Return(enemy.gameObject);
                }
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
