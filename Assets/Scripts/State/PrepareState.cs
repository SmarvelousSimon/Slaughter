using System.Collections;
using UnityEngine;
namespace slaughter.de.Managers
{
    public class PrepareState : State
    {
        
        public override IEnumerator Prepare()
        {
            Debug.Log("Enter pepare state.");
            UIManager.Instance.CloseLevelMenu();
            yield return new WaitForSeconds(2f);
            Debug.Log("Exit pepare state.");
            GameManager.Instance.SetState(new WaveState());
            
        }
    }
}
