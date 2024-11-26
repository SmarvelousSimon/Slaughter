using slaughter.de.RandomSelector;
using UnityEngine;

namespace slaughter.de.Items.Coins
{
    [CreateAssetMenu(menuName = "Data/CoinData", fileName = "CoinData")]
    public class CoinData : ScriptableObject, IWeightedObject
    {
        public float baseWeight;

        public int value;
        public Sprite sprite;
        public float BaseWeight => baseWeight;
        public float Weight { get; set; }
    }
}