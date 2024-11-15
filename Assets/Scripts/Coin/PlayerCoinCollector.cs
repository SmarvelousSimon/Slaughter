using System.Collections;
using System.Collections.Generic;
using slaughter.de.Coin;
using slaughter.de.Pooling;
using UnityEngine;

namespace slaughter.de.Managers
{
    public class PlayerCoinCollector : MonoBehaviour
    {
        private Dictionary<CoinType, int> coinsCollected = new();

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("Coin"))
            {
                var coinComponent = collider.gameObject.GetComponent<Coin>();
                if (coinComponent != null)
                    StartCoroutine(MoveCoinToPlayer(collider.gameObject, coinComponent.coinType));
            }
        }

        private IEnumerator MoveCoinToPlayer(GameObject coin, CoinType coinType)
        {
            var duration = 0.3f; // Zeit, die die Münze benötigt, um zum Spieler zu fliegen
            float elapsedTime = 0;
            var startPosition = coin.transform.position;
            while (elapsedTime < duration)
            {
                coin.transform.position = Vector3.Slerp(startPosition, transform.position, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            IncrementCoinCount(coinType);
            CoinPoolManager.Instance.ReturnCoin(coin); // Münze zurück in den Pool
        }

        private void IncrementCoinCount(CoinType coinType)
        {
            if (!coinsCollected.ContainsKey(coinType)) coinsCollected[coinType] = 0;
            coinsCollected[coinType]++;
             //Debug.Log($"Collected {coinsCollected[coinType]} of {coinType}");
        }
    }
}