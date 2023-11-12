using slaughter.de.Pooling;
using UnityEngine;
namespace slaughter.de.Managers
{
    public class GameManager : MonoBehaviour
    {
        public GameState currentState;
        public float waveDuration = 10f; // Dauer der Welle in Sekunden
        float waveTimer;
        public float nextSpawnTime = 10f;
        [SerializeField]
        float spawnRate;
        [SerializeField]
        Vector3 spawnLocation;
        public float GetWaveTimer()
        {
            return waveTimer;
        }

        public static GameManager Instance { get; private set; }

        void Awake()
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

        void Start()
        {
            ChangeState(GameState.WaveInProgress);
        }

        void Update()
        {

            switch (currentState)
            {
                case GameState.Running:
                    // Logik für das laufende Spiel
                    break;
                case GameState.WaveInProgress:
                    waveTimer -= Time.deltaTime;
                    if (waveTimer <= 0 && currentState == GameState.WaveInProgress) // Zusätzliche Überprüfung hier
                    {
                        Debug.Log("Wave ended. Switching to item selection.");
                        ChangeState(GameState.ItemSelection);
                    }
                    else
                    {
                        // Hier wird die Spawn-Logik eingebunden
                        SpawnEnemies();
                    }
                    break;
                    // Logik für die Gegnerwellen
                    break;
                case GameState.Paused:
                    // Pausenlogik
                    break;
                case GameState.ItemSelection:
                    // Logik für die Item-Auswahl
                    break;
            }
        }
        void SpawnEnemies()
        {
            if (Time.time > nextSpawnTime)
            {
                GameObject enemy = EnemyPoolManager.Instance.Get();
                enemy.transform.position = spawnLocation; // Setze die Position des Gegners
                enemy.SetActive(true); // Aktiviere den Gegner

                nextSpawnTime = Time.time + spawnRate;
            }
        }


        public void ChangeState(GameState newState)
        {
            if (currentState != newState) // Verhindert, dass der Zustand mehrmals hintereinander gesetzt wird
            {
                currentState = newState;
                switch (newState)
                {
                    case GameState.WaveInProgress:
                        waveTimer = waveDuration;
                        StartWave();
                        break;
                    case GameState.ItemSelection:
                        OpenItemSelectionMenu();
                        break;
                    // Weitere Zustandsübergänge
                }
            }
        }

        void StartWave()
        {
            // Initialisiere und starte Gegnerwelle
        }

        void OpenItemSelectionMenu()
        {
            // Zeige das Menü für die Item-Auswahl
        }

        // Weitere Methoden...
    }
}
