using System.Collections.Generic;
using System.Threading;
using slaughter.de.Items.Weapons;
using slaughter.de.Shop;
using UnityEngine;
using UnityEngine.UI;

namespace slaughter.de.UI
{
    public class ShopMenu : ConfirmationMenu
    {
        private ShopManager _manager;

        [SerializeReference] private Transform unlockedWeaponsList;
        [SerializeReference] private Transform lockedWeaponsList;
        [SerializeReference] private Image currentWeapon;
        [SerializeReference] private ItemElement itemPrefab;
        
        public void RegisterManager(ShopManager manager)
        {
            _manager = manager;
        }

        private void ClearList()
        {
            foreach (Transform child in unlockedWeaponsList)
            {
                Destroy(child.gameObject);
            }

            foreach (Transform child in lockedWeaponsList)
            {
                Destroy(child.gameObject);
            }
        }

        private void PopulateList()
        {
            foreach (WeaponData data in _manager.GetUnlockedWeapons())
            {
                ItemElement itemElement = Instantiate(itemPrefab, unlockedWeaponsList);
                itemElement.OnClick += HandleClick;
                itemElement.SetData(data);
            }
            
            foreach (WeaponData data in _manager.GetLockedWeapons())
            {
                ItemElement itemElement = Instantiate(itemPrefab, lockedWeaponsList);
                itemElement.SetData(data);
            }

            currentWeapon.sprite = _manager.CurrentWeapon.thumbnail;

        }

        private void HandleClick(WeaponData data)
        {
            _manager.BuyWeapon(data);
            ClearList();
            PopulateList();
        }

        public override async Awaitable Show(CancellationToken token)
        {
            ClearList();
            PopulateList();
            
            await base.Show(token);
        }
        
    }
}