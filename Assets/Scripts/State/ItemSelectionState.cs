using System.Collections;
using slaughter.de.Managers;
using UnityEngine;

namespace slaughter.de.State
{
    public class ItemSelectionState : State
    {
        private bool selectionCompleted;

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
            StateManager.Instance.SetState(new WaveState());
        }

        private void HandleSelectionCompleted()
        {
            selectionCompleted = true;
            UIManager.Instance.OnItemSelectionCompleted -= HandleSelectionCompleted;
        }
    }
}