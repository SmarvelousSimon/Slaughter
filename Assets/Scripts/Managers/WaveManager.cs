using System;
using System.Collections;
using slaughter.de.Actors.Enemy;
using slaughter.de.Pooling;
using UnityEngine;

namespace slaughter.de.Managers
{
    public class WaveManager : MonoBehaviour
    {
        private Coroutine _waveCoroutine;
        private Coroutine _repeatedCoroutine; // Referenz für die Despawn-Coroutine
        public event Action OnWaveEnded; // Ereignis, das ausgelöst wird, wenn die Welle endet


        public GameObject player;
        public float nextSpawnTime = 10f;
        [SerializeField] private float spawnRate;
        [SerializeField] private Vector3 spawnLocation;

        private readonly ControversialThemes[] _waveThemes =
            (ControversialThemes[])Enum.GetValues(typeof(ControversialThemes));

        private int _currentWaveIndex;

        private bool _isWaveActive;
        private float _waveTimer;
        public static WaveManager Instance { get; private set; }

        private void Awake()
        {
            MakeSingleton();
        }

        private void Start()
        {
            // entferne den Start der RepeatedFunctionCoroutine hier
            // Sie wird jetzt innerhalb von StartNextWave gestartet
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
            
            
            // Spieler an die Start position zurück setzen
            // heals des spielers zurück setzen
            
            // coins zurück setzen aber die vorherigen in eine tasche abspeichern
            // alle coins in den pool zurück setzen und allgemein den pool aufräumen?
            
            // alle gegner weg und den pool ggf. aufräumen weil evtl andere gegner kommen?
            
            //timer zurück setzen
            
            if (_currentWaveIndex < _waveThemes.Length)
            {
                _isWaveActive = true;
                _waveCoroutine = StartCoroutine(SpawnWave(_waveThemes[_currentWaveIndex]));
                _repeatedCoroutine = StartCoroutine(RepeatedFunctionCoroutine()); // Starte die Despawn-Coroutine
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

            if (_repeatedCoroutine != null)
            {
                StopCoroutine(_repeatedCoroutine);
                _repeatedCoroutine = null;
            }

            // Gegner entfernen
            var enemies = FindObjectsOfType<EnemyController>();
            foreach (var enemy in enemies) EnemyPoolManager.Instance.Return(enemy.gameObject);

            _waveTimer = 0f;
            Debug.Log("Wave Ended");
            
        }

        public void ResetWaves()
        {
            StopWave(); // Stellt sicher, dass alle aktuellen Aktivitäten gestoppt werden
            _currentWaveIndex = 0; // Setzt den Index der aktuellen Welle zurück
            _waveTimer = GameManager.Instance.waveDuration; // Setzt den Wave-Timer zurück
            _isWaveActive = false; // Setzt den Status der Welle zurück
            // Eventuell weitere zurückzusetzende Variablen
        }


        private IEnumerator SpawnWave(ControversialThemes theme)
        {
            var startTime = Time.time;
            nextSpawnTime = Time.time; // Initialisiere nextSpawnTime mit der aktuellen Zeit

            while (_isWaveActive && Time.time - startTime < GameManager.Instance.waveDuration)
            {
                if (Time.time > nextSpawnTime)
                {
                    SpawnGroupOrSingle();
                    nextSpawnTime = Time.time + spawnRate; // Aktualisiere nextSpawnTime für den nächsten Spawn
                }

                yield return null;
            }

            if (!_isWaveActive)
            {
                Debug.Log("Wave wurde manuell gestoppt.");
            }
            else
            {
                Debug.Log("Wave-Dauer beendet.");
                OnWaveEnded?.Invoke(); // Ereignis auslösen, dass die Welle beendet ist
            }
        }

        private void SpawnGroupOrSingle()
        {
            var spawnGroup = UnityEngine.Random.Range(0, 2) > 0;
            var groupSize = spawnGroup ? UnityEngine.Random.Range(2, 5) : 1;

            for (var i = 0; i < groupSize; i++)
            {
                var spawnPos = GetRandomSpawnPosition();
                SpawnEnemyAtPosition(spawnPos);
            }
        }

        private Vector3 GetRandomSpawnPosition()
        {
            // Implementiere Logik, um eine zufällige Position außerhalb des Sichtfeldes zu wählen
            // Beispiel: Zufällige Position um den Spieler herum
            var distance = 10f; // Außerhalb des Sichtfeldes
            var randomDirection = UnityEngine.Random.insideUnitCircle.normalized;
            var spawnPos = player.transform.position + new Vector3(randomDirection.x, randomDirection.y, 0) * distance;
            return spawnPos;
        }

        private void SpawnEnemyAtPosition(Vector3 position)
        {
            var enemy = EnemyPoolManager.Instance.Get();
            enemy.transform.position = position;
            enemy.SetActive(true);
        }

        private void DespawnEnemiesOutOfRange()
        {
            var despawnDistance = 30f; // Beispielwert für die Entfernung zum Despawnen
            foreach (var enemy in FindObjectsOfType<EnemyController>())
                if (Vector3.Distance(player.transform.position, enemy.transform.position) > despawnDistance)
                    EnemyPoolManager.Instance.Return(enemy.gameObject);
        }

        private void MakeSingleton()
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

        private IEnumerator RepeatedFunctionCoroutine()
        {
            Debug.Log("RepeatedFunctionCoroutine gestartet.");
            while (_isWaveActive)
            {
                DespawnEnemiesOutOfRange(); // Funktion aufrufen
                yield return new WaitForSeconds(3f); // Alle 3 Sekunden pausieren
            }

            Debug.Log("RepeatedFunctionCoroutine wurde gestoppt, weil _isWaveActive false ist.");
        }
    }
}