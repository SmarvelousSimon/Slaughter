using System.Collections;
using UnityEngine;

namespace slaughter.de.Camera
{
    public class FollowCharacter : MonoBehaviour
    {
        public GameObject Character;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(Follow(Character));
        }

        IEnumerator Follow(GameObject charGameObject)
        {
            while (true)
            {
                var pos = new Vector3(charGameObject.transform.position.x, charGameObject.transform.position.y, -15);
                transform.position = pos;
                yield return null;
            }
        }
    }
}
