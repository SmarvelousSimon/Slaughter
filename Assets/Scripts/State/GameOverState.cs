using System.Collections;
using slaughter.de.Actors.Character;
using slaughter.de.Pooling;
using UnityEngine;
namespace slaughter.de.Managers
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

            UIManager.Instance.CloseGameOverMenu();

            if (playerWantToStartAnotherGame)
            {
                ClearLevel();
                GameManager.Instance.SetState(new PrepareState());
            }
            else
            {
                GameManager.Instance.SetState(new MenuState());
            }
        }

        void HandleRestartGameCompleted()
        {
            selectionCompleted = true;
            playerWantToStartAnotherGame = true;
            UIManager.Instance.OnRestartGameCompleted -= HandleRestartGameCompleted;
            UIManager.Instance.OnCloseGameCompleted -= HandleCloseGameCompleted;
        }

        void HandleCloseGameCompleted()
        {
            selectionCompleted = true;
            playerWantToStartAnotherGame = false;
            UIManager.Instance.OnRestartGameCompleted -= HandleRestartGameCompleted;
            UIManager.Instance.OnCloseGameCompleted -= HandleCloseGameCompleted;
        }

        void ClearLevel()
        {
            Debug.Log("ClearLevel");
            GameManager.Instance.ResetPlayer();
            WaveManager.Instance.ResetWaves(); // schmeißt die gegner in den pool
            // EnemyPoolManager.Instance.ResetPool();
            UIManager.Instance.ResetUI();
            CoinPoolManager.Instance.ResetPool();
            // set all coins to zero
            // set hp to default
            // back to pool
            // reset progress
        }
    }
}
