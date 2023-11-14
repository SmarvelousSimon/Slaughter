using System.Collections;
using slaughter.de.Pooling;
using UnityEngine;
namespace slaughter.de.Actors.Enemy
{
    public class EnemyController : MonoBehaviour, CharacterBase
    {
        public float health = 5f;


        public GameObject player;

        public float speed = 1f;

        public int damage = 1;
        bool canTakeDamage = true;

        void Start()
        {
            player = GameObject.FindWithTag("Player");
        }

        void Update()
        {
            Vector2 direction = player.transform.position - transform.position;

            // Normalisiere die Richtung (mache sie zu einem Einheitsvektor)
            direction.Normalize();

            transform.position = transform.position + (Vector3)(direction * speed * Time.deltaTime);
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                StartCoroutine(DoDamageOn(collision.gameObject));
                Debug.Log("Spieler Tot");
            }
        }

        void OnCollisionExit(Collision other)
        {
            canTakeDamage = false;
        }


        public void TakeDamage(float damage)
        {
            health -= damage;

            if (health <= 0)
            {
                killEnemy();
            }
        }

        IEnumerator DoDamageOn(GameObject damageTaker)
        {
            canTakeDamage = true;
            while (canTakeDamage) // sobald on Collusion Exit muss das auf false !!!!!!!!!!!!
            {
                TakeDamage(10f);
                yield return new WaitForSeconds(1);
            }

            yield return null;
        }

        void killEnemy()
        {
            // Stelle sicher, dass du den PoolManager korrekt eingerichtet hast, um diesen Aufruf zu unterstützen
            ShovelPoolManager.Instance.Return(gameObject);
        }
    }
}
