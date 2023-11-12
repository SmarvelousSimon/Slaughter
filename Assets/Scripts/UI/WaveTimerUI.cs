using slaughter.de.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace slaughter.de.UI
{
    public class WaveTimerUI : MonoBehaviour
    {
        TextMeshProUGUI timerText;
        GameManager gameManager;

        void Start()
        {
            if (timerText == null)
            {
                timerText = GetComponentInChildren<TextMeshProUGUI>();
            }
            gameManager = FindObjectOfType<GameManager>(); // Achten Sie darauf, dass nur eine Instanz von GameManager existiert
        }

        void Update()
        {
            if (GameManager.Instance == null)
            {
                Debug.LogError("GameManager instance is null.");
                return;
            }


            if (gameManager.currentState == GameState.WaveInProgress)
            {
                timerText.text = "Time Left: " + gameManager.GetWaveTimer().ToString("F2");
            }
            else
            {
                timerText.text = "Nix"; // Text ausblenden, wenn keine Welle aktiv ist
            }
        }
    }
}
