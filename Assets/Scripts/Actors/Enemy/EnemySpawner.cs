using slaughter.de.Actors.Player;
using slaughter.de.Core;
using slaughter.de.Items.Coins;
using slaughter.de.Items.Weapons;
using slaughter.de.Pooling;
using UnityEngine;

namespace slaughter.de.Actors.Enemy
{
    public class EnemySpawner
    {
        private readonly ObjectPool<Bullet> _bulletPool;
        private readonly CoinSpawner _coinSpawner;
        private readonly LayerMask _layer;
        private readonly PlayerController _player;

        private readonly ObjectPool<Enemy> _pool;

        public EnemySpawner(GameSettings settings, ObjectPool<Bullet> bulletPool, Spawner spawner,
            PlayerController player)
        {
            _pool = new ObjectPool<Enemy>(settings.enemyPrefab, spawner);
            _coinSpawner = new CoinSpawner(settings, spawner);
            _bulletPool = bulletPool;
            _layer = settings.playerLayer;
            _player = player;
        }

        public void SpawnEnemy(Vector3 position, EnemyData data)
        {
            var enemy = _pool.SpawnObject(position);
            enemy.CoinSpawner = _coinSpawner;
            enemy.BulletPool = _bulletPool;
            enemy.Player = _player;
            enemy.SetData(data, _layer);
        }

        public async Awaitable WaitForEnemyReturn()
        {
            await _pool.WaitForObjectsInactive();
        }

        public void Despawn()
        {
            _pool.ReturnAll();
        }
    }
}