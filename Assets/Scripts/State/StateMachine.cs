using UnityEngine;

namespace slaughter.de.Managers
{
    public abstract class StateMachine : MonoBehaviour
    {
        protected State.State State;

        public void SetState(State.State state)
        {
            if (State != null) State.StopRunningCoroutine(this);

            State = state;
            Debug.Log(State);
            StartCoroutine(State.Start());
        }
    }
}