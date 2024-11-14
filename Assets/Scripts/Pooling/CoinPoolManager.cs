using slaughter.de.Coin;
using slaughter.de.Managers;
using UnityEngine;

namespace slaughter.de.Pooling
{
    public class CoinPoolManager : MonoBehaviour
    {
        public GameObject coinPrefab;
        public Sprite[] coinSprites; // Array von Sprites für jede Münzart

        private ObjectPoolBase _coinPool;
        public static CoinPoolManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            _coinPool = new ObjectPoolBase(coinPrefab, 10); // Angenommene Poolgröße 10
        }

        public GameObject GetCoin(CoinType type)
        {
            var coin = _coinPool.Get();
            var spriteRenderer = coin.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null && (int)type < coinSprites.Length)
                spriteRenderer.sprite = coinSprites[(int)type]; // Setze das Sprite basierend auf dem Münztyp
            return coin;
        }

        public void ReturnCoin(GameObject coin)
        {
            _coinPool.Return(coin);
        }

        public void ResetPool()
        {
            Debug.Log("Resetting Coin Pool");
            foreach (var obj in _coinPool.GetAllActiveObjects())
            {
                obj.SetActive(false);
                _coinPool.Return(obj);
            }
        }
    }
}