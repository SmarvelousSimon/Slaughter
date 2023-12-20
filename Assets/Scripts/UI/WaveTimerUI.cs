using slaughter.de.Managers;
using TMPro;
using UnityEngine;
namespace slaughter.de.UI
{
    public class WaveTimerUI : MonoBehaviour
    {
        TextMeshProUGUI _timerText;

        void Start()
        {
            _timerText = GetComponentInChildren<TextMeshProUGUI>();
        }

        void Update()
        {
            if (GameManager.Instance.GetCurrentStateType() == typeof(WaveState))
            {
                _timerText.text = WaveManager.Instance.GetWaveTimer().ToString("F2");
            }
            else
            {
                _timerText.text = "";
            }
        }
    }
}
