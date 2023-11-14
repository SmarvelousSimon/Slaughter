using System.Collections;
using UnityEngine;
namespace slaughter.de.Managers
{
    public class GameOverState : State
    {
        bool selectionCompleted;
        bool playerWantToStartAnotherGame;

        public override IEnumerator Start()
        {
            yield return GameOver();
        }
        private IEnumerator GameOver()
        {
            UIManager.Instance.OnGameOverCompleted += HandleSelectionCompleted;
            UIManager.Instance.OpenGameOverMenu();
            Debug.Log("GameOver");
            yield return new WaitUntil(() => selectionCompleted);
            UIManager.Instance.CloseGameOverMenu();
            if (playerWantToStartAnotherGame) // TODO wie soll hier der bool entschieden werden
            {
                GameManager.Instance.SetState(new PrepareState());
            }
            else
            {
                GameManager.Instance.SetState(new MenuState());
            }



        }

        void HandleSelectionCompleted() // TODO noch nicht fertig implementiert
        {
            selectionCompleted = true;
            UIManager.Instance.OnGameOverCompleted -= HandleSelectionCompleted;
        }
    }
}
