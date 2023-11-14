using System;
using System.Collections;
using slaughter.de.Actors.Character;
using slaughter.de.Pooling;
using UnityEngine;

namespace slaughter.de.Actors.Enemy
{
    public class EnemyController : MonoBehaviour, CharacterBase
    {
        private bool canTakeDamage = true;
        public float health = 5f;
    
    
        public GameObject player;
    
        public float speed = 1f;
    
        public int damage = 1;
    
        private void Start()
        {
            player = GameObject.FindWithTag("Player");
        }
    
        private void Update()
        {
            Vector2 direction = player.transform.position - transform.position;
    
            // Normalisiere die Richtung (mache sie zu einem Einheitsvektor)
            direction.Normalize();
    
            transform.position = transform.position + (Vector3)(direction * speed * Time.deltaTime);
        }
    
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                StartCoroutine(DoDamageOn(collision.gameObject));
                Debug.Log("Spieler Tot");
            }
        }

        void OnCollisionExit(Collision other)
        {
            this.canTakeDamage = false;
        }

        private IEnumerator DoDamageOn(GameObject damageTaker)
        {
            this.canTakeDamage = true;
            while (canTakeDamage) // sobald on Collusion Exit muss das auf false !!!!!!!!!!!!
            {
                damageTaker.GetComponent<PlayerController>().takeDamage(10f);
                yield return new WaitForSeconds(1);
            }
        
            yield return null;
        }


        public void takeDamage(float damage)
        {
            health -= damage;
            
            if (health <= 0)
            {
                killEnemy();
            }
        }
    
        private void killEnemy()
        {
            // Stelle sicher, dass du den PoolManager korrekt eingerichtet hast, um diesen Aufruf zu unterstützen
            ShovelPoolManager.Instance.Return(gameObject);
        }
    }
}
