using TMPro;
using UnityEngine;

namespace slaughter.de.UI
{
    public class WaveTimer : MonoBehaviour
    {
        [SerializeReference] private TextMeshProUGUI timerText;

        private float _remainingTime;

        private void Update()
        {
            _remainingTime -= Time.deltaTime;
            timerText.text = _remainingTime.ToString("F2");

            if (_remainingTime > 0f) return;
            
            _remainingTime = 0f;
            timerText.text = _remainingTime.ToString("F2");
            enabled = false;
        }

        public void StartTimer(float time)
        {
            _remainingTime = time;
            timerText.text = _remainingTime.ToString("F2");
            gameObject.SetActive(true);
            enabled = true;
        }

        public void HideTimer()
        {
            gameObject.SetActive(false);
        }
    }
}