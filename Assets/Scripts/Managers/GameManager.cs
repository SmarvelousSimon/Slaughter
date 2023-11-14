using System;
namespace slaughter.de.Managers
{
    public class GameManager : StateMachine
    {
        public static GameManager Instance { get; private set; }
        public State CurrentState
        {
            get;
        }


        void Awake()
        {
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
    }
}
