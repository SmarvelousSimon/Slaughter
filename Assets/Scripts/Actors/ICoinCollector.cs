using System;
using UnityEngine;

namespace slaughter.de.Actors
{
    public interface ICoinCollector
    {
        public Transform Transform { get; }
        public event Action<int> OnCoinCollected;

        public void CollectCoin(int value);
    }
}