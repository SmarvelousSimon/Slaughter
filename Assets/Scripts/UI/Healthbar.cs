using slaughter.de.Actors.Character;
using UnityEngine;
using UnityEngine.UI;

namespace slaughter.de.UI
{
    public class HealthBar : MonoBehaviour
    {
        public PlayerController player;
        private Image healthBarImage;

        private void Start()
        {
            healthBarImage = GetComponent<Image>();
        }

        private void Update()
        {
            if (player != null)
            {
                // Berechne den prozentualen Anteil der Gesundheit
                var healthPercent = player.health / 100f;
                healthBarImage.rectTransform.sizeDelta =
                    new Vector2(healthPercent * 200, healthBarImage.rectTransform.sizeDelta.y);
            }
        }
    }
}