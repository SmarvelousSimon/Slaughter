using System.Collections;
using slaughter.de.Pooling;
using UnityEngine;
namespace slaughter.de.Managers
{
    public class PrepareState : State
    {
        
        public override IEnumerator Start()
        {
            yield return Prepare();
        }

        public override IEnumerator Prepare()
        {
            Debug.Log("Enter pepare state.");
            UIManager.Instance.CloseLevelMenu(); //TODO hier müssen alle menüs aus 
            yield return new WaitForSeconds(2f);
            Debug.Log("Exit pepare state.");
            GameManager.Instance.SetState(new WaveState());
        }
       
    }
}
