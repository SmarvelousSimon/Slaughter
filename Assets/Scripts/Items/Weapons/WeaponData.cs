using slaughter.de.Pooling;
using slaughter.de.Utilities;
using UnityEngine;

namespace slaughter.de.Items.Weapons
{
    [CreateAssetMenu(menuName = "Data/WeaponData", fileName = "WeaponData")]
    public class WeaponData : ScriptableObject
    {
        public Sprite thumbnail;
        public int cost;
        public int wave;
        
        public float range;
        public float speed;
        public float damage;
        public float attackRate;

        public Sprite bulletSprite;
        public float spriteScale = 1f;
        public Orientation spriteOrientation;

        public Weapon GetWeapon(ObjectPool<Bullet> bulletPool, LayerMask layer)
        {
            return new Weapon(this, bulletPool, layer);
        }
    }
}