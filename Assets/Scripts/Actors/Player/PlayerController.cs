using System;
using System.Collections.Generic;
using slaughter.de.Core;
using slaughter.de.Items.Coins;
using slaughter.de.Items.Weapons;
using slaughter.de.Pooling;
using UnityEngine;

namespace slaughter.de.Actors.Player
{
    [RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
    public class PlayerController : MonoBehaviour, IDamageable, ICoinCollector
    {
        public WeaponData CurrentWeapon => _weapon.WeaponData;
        
        private static readonly int Move = Animator.StringToHash("Move");

        [SerializeField] [HideInInspector] private Animator animator;
        [SerializeField] [HideInInspector] private SpriteRenderer spriteRenderer;

        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private float maxSpeed = 8f;

        private readonly List<Collider2D> _coinBuffer = new();

        private float _health = 100f;

        private float _maxHealth = 100f;


        private readonly float _speed = 8f;
        
        
        private Weapon _weapon;

        public bool IsDead { get; private set; }
        public ObjectPool<Bullet> BulletPool { get; set; }
        
        private LayerMask _attackLayer;
        private ContactFilter2D _coinFilter;
        private float _coinCollectRadius;

        public GameSettings GameSettings
        {
            set
            {
                _attackLayer = value.enemyLayer;
                _coinCollectRadius = value.coinCollectRadius;
                
                ContactFilter2D coinFilter = new();
                coinFilter.NoFilter();
                coinFilter.layerMask = value.coinLayer;
                coinFilter.useLayerMask = true;
                _coinFilter = coinFilter;
            }
        }

        public float Health
        {
            get => _health;
            private set
            {
                _health = value;
                OnPlayerHealthChanged?.Invoke(_health);
                
                if (_health > 0f) return;

                Debug.Log("Death");
                IsDead = true;
                OnPlayerDeath?.Invoke();
            }
        }

        public float MaxHealth
        {
            get => _maxHealth;
            private set
            {
                _maxHealth = value;
                OnPlayerMaxHealthChanged?.Invoke(_maxHealth);
            }
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = transform.position.z;
                var shootDirection = mousePosition - transform.position;

                _weapon.Attack(transform.position, shootDirection);
            }

            var hits = Physics2D.OverlapCircle(transform.position, _coinCollectRadius, _coinFilter, _coinBuffer);
            if (hits > 0)
                foreach (var hit in _coinBuffer)
                {
                    if (!hit.TryGetComponent(out Coin coin)) continue;

                    coin.Collect(this);
                }

            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            var direction = new Vector2(horizontal, vertical);

            if (direction is { x: 0, y: 0 })
            {
                animator.SetBool(Move, false);
                return;
            }

            animator.SetBool(Move, true);
            spriteRenderer.flipX = direction.x < 0;

            transform.position += (Vector3)(direction * (_speed * Time.deltaTime));
        }

        private void OnDisable()
        {
            animator.SetBool(Move, false);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
#endif
        public event Action<int> OnCoinCollected;

        public Transform Transform => transform;

        public void CollectCoin(int value)
        {
            OnCoinCollected?.Invoke(value);
        }

        public void TakeDamage(float damage)
        {
            Health -= damage;
        }

        public event Action OnPlayerDeath;
        public event Action<float> OnPlayerHealthChanged;
        public event Action<float> OnPlayerMaxHealthChanged;

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void ResetHealth()
        {
            Health = MaxHealth;
            IsDead = false;
        }

        public void Equip(WeaponData weapon)
        {
            _weapon = weapon.GetWeapon(BulletPool, _attackLayer);
        }
    }
}