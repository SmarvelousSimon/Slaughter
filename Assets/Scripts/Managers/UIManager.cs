using System;
using UnityEngine;
namespace slaughter.de.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        GameObject levelMenu;
        public static UIManager Instance { get; private set; }

        void Awake()
        {
            MakeSingelton();
        }

        public event Action OnItemSelectionCompleted;
        public event Action OnGameOverCompleted;
// Start Region WaveMenu
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
// End Region WaveMenu

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
        public void OpenGameOverMenu()
        {
            throw new NotImplementedException();
        }
        public void CloseGameOverMenu()
        {
            throw new NotImplementedException();
        }
    }
}
