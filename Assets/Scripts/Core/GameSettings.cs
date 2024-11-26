using System.Collections.Generic;
using slaughter.de.Actors.Enemy;
using slaughter.de.Items.Coins;
using slaughter.de.Items.Weapons;
using slaughter.de.Waves;
using UnityEngine;

namespace slaughter.de.Core
{
    [CreateAssetMenu(menuName = "Data/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        public Coin coinPrefab;
        public Enemy enemyPrefab;
        public Bullet bulletPrefab;

        public float coinTravelTime = 0.3f;
        public float coinCollectRadius = 2f;

        public WeaponData startWeapon;

        public List<WaveData> waves;
        public List<WeaponData> weapons;
        public List<CoinData> coins;

        public LayerMask playerLayer;
        public LayerMask enemyLayer;
        public LayerMask coinLayer;
    }
}