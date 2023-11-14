using slaughter.de.Managers;
using TMPro;
using UnityEngine;
namespace slaughter.de.UI
{
    public class WaveTimerUI : MonoBehaviour
    {
        TextMeshProUGUI timerText;
        WaveManager _waveManager;

        void Start()
        {
            timerText = GetComponentInChildren<TextMeshProUGUI>();
            _waveManager = WaveManager.Instance;
        }

        void Update()
        {
            if (GameManager.Instance.GetCurrentStateType() == typeof(WaveState))
            {
                timerText.text = "Time Left: " + _waveManager.GetWaveTimer().ToString("F2");
            }
            else
            {
                timerText.text = "";
            }
        }
    }
}
