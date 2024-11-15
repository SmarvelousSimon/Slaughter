using System.Collections;
using slaughter.de.Pooling;
using slaughter.de.State;
using UnityEngine;

namespace slaughter.de.Managers
{
    public class PrepareState : State.State
    {
        public override IEnumerator Start()
        {
            yield return Prepare();
        }

        public override IEnumerator Prepare()
        {
            GameManager.Instance.CloseAllMenues();
            yield return new WaitForSeconds(2f);
            StateManager.Instance.SetState(new WaveState());
        }
    }
}