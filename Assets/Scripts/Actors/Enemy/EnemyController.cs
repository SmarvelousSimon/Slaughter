using System.Collections;
using slaughter.de.Coin;
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
        private bool canTakeDamage = true;

        private Vector2 targetPosition; // Die Position, der der Gegner folgen wird

        public float followDelay = 0.5f; // Verzögerung in Sekunden

        private static readonly WaitForSeconds followWait;
        private static readonly WaitForSeconds oneSecondWait = new WaitForSeconds(1f);

        private Vector2 direction; // Wiederverwendeter Vector zur Richtung

        static EnemyController()
        {
            // Cache für WaitForSeconds-Objekte, um Allokationen zu reduzieren
            followWait = new WaitForSeconds(0.5f);
        }

        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            InvokeRepeating(nameof(UpdateTargetPosition), 0f, followDelay);
        }

        private void Update()
        {
            MoveTowardsTarget();
        }

        private void UpdateTargetPosition()
        {
            if (player != null)
            {
                targetPosition = player.transform.position;
            }
        }

        private void MoveTowardsTarget()
        {
            direction = targetPosition - (Vector2)transform.position;
            direction.Normalize();
            transform.position += (Vector3)(direction * speed * Time.deltaTime);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                StartCoroutine(DoDamageOn(collision.gameObject));
                Debug.Log("Spieler Tot");
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            canTakeDamage = false;
        }

        public void TakeDamage(float damage)
        {
            health -= damage;

            if (health <= 0)
            {
                KillEnemy();
            }
        }

        private IEnumerator DoDamageOn(GameObject damageTaker)
        {
            canTakeDamage = true;
            while (canTakeDamage)
            {
                TakeDamage(10f);
                yield return oneSecondWait;
            }
        }

        private void KillEnemy()
        {
            ShovelPoolManager.Instance.Return(gameObject);
            DropCoin();
        }

        private void DropCoin()
        {
            var droppedCoin = GetRandomCoinType();
            var coin = CoinPoolManager.Instance.GetCoin(droppedCoin);
            coin.transform.position = transform.position; // Platziere die Münze dort, wo der Gegner gestorben ist
        }

        private CoinType GetRandomCoinType()
        {
            // Hier kannst du eine Zufallslogik basierend auf den Seltenheiten implementieren
            var randomValue = Random.Range(0, 100); // Beispielwert

            if (randomValue < 50) return CoinType.Common; // 50% Wahrscheinlichkeit
            if (randomValue < 70) return CoinType.Uncommon; // 20% Wahrscheinlichkeit
            if (randomValue < 85) return CoinType.Rare; // 15% Wahrscheinlichkeit
            // Füge hier weitere Wahrscheinlichkeiten für die anderen Typen hinzu
            return CoinType.Common; // Standardfall
        }
    }
}