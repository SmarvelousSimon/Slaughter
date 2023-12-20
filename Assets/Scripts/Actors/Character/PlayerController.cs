using System.Collections;
using slaughter.de.Managers;
using UnityEngine;
namespace slaughter.de.Actors.Character
{
    public class PlayerController : MonoBehaviour, CharacterBase
    {
        public float health = 100f;
        private int enemyCollisionCount = 0; // Zählt, wie viele Gegner den Spieler berühren

        void Start()
        {
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
            Debug.Log("Player Health: " + health);
            if (health <= 0)
            {
                KillPlayer();
            }
        }


        void KillPlayer()
        {
            GameManager.Instance.SetState(new GameOverState());
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                enemyCollisionCount++;
                if (enemyCollisionCount == 1) // Starte die Coroutine nur, wenn der erste Gegner den Spieler berührt
                {
                    StartCoroutine(TakeDamageOverTime());
                }
            }
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                enemyCollisionCount--;
            }
        }

        IEnumerator TakeDamageOverTime()
        {
            while (enemyCollisionCount > 0)
            {
                TakeDamage(10);
                yield return new WaitForSeconds(1); // Wartezeit zwischen den Schadensereignissen
            }
        }
        
        void ResetPlayer()
        {
            var playerController = FindObjectOfType<PlayerController>();
            if (playerController != null)
            {
                playerController.ResetHealth(); // Methode im PlayerController zum Zurücksetzen der Gesundheit
            }
        }
        
        public void ResetHealth()
        {
            health = 100f; // Standardgesundheit
            enemyCollisionCount = 0; // Zurücksetzen der Kollisionen
            // Weitere zurückzusetzende Parameter
        }

    }
}
