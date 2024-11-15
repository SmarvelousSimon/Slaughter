using UnityEngine;

namespace slaughter.de.Item.Weapons.Base
{
    public class WeaponBase : MonoBehaviour
    {
        public float damage;
        public float speed;
        public float cooldownDuration;
        private float _currentCooldown;

        protected virtual void Start()
        {
            _currentCooldown = cooldownDuration;
        }

        protected virtual void Update()
        {
            _currentCooldown -= Time.deltaTime;
            if (_currentCooldown <= 0f)
            {
                Attack();
                _currentCooldown = cooldownDuration;
            }
        }

        protected virtual void Attack()
        {
            // Basisangriffslogik
        }
    }
}