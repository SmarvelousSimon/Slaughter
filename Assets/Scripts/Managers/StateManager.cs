using System;
using slaughter.de.Actors.Character;
using slaughter.de.enums;
using slaughter.de.Pooling;
using slaughter.de.State;
using UnityEngine;

namespace slaughter.de.Managers
{
    /// <summary>
    /// This manager starts and handle only states. Everything else is forbidden
    /// </summary>
    public class StateManager : StateMachine
    {

        public static StateManager Instance { get; private set; }
       
        private void Awake()
        {
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

    }
}