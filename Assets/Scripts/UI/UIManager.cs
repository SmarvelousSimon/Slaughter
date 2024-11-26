using System;
using slaughter.de.Actors.Player;
using UnityEngine;

namespace slaughter.de.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject levelMenu;
        [SerializeField] private GameObject gameOverMenu;
        [SerializeField] private HealthBar healthBar;

        private void Awake()
        {
            DisableAllMenues();
        }

        public event Action OnItemSelectionCompleted;
        public event Action OnRestartGameCompleted;
        public event Action OnCloseGameCompleted;

        public void RegisterPlayer(PlayerController player)
        {
            healthBar.RegisterPlayer(player);
        }

        private void DisableAllMenues()
        {
            levelMenu.SetActive(false);
            gameOverMenu.SetActive(false);
        }

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

        public void RestartGame()
        {
            OnRestartGameCompleted?.Invoke();
        }

        public void CloseGameAndGoToMainMenu()
        {
            OnCloseGameCompleted?.Invoke();
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
        }

        // Diese Funktion können Sie aufrufen, wenn das Menü geschlossen wird
        public void ResumeGame()
        {
            Time.timeScale = 1f;
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