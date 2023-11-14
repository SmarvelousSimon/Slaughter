using slaughter.de.Actors.Enemy;
using slaughter.de.Pooling;
using UnityEngine;
namespace slaughter.de.Weapons
{
    public class ShovelBehaviour : ProjectileWeaponBehaviour
    {
        public Vector3 target;
        float speed;

        public void Initialize(Vector3 position, Vector3 newTarget, float newSpeed)
        {
            SetPosition(position);
            SetTarget(newTarget);
            SetSpeed(newSpeed);
        }
        void SetPosition(Vector3 position)
        {
            this.transform.position = position;
        }

        void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }

        private void SetTarget(Vector3 newTarget)
        {
            var direction = (newTarget - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            target = newTarget;
        }

        private void SetSpeed(float newSpeed)
        {
            speed = newSpeed;
        }
        
        
        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy")) // Vergewissere dich, dass du das Tag "Enemy" den Gegnern zugewiesen hast
            {
                makeDamage(collision);
                ShovelPoolManager.Instance.Return(gameObject);
            }
        }
        void makeDamage(Collider2D collision)
        {
            var enemy = collision.gameObject.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.takeDamage(5f);
            }
        }

    }
}
