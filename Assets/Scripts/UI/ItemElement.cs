using System;
using slaughter.de.Items.Weapons;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace slaughter.de.UI
{
    public class ItemElement : MonoBehaviour, IPointerClickHandler
    {
        public event Action<WeaponData> OnClick;
        
        [SerializeReference] private Image thumbnail;
        [SerializeReference] private TextMeshProUGUI cost;
        
        
        private WeaponData _data;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke(_data);
        }

        public void SetData(WeaponData data)
        {
            _data = data;
            thumbnail.sprite = _data.thumbnail;
            cost.text = _data.cost.ToString();
        }
    }
}