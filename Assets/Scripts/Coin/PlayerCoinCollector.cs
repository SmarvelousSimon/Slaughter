using System.Collections;
using System.Collections.Generic;
using slaughter.de.Pooling;
using UnityEngine;
namespace slaughter.de.Managers
{
    public class PlayerCoinCollector : MonoBehaviour
    {
        private Dictionary<CoinType, int> coinsCollected = new Dictionary<CoinType, int>();

        void OnTriggerEnter2D(Collider2D collider)
        {

            if (collider.gameObject.CompareTag("Coin"))
            {
                Coin coinComponent = collider.gameObject.GetComponent<Coin>();
                if (coinComponent != null)
                {
                    StartCoroutine(MoveCoinToPlayer(collider.gameObject, coinComponent.coinType));
                }
            }
        }

        IEnumerator MoveCoinToPlayer(GameObject coin, CoinType coinType)
        {
            float duration = 0.3f; // Zeit, die die Münze benötigt, um zum Spieler zu fliegen
            float elapsedTime = 0;
            Vector3 startPosition = coin.transform.position;
            while (elapsedTime < duration)
            {
                coin.transform.position = Vector3.Slerp(startPosition, transform.position, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            IncrementCoinCount(coinType);
            CoinPoolManager.Instance.ReturnCoin(coin); // Münze zurück in den Pool
        }

        void IncrementCoinCount(CoinType coinType)
        {
            if (!coinsCollected.ContainsKey(coinType))
            {
                coinsCollected[coinType] = 0;
            }
            coinsCollected[coinType]++;
            Debug.Log($"Collected {coinsCollected[coinType]} of {coinType}");
        }
    }
}
