using System.Collections;
using UnityEngine;

namespace slaughter.de.State
{
    public abstract class State
    {
        protected Coroutine RunningCoroutine;

        public virtual IEnumerator Start()
        {
            yield break;
        }

        public virtual IEnumerator ItemSelection()
        {
            yield break;
        }

        public virtual IEnumerator Wave()
        {
            yield break;
        }

        public virtual IEnumerator Pause()
        {
            yield break;
        }

        public virtual IEnumerator Prepare()
        {
            yield break;
        }

        public void StopRunningCoroutine(MonoBehaviour owner)
        {
            if (RunningCoroutine != null)
            {
                owner.StopCoroutine(RunningCoroutine);
                RunningCoroutine = null;
            }
        }
    }
}