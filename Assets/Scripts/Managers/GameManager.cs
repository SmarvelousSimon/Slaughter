using System;
using slaughter.de.Actors.Character;
using slaughter.de.enums;
using slaughter.de.Pooling;
using UnityEngine;

namespace slaughter.de.Managers
{
    public class GameManager : MonoBehaviour
    {
        public float waveDuration = 10f; // Dauer der Welle in Sekunden

        private PlayerController _playerController;

        public static GameManager Instance { get; private set; }


        private void Awake()
        {
            _playerController = FindFirstObjectByType<PlayerController>();
            MakeSingleton();
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

        public void ResetPlayer()
        {
            if (_playerController) _playerController.ResetHealth();
        }

        public void ResetLevel(Reason reason)
        {
            ResetPlayer();
            UIManager.Instance.ResetUI();
            CoinPoolManager.Instance.ResetPool();

            if (reason == Reason.PlayerDeath)
            {
                WaveManager.Instance.ResetWaves(); // schmeißt die gegner in den pool
            }
            else if (reason == Reason.PlayerWantNewTry)
            {
            }
        }

        public void StartNextWave()
        {
            WaveManager.Instance.StartNextWave();
        }

        public void CloseAllMenues()
        {
            UIManager.Instance.CloseLevelMenu(); //TODO hier müssen alle menüs aus 
        }

        public Type GetCurrentStateType()
        {
            return StateManager.Instance.GetCurrentStateType();
        }

        public void SetWaveTimer(float remainingTime)
        {
            WaveManager.Instance.SetWaveTimer(remainingTime);
        }

        public void StopWave()
        {
            WaveManager.Instance.StopWave();
        }

        public void CloseGameOverMenu()
        {
           UIManager.Instance.CloseGameOverMenu();
        }
    }
}