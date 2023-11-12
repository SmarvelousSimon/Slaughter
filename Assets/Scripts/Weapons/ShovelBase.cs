using slaughter.de.Pooling;
using UnityEngine;
namespace slaughter.de.Weapons
{
    public class ShovelBase : WeaponBase
    {
        public float attackRange = 5;
        public LayerMask enemyLayer = 8;

        protected override void Attack()
        {
            base.Attack();
            var hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);
            if (hitEnemies.Length > 0)
            {
                GameObject spawnedShovel = ShovelPoolManager.Instance.Get();
                var shovelBehaviour = spawnedShovel.GetComponent<ShovelBehaviour>();
                if (shovelBehaviour != null)
                {
                    shovelBehaviour.Initialize(this.transform.position ,hitEnemies[0].transform.position, speed);
                }
            }
        }
    }
}
