using slaughter.de.Managers;
using UnityEngine;
namespace slaughter.de.Pooling
{
    public class CoinPoolManager : MonoBehaviour
    {
        public GameObject coinPrefab;
        public Sprite[] coinSprites; // Array von Sprites für jede Münzart

        private ObjectPoolBase coinPool;
        public static CoinPoolManager Instance { get; private set; }

        void Awake()
        {
            Instance = this;
            coinPool = new ObjectPoolBase(coinPrefab, 10); // Angenommene Poolgröße 10
        }

        public GameObject GetCoin(CoinType type)
        {
            GameObject coin = coinPool.Get();
            SpriteRenderer spriteRenderer = coin.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null && (int)type < coinSprites.Length)
            {
                spriteRenderer.sprite = coinSprites[(int)type]; // Setze das Sprite basierend auf dem Münztyp
            }
            return coin;
        }

        public void ReturnCoin(GameObject coin)
        {
            coinPool.Return(coin);
        }
    }
}
