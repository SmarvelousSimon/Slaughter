using slaughter.de.Core;
using slaughter.de.Pooling;
using slaughter.de.RandomSelector;
using UnityEngine;

namespace slaughter.de.Items.Coins
{
    public class CoinSpawner
    {
        private readonly ObjectPool<Coin> _pool;
        private readonly WeightedRandomSelector<CoinData> _randomSelector;
        private readonly float _travelTime;

        public CoinSpawner(GameSettings settings, Spawner spawner)
        {
            _pool = new ObjectPool<Coin>(settings.coinPrefab, spawner);
            _randomSelector = new WeightedRandomSelector<CoinData>(settings.coins);
            _travelTime = settings.coinTravelTime;
        }

        public void SpawnCoin(Vector3 position, float enemyStrength)
        {
            var coin = _pool.SpawnObject(position);
            var data = _randomSelector.ChoseRandom(enemyStrength);
            coin.TravelTime = _travelTime;
            coin.Sprite = data.sprite;
            coin.Value = data.value;
        }
    }
}