using UnityEngine;

namespace slaughter.de.Weapons
{
    public class WeaponBase : MonoBehaviour
    {
        public float damage;
        public float speed;
        public float cooldownDuration;
        private float currentCooldown;

        protected virtual void Start()
        {
            currentCooldown = cooldownDuration;
        }

        protected virtual void Update()
        {
            currentCooldown -= Time.deltaTime;
            if (currentCooldown <= 0f)
            {
                Attack();
                currentCooldown = cooldownDuration;
            }
        }

        protected virtual void Attack()
        {
            // Basisangriffslogik
        }
    }
}