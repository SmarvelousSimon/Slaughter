using slaughter.de.Actors.Player;
using slaughter.de.Items.Coins;
using slaughter.de.Items.Weapons;
using slaughter.de.Pooling;
using slaughter.de.Utilities;
using UnityEngine;

namespace slaughter.de.Actors.Enemy
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Enemy : MonoBehaviour, IPoolable<Enemy>, IDamageable
    {
        [SerializeReference] [HideInInspector] private SpriteRenderer spriteRenderer;
        [SerializeReference] [HideInInspector] private BoxCollider2D boxCollider;

        private EnemyData _data;
        private float _health;
        private Weapon _weapon;
        public PlayerController Player { get; set; }
        public CoinSpawner CoinSpawner { get; set; }
        public ObjectPool<Bullet> BulletPool { get; set; }

        private Vector3 _translateDirection = Vector3.right;

        private void Update()
        {
            var direction = Player.transform.position - transform.position;
            _data.spriteOrientation.SetTransformDirection(direction, transform);

            if (direction.magnitude < _data.attackRange)
            {
                _weapon.Attack(transform.position, Player.transform.position);
                return;
            }

            transform.Translate(_translateDirection * (_data.speed * Time.deltaTime));
        }


#if UNITY_EDITOR
        private void OnValidate()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            boxCollider = GetComponent<BoxCollider2D>();
        }
#endif

        public void TakeDamage(float damage)
        {
            _health -= damage;
            if (_health > 0) return;

            CoinSpawner.SpawnCoin(transform.position, _data.strength);
            Pool.ReturnObject(this);
        }

        public ObjectPool<Enemy> Pool { get; set; }

        public void SetData(EnemyData data, LayerMask layer)
        {
            _data = data;
            spriteRenderer.sprite = _data.sprite;
            _health = data.health;
            spriteRenderer.color = _data.color;
            boxCollider.size = spriteRenderer.bounds.size;
            _translateDirection = _data.spriteOrientation.GetTranslateDirection();
            _weapon = _data.weaponData.GetWeapon(BulletPool, layer);
        }
    }
}