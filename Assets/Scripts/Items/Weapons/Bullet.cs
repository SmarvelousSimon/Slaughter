using System.Collections.Generic;
using slaughter.de.Actors;
using slaughter.de.Pooling;
using slaughter.de.Utilities;
using UnityEngine;

namespace slaughter.de.Items.Weapons
{
    public class Bullet : MonoBehaviour, IPoolable<Bullet>
    {
        [SerializeField] [HideInInspector] private SpriteRenderer spriteRenderer;
        private readonly List<Collider2D> _buffer = new();
        private WeaponData _data;

        public Vector3 Direction
        {
            set => _data.spriteOrientation.SetTransformDirection(value, transform);
        }
        
        public Vector2 StartPosition { get; set; }
        public LayerMask LayerMask { get; set; }
        private Vector3 _translateDirection = Vector3.up;

        private void Update()
        {
            transform.Translate(_translateDirection * (_data.speed * Time.deltaTime));
            if (Vector3.Distance(StartPosition, transform.position) > _data.range) Pool.ReturnObject(this);

            ContactFilter2D filter = new();
            filter.NoFilter();
            filter.layerMask = LayerMask;
            filter.useLayerMask = true;

            var hits = Physics2D.OverlapBox(spriteRenderer.bounds.center, spriteRenderer.bounds.size, 0f, filter,
                _buffer);

            if (hits <= 0) return;

            foreach (var hit in _buffer)
            {
                if (!hit.TryGetComponent(out IDamageable damageable)) continue;

                damageable.TakeDamage(_data.damage);
                Pool.ReturnObject(this);
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
#endif

        public ObjectPool<Bullet> Pool { get; set; }

        public void SetData(WeaponData data)
        {
            _data = data;
            spriteRenderer.sprite = data.bulletSprite;
            _translateDirection = _data.spriteOrientation.GetTranslateDirection();
        }
    }
}