using System.Collections;
using UnityEngine;
namespace slaughter.de.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        [HideInInspector]
        public float lastViewXDirection;
        [HideInInspector]
        public float lastViewYDirection;
        [HideInInspector]
        public Vector2 moveDir;
        public float speed = 8f;
        [SerializeField]
        UnityEngine.Camera mainCamera;

        readonly bool canMove = true;

        void Start()
        {
            StartCoroutine(MovementAndRotation());
        }

        IEnumerator MovementAndRotation()
        {
            while (true)
            {
                if (canMove) // TODO Corutine wo anders starten weil hier einfach immer durchgerast wird...
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
                }
                // Warte eine Frame, bevor die nächste Iteration der Coroutine ausgeführt wird
                yield return null;
            }
        }
    }
}
