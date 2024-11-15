using System;
using slaughter.de.Actors.Character;
using slaughter.de.enums;
using slaughter.de.Pooling;
using UnityEngine;

namespace slaughter.de.Managers
{
    public class GameManager : StateMachine
    {
        public event Action gameReset;
 
        private PlayerController _playerController;

        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            _playerController = FindFirstObjectByType<PlayerController>();
            MakeSingleton();
        }

        private void Start()
        {
            SetState(new PrepareState()); // Zustand wechseln und Start-Methode ausführen
        }

        public Type GetCurrentStateType()
        {
            return State?.GetType(); // Gibt den Typ des aktuellen Zustands zurück
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
    }
}