using System;
using System.Threading;
using slaughter.de.Actors.Enemy;
using slaughter.de.Actors.Player;
using slaughter.de.Core;
using slaughter.de.ExtensionMethods;
using slaughter.de.Items.Weapons;
using slaughter.de.Pooling;
using slaughter.de.RandomSelector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace slaughter.de.Waves
{
    public class WaveManager
    {
        public event Action<float> OnWaveStarted;
        public event Action OnWaveCompleted;
        
        
        private readonly ObjectPool<Bullet> _bulletPool;
        private readonly PlayerController _player;
        private readonly EnemySpawner _spawner;

        private WaveData _currentWave;

        private WeightedRandomSelector<EnemyData> _enemySelector;
        private CancellationTokenSource _tokenSource;
        private IDisposable _tokenSourceHandle;

        public WaveManager(GameSettings settings, ObjectPool<Bullet> bulletPool, Spawner spawner,
            PlayerController player)
        {
            _player = player;
            _player.OnPlayerDeath += OnPlayerDeath;

            _spawner = new EnemySpawner(settings, bulletPool, spawner, player);
        }

        public async Awaitable StartWave(WaveData waveData, CancellationToken token)
        {
            _tokenSource = CancellationTokenSource.CreateLinkedTokenSource(token);
            _currentWave = waveData;
            _enemySelector = new WeightedRandomSelector<EnemyData>(_currentWave.enemyData);
            
            SpawnGroupLoop(_tokenSource.Token).Forget();
            OnWaveStarted?.Invoke(_currentWave.waveDuration);
            await Awaitable.WaitForSecondsAsync(_currentWave.waveDuration, _tokenSource.Token).SuppressCancelThrow();
            
            _tokenSource.Cancel();
            _tokenSource.Dispose();

            await _spawner.WaitForEnemyReturn();
            await Awaitable.WaitForSecondsAsync(3f, token).SuppressCancelThrow();
            OnWaveCompleted?.Invoke();
        }

        private async Awaitable SpawnGroupLoop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                SpawnGroup();
                await Awaitable.WaitForSecondsAsync(_currentWave.spawnRate, token).SuppressCancelThrow();
            }
        }

        private void OnPlayerDeath()
        {
            _tokenSource?.Cancel();
            _spawner.Despawn();
        }

        private void SpawnGroup()
        {
            for (var i = 0; i < _currentWave.groupSize; i++)
            {
                var position = GetRandomSpawnPosition();
                var data = _enemySelector.ChoseRandom();
                _spawner.SpawnEnemy(position, data);
            }
        }

        private Vector3 GetRandomSpawnPosition()
        {
            // Implementiere Logik, um eine zufällige Position außerhalb des Sichtfeldes zu wählen
            // Beispiel: Zufällige Position um den Spieler herum
            var distance = 10f; // Außerhalb des Sichtfeldes
            var randomDirection = Random.insideUnitCircle.normalized;
            var spawnPos = _player.transform.position + new Vector3(randomDirection.x, randomDirection.y, 0) * distance;
            return spawnPos;
        }
    }
}