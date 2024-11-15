using System.Collections;
using slaughter.de.enums;
using slaughter.de.Managers;
using UnityEngine;

namespace slaughter.de.State
{
    public class GameOverState : State
    {
        private bool selectionCompleted = false;
        private bool playerWantToStartAnotherGame = false;

        public override IEnumerator Start()
        {
            yield return GameOver();
        }

        private IEnumerator GameOver()
        {
            UIManager.Instance.OnRestartGameCompleted += HandleRestartGameCompleted;
            UIManager.Instance.OnCloseGameCompleted += HandleCloseGameCompleted;

            UIManager.Instance.OpenGameOverMenu();
            Debug.Log("GameOver");

            // Warte, bis ein Ereignis ausgelöst wird
            yield return new WaitUntil(() => selectionCompleted);

            GameManager.Instance.CloseGameOverMenu();

            if (playerWantToStartAnotherGame)
            {
                GameManager.Instance.ResetLevel(Reason.PlayerDeath);
                StateManager.Instance.SetState(new PrepareState());
            }
            else
            {
                StateManager.Instance.SetState(new MenuState());
            }
        }

        private void HandleRestartGameCompleted()
        {
            selectionCompleted = true;
            playerWantToStartAnotherGame = true;
            UIManager.Instance.OnRestartGameCompleted -= HandleRestartGameCompleted;
            UIManager.Instance.OnCloseGameCompleted -= HandleCloseGameCompleted;
        }

        private void HandleCloseGameCompleted()
        {
            selectionCompleted = true;
            playerWantToStartAnotherGame = false;
            UIManager.Instance.OnRestartGameCompleted -= HandleRestartGameCompleted;
            UIManager.Instance.OnCloseGameCompleted -= HandleCloseGameCompleted;
        }
        
    }
}