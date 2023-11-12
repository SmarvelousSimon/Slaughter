using UnityEngine;

namespace slaughter.de.Actors.Character
{
    public class PlayerController : MonoBehaviour, CharacterBase
    {
        public float health = 100f;

        // Start is called before the first frame update
        void Start()
        {
        }

        public void takeDamage(float damage)
        {
            health -= damage;
            Debug.Log(health);
        }

        void killPlayer()
        {
        }
    }
}
