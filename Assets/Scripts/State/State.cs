using System.Collections;
namespace slaughter.de.Managers
{
    public abstract class State
    {
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
    }
}
