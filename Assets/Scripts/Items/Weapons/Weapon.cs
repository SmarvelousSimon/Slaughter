using slaughter.de.ExtensionMethods;
using slaughter.de.Pooling;
using UnityEngine;

namespace slaughter.de.Items.Weapons
{
    public class Weapon
    {
        public WeaponData WeaponData { get; }
        private readonly LayerMask _layer;
        private readonly ObjectPool<Bullet> _pool;

        private bool _onCooldown;

        public Weapon(WeaponData weaponData, ObjectPool<Bullet> bulletPool, LayerMask layer)
        {
            WeaponData = weaponData;
            _pool = bulletPool;
            _layer = layer;
        }

        public void Attack(Vector3 position, Vector3 direction)
        {
            if (_onCooldown) return;

            var bullet = _pool.SpawnObject(position);
            bullet.SetData(WeaponData);
            bullet.StartPosition = position;
            bullet.Direction = direction;
            bullet.LayerMask = _layer;
            bullet.transform.localScale = new Vector3(WeaponData.spriteScale, WeaponData.spriteScale, 0);

            StartCooldown().Forget();
        }

        private async Awaitable StartCooldown()
        {
            _onCooldown = true;
            await Awaitable.WaitForSecondsAsync(WeaponData.attackRate);
            _onCooldown = false;
        }
    }
}