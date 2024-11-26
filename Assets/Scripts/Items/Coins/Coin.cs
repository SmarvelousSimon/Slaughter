using System.Threading;
using slaughter.de.Actors;
using slaughter.de.ExtensionMethods;
using slaughter.de.Pooling;
using UnityEngine;

namespace slaughter.de.Items.Coins
{
    [RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
    public class Coin : MonoBehaviour, IPoolable<Coin>
    {
        [SerializeReference] [HideInInspector] private SpriteRenderer spriteRenderer;
        [SerializeReference] [HideInInspector] private BoxCollider2D boxCollider;

        private CancellationTokenSource _tokenSource;
        public int Value { get; set; }
        public float TravelTime { get; set; }

        public Sprite Sprite
        {
            set => spriteRenderer.sprite = value;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            boxCollider = GetComponent<BoxCollider2D>();
        }
#endif

        public ObjectPool<Coin> Pool { get; set; }

        public void Collect(ICoinCollector collector)
        {
            _tokenSource = new CancellationTokenSource();
            boxCollider.enabled = false;
            TravelToCollector(collector, _tokenSource.Token).Forget();
        }

        private async Awaitable TravelToCollector(ICoinCollector collector, CancellationToken token)
        {
            float time = 0;
            var startPosition = transform.position;
            while (time <= TravelTime && !token.IsCancellationRequested)
            {
                transform.position = Vector3.Slerp(startPosition, collector.Transform.position, time / TravelTime);
                time += Time.deltaTime;
                await Awaitable.NextFrameAsync(token);
            }

            collector.CollectCoin(Value);
            boxCollider.enabled = true;
            ReturnToPool();
        }

        private void ReturnToPool()
        {
            _tokenSource?.Cancel();
            Pool.ReturnObject(this);
        }
    }
}