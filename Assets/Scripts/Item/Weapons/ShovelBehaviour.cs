using slaughter.de.Actors.Enemy;
using slaughter.de.Pooling;
using UnityEngine;

namespace slaughter.de.Weapons
{
    public class ShovelBehaviour : ProjectileWeaponBehaviour
    {
        public Vector3 target;
        private float speed;

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject
                .CompareTag("Enemy")) // Vergewissere dich, dass du das Tag "Enemy" den Gegnern zugewiesen hast
            {
                makeDamage(collision);
                ShovelPoolManager.Instance.Return(gameObject);
            }
        }

        public void Initialize(Vector3 position, Vector3 newTarget, float newSpeed)
        {
            SetPosition(position);
            SetTarget(newTarget);
            SetSpeed(newSpeed);
        }

        private void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        private void SetTarget(Vector3 newTarget)
        {
            var direction = (newTarget - transform.position).normalized;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            target = newTarget;
        }

        private void SetSpeed(float newSpeed)
        {
            speed = newSpeed;
        }

        private void makeDamage(Collider2D collision)
        {
            var enemy = collision.gameObject.GetComponent<EnemyController>();
            if (enemy != null) enemy.TakeDamage(5f);
        }
    }
}