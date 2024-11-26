using System;
using System.Collections.Generic;
using System.Linq;
using slaughter.de.Actors.Player;
using slaughter.de.Items.Weapons;

namespace slaughter.de.Shop
{
    public class ShopManager
    {
        private readonly PlayerController _player;
        private readonly List<WeaponData> _weapons;
        private readonly WeaponData _startWeapon;

        public WeaponData CurrentWeapon => _player.CurrentWeapon;

        public ShopManager(PlayerController player, List<WeaponData> weapons, WeaponData startWeapon)
        {
            _player = player;
            _weapons = weapons;
            _startWeapon = startWeapon;

            _player.OnCoinCollected += HandleCoinCollected;
        }

        public int Coins { get; private set; }

        public int CurrentWave { get; set; }

        public event Action<int> OnCoinsUpdated;

        public void BuyWeapon(WeaponData weapon)
        {
            Coins -= weapon.cost;
            OnCoinsUpdated?.Invoke(Coins);
            _player.Equip(weapon);
        }

        public List<WeaponData> GetUnlockedWeapons()
        {
            return _weapons.Where(x => x.wave <= CurrentWave && x.cost <= Coins && x != CurrentWeapon).ToList();
        }

        public List<WeaponData> GetLockedWeapons()
        {
            return _weapons.Where(x => x.wave <= CurrentWave && x.cost > Coins && x != CurrentWeapon).ToList();
        }

        public void Reset()
        {
            Coins = 0;
            _player.Equip(_startWeapon);
        }

        private void HandleCoinCollected(int value)
        {
            Coins += value;
            OnCoinsUpdated?.Invoke(Coins);
        }
    }
}