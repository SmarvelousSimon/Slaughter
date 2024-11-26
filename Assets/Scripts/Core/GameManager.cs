using System.Collections.Generic;
using System.Threading;
using slaughter.de.Actors.Player;
using slaughter.de.CameraScripts;
using slaughter.de.ExtensionMethods;
using slaughter.de.Items.Weapons;
using slaughter.de.Pooling;
using slaughter.de.Shop;
using slaughter.de.UI;
using slaughter.de.Waves;
using UnityEditor;
using UnityEngine;

namespace slaughter.de.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeReference] private CameraScript cameraScript;
        [SerializeReference] private PlayerController playerController;
        [SerializeReference] private ConfirmationMenu gameOverMenu;
        [SerializeReference] private ConfirmationMenu winGameMenu;
        [SerializeReference] private ShopMenu shopMenu;
        [SerializeReference] private HealthBar healthBar;
        [SerializeReference] private CoinDisplay coinDisplay;
        [SerializeReference] private Spawner spawner;
        [SerializeReference] private Transform playerSpawnLocation;
        [SerializeReference] private GameSettings settings;
        [SerializeReference] private WaveTimer waveTimer;

        private readonly Queue<WaveData> _waveQueue = new();

        private ObjectPool<Bullet> _bulletPool;
        private bool _isPaused;
        private Awaitable _mainLoop;
        private ShopManager _shopManager;

        private CancellationTokenSource _tokenSource;

        private WaveManager _waveManager;


        private void Awake()
        {
            _bulletPool = new ObjectPool<Bullet>(settings.bulletPrefab, spawner);
            _waveManager = new WaveManager(settings, _bulletPool, spawner, playerController);
            _shopManager = new ShopManager(playerController, settings.weapons, settings.startWeapon);
        }

        private void Start()
        {
            cameraScript.RegisterPlayer(playerController);
            healthBar.RegisterPlayer(playerController);

            playerController.BulletPool = _bulletPool;
            playerController.GameSettings = settings;

            gameOverMenu.OnQuit += QuitGame;
            winGameMenu.OnQuit += QuitGame;

            _waveManager.OnWaveStarted += waveTimer.StartTimer;
            _waveManager.OnWaveCompleted += waveTimer.HideTimer;
            
            _shopManager.OnCoinsUpdated += coinDisplay.HandleCoinsUpdated;

            shopMenu.RegisterManager(_shopManager);
            ResetGame();
        }

        private void ResetGame()
        {
            playerController.ResetHealth();
            _shopManager.Reset();

            _waveQueue.Clear();
            foreach (var wave in settings.waves) _waveQueue.Enqueue(wave);

            _shopManager.Reset();

            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
            _tokenSource = new CancellationTokenSource();


            playerController.enabled = false;
            MainLoop(_tokenSource.Token).Forget();
        }

        private void QuitGame()
        {
            _tokenSource?.Cancel();
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }

        private async Awaitable MainLoop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var currentWave = _waveQueue.Dequeue();
                _shopManager.CurrentWave++;

                playerController.SetPosition(playerSpawnLocation.position);
                playerController.ResetHealth();
                
                playerController.enabled = true;
                await _waveManager.StartWave(currentWave, token);
                playerController.enabled = false;
                
                if (playerController.IsDead)
                {
                    await gameOverMenu.Show(token);
                    ResetGame();
                }
                else if (_waveQueue.Count == 0)
                {
                    await winGameMenu.Show(token);
                    ResetGame();
                }

                await shopMenu.Show(token);
            }
        }
    }
}