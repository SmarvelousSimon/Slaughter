using UnityEngine;

namespace slaughter.de.State
{
    public abstract class StateMachine : MonoBehaviour
    {
        protected State State;

        public void SetState(State state)
        {
            if (State != null) State.StopRunningCoroutine(this);

            State = state;
            Debug.Log(State);
            StartCoroutine(State.Start());
        }
    }
}