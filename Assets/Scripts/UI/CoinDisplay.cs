using TMPro;
using UnityEngine;

namespace slaughter.de.UI
{
    public class CoinDisplay : MonoBehaviour
    {
        [SerializeReference] private TextMeshProUGUI text;

        private void Awake()
        {
            text.text = "000";
        }

        public void HandleCoinsUpdated(int coins)
        {
            text.text = coins.ToString("D3");
        }
    }
}