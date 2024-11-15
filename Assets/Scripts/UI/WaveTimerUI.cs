using slaughter.de.Managers;
using slaughter.de.State;
using TMPro;
using UnityEngine;

namespace slaughter.de.UI
{
    public class WaveTimerUI : MonoBehaviour
    {
        private TextMeshProUGUI _timerText;

        private void Start()
        {
            _timerText = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void Update()
        {
            if (StateManager.Instance.GetCurrentStateType() == typeof(WaveState))
                _timerText.text = WaveManager.Instance.GetWaveTimer().ToString("F2");
            else
                _timerText.text = "";
        }
    }
}