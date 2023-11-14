using System.Collections;
using UnityEngine;
namespace slaughter.de.Managers
{
    public class ItemSelectionState : State
    {
        bool selectionCompleted;

        public override IEnumerator Start()
        {
            // Deine Implementierung der Logik für den WaveState
            yield return ItemSelection(); // Starte die Wave Coroutine
        }

        public override IEnumerator ItemSelection()
        {
            UIManager.Instance.OnItemSelectionCompleted += HandleSelectionCompleted;
            UIManager.Instance.OpenLevelMenu();
            Debug.Log("Open itemselection.");

            yield return new WaitUntil(() => selectionCompleted);

            UIManager.Instance.CloseLevelMenu();
            Debug.Log("Exit itemselection.");
            GameManager.Instance.SetState(new WaveState());
        }

        void HandleSelectionCompleted()
        {
            selectionCompleted = true;
            UIManager.Instance.OnItemSelectionCompleted -= HandleSelectionCompleted;
        }
    }
}
