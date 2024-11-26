using slaughter.de.Items.Weapons;
using slaughter.de.RandomSelector;
using slaughter.de.Utilities;
using UnityEngine;

namespace slaughter.de.Actors.Enemy
{
    [CreateAssetMenu(menuName = "Data/EnemyData", fileName = "EnemyData")]
    public class EnemyData : ScriptableObject, IWeightedObject
    {
        public Sprite sprite;
        public float health;
        public float attackRange;
        public float speed;
        public float strength;
        public WeaponData weaponData;
        public Color color;

        public Orientation spriteOrientation;

        public float weight;

        public float BaseWeight => weight;
        public float Weight { get; set; }
    }
}