using System.Collections;
using slaughter.de.Managers;
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
        private Vector2 targetPosition; // Die Position, der der Gegner folgen wird


        public float followDelay = 0.5f; // Verzögerung in Sekunden
        void Start()
        {
            player = GameObject.FindWithTag("Player");
        }


        void Update()
        {
            StartCoroutine(UpdateTargetPosition());
            MoveTowardsTarget();
        }


        IEnumerator UpdateTargetPosition()
        {
            while (true)
            {
                targetPosition = player.transform.position;
                yield return new WaitForSeconds(followDelay);
            }
        }

        void MoveTowardsTarget()
        {
            Vector2 direction = targetPosition - (Vector2)transform.position;
            direction.Normalize();
            transform.position += (Vector3)(direction * speed * Time.deltaTime);
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
            ShovelPoolManager.Instance.Return(gameObject);
            DropCoin();

        }

        void DropCoin()
        {

            CoinType droppedCoin = GetRandomCoinType();
            GameObject coin = CoinPoolManager.Instance.GetCoin(droppedCoin);
            coin.transform.position = transform.position; // Platziere die Münze dort, wo der Gegner gestorben ist

        }


        CoinType GetRandomCoinType()
        {
            // Hier kannst du eine Zufallslogik basierend auf den Seltenheiten implementieren
            int randomValue = UnityEngine.Random.Range(0, 100); // Beispielwert

            if (randomValue < 50) return CoinType.Common; // 50% Wahrscheinlichkeit
            if (randomValue < 70) return CoinType.Uncommon; // 20% Wahrscheinlichkeit
            if (randomValue < 85) return CoinType.Rare; // 15% Wahrscheinlichkeit
            // Füge hier weitere Wahrscheinlichkeiten für die anderen Typen hinzu
            return CoinType.Common; // Standardfall
        }
    }
}
