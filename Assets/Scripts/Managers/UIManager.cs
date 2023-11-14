using System;
using UnityEngine;
namespace slaughter.de.Managers
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }
        [SerializeField] private GameObject levelMenu;

        public event Action OnItemSelectionCompleted;

        void Awake()
        {
            MakeSingelton();
        }

        public void OpenLevelMenu()
        {
            levelMenu.SetActive(true);
        }

        public void CloseLevelMenu()
        {
            levelMenu.SetActive(false);
        }

        public void ItemSelected()
        {
            // Diese Methode wird aufgerufen, wenn der Spieler seine Auswahl getroffen hat und auf einen Button klickt
            OnItemSelectionCompleted?.Invoke();
        }

        private void MakeSingelton()
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
