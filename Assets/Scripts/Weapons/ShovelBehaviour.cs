using slaughter.de.Actors.Enemy;
using slaughter.de.Pooling;
using UnityEngine;
namespace slaughter.de.Weapons
{
    public class ShovelBehaviour : ProjectileWeaponBehaviour
    {
        public Vector3 target;
        float speed;

        void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }


        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy")) // Vergewissere dich, dass du das Tag "Enemy" den Gegnern zugewiesen hast
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
        void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        void SetTarget(Vector3 newTarget)
        {
            var direction = (newTarget - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            target = newTarget;
        }

        void SetSpeed(float newSpeed)
        {
            speed = newSpeed;
        }
        void makeDamage(Collider2D collision)
        {
            var enemy = collision.gameObject.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(5f);
            }
        }
    }
}
