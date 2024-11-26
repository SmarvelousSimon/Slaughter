using slaughter.de.Actors.Player;
using UnityEngine;
using UnityEngine.UI;

namespace slaughter.de.UI
{
    [RequireComponent(typeof(Image))]
    public class HealthBar : MonoBehaviour
    {
        [SerializeReference] [HideInInspector] private Image healthBarImage;

        private PlayerController _player;

        private void OnDestroy()
        {
            if (_player == null) return;

            _player.OnPlayerHealthChanged -= UpdateHealthBar;
            _player.OnPlayerMaxHealthChanged -= UpdateHealthBar;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            healthBarImage = GetComponent<Image>();
        }
#endif

        public void RegisterPlayer(PlayerController player)
        {
            _player = player;
            _player.OnPlayerHealthChanged += UpdateHealthBar;
            _player.OnPlayerMaxHealthChanged += UpdateHealthBar;
        }

        private void UpdateHealthBar(float _)
        {
            var healthPercent = _player.Health / _player.MaxHealth;
            healthBarImage.rectTransform.sizeDelta =
                new Vector2(healthPercent * 200, healthBarImage.rectTransform.sizeDelta.y);
        }
    }
}