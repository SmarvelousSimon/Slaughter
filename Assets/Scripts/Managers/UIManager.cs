using System;
using UnityEngine;

namespace slaughter.de.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject levelMenu;
        [SerializeField] private GameObject gameOverMenu;

        private bool isPaused = false;

        public event Action OnItemSelectionCompleted;
        public event Action OnRestartGameCompleted;
        public event Action OnCloseGameCompleted;

        public static UIManager Instance { get; private set; }

        private void Awake()
        {
            DisableAllMenues();
            MakeSingelton();
        }

        private void DisableAllMenues()
        {
            levelMenu.SetActive(false);
            gameOverMenu.SetActive(false);
        }

        #region WaveMenu

        public void OpenLevelMenu()
        {
            PauseGame();
            levelMenu.SetActive(true);
        }

        public void CloseLevelMenu()
        {
            ResumeGame();
            levelMenu.SetActive(false);
        }

        public void ItemSelected()
        {
            // Diese Methode wird aufgerufen, wenn der Spieler seine Auswahl getroffen hat und auf einen Button klickt
            OnItemSelectionCompleted?.Invoke();
        }

        #endregion

        public void RestartGame()
        {
            OnRestartGameCompleted?.Invoke();
        }

        public void CloseGameAndGoToMainMenu()
        {
            OnCloseGameCompleted?.Invoke();
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

        public void OpenGameOverMenu()
        {
            PauseGame();
            gameOverMenu.SetActive(true);
        }

        public void CloseGameOverMenu()
        {
            ResumeGame();
            gameOverMenu.SetActive(false);
        }

        // Diese Funktion können Sie aufrufen, wenn das Menü geöffnet wird
        public void PauseGame()
        {
            Time.timeScale = 0f;
            isPaused = true;
        }

        // Diese Funktion können Sie aufrufen, wenn das Menü geschlossen wird
        public void ResumeGame()
        {
            Time.timeScale = 1f;
            isPaused = false;
        }


        public void ResetUI()
        {
            Debug.Log("ResetUI");

            // Setze UI-Elemente zurück
            // ...
            //
        }
    }
}