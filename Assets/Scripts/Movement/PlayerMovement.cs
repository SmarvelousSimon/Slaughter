using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace slaughter.de.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        [HideInInspector]
        public float lastViewXDirection;
        [HideInInspector]
        public float lastViewYDirection;
        [HideInInspector]
        public Vector2 moveDir = new Vector2();
        public float speed = 8f;

        [SerializeField]
        UnityEngine.Camera mainCamera;

        void Start()
        {
            StartCoroutine(MovementAndRotation());
        }

        IEnumerator MovementAndRotation()
        {
            while (true)
            {
                float horizontal = Input.GetAxis("Horizontal");
                float vertical = Input.GetAxis("Vertical");
                var direction = new Vector2(horizontal, vertical);
                moveDir = direction;

                if (moveDir.x != 0)
                {
                    lastViewXDirection = moveDir.x;
                }

                if (moveDir.y != 0)
                {
                    lastViewYDirection = moveDir.y;
                }


                transform.position += (Vector3)(direction * (speed * Time.deltaTime));

                // Warte eine Frame, bevor die nächste Iteration der Coroutine ausgeführt wird
                yield return null;
            }
        }
    }
}
