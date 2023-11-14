using System.Collections;
using slaughter.de.Pooling;
using UnityEngine;
namespace slaughter.de.Weapons
{

    public class ProjectileWeaponBehaviour : MonoBehaviour
    {
        public float destroyAfterSeconds;

        protected virtual void Start()
        {
            // StartCoroutine(ReturnToPoolAfterDelay(destroyAfterSeconds));
        }

        IEnumerator ReturnToPoolAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            Debug.Log("Send Back: " + delay);
            ShovelPoolManager.Instance.Return(gameObject);
        }
    }
}
