using System;
using slaughter.de.Actors.Character;
namespace slaughter.de.Managers
{
    public class GameManager : StateMachine
    {
        private PlayerController _playerController;
        public GameManager(State currentState)
        {
            CurrentState = currentState;
        }

        public static GameManager Instance { get; private set; }
        State CurrentState
        {
            get;
        }


        void Awake()
        {
            _playerController = FindObjectOfType<PlayerController>();
            MakeSingelton();
        }

        void Start()
        {
            // currentState = new PrepareState(); // Zeile 22
            SetState(new PrepareState()); // Rufe SetState mit einer Instanz von PrepareState auf

            StartCoroutine(State.Prepare()); // Zeile 24
        }

        public Type GetCurrentStateType()
        {
            return CurrentState?.GetType();
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
        public void ResetPlayer()
        {
            if (_playerController != null)
            {
                _playerController.ResetHealth();
            }
        }
    }
}
