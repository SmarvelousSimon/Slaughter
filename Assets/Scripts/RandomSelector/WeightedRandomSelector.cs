using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace slaughter.de.RandomSelector
{
    public class WeightedRandomSelector<T> where T : IWeightedObject
    {
        private const float RarityMultiplier = 0.5f;

        private readonly float _maxBaseWeight;

        private readonly List<T> _objects;

        public WeightedRandomSelector(List<T> objects)
        {
            _objects = objects;
            _maxBaseWeight = _objects.Max(x => x.BaseWeight);
            _objects.ForEach(x => x.Weight = x.BaseWeight);
            Normalize();
        }

        public T ChoseRandom()
        {
            var random = Random.value;

            var cumulativeWeight = 0f;
            foreach (var item in _objects)
            {
                cumulativeWeight += item.Weight;
                if (random <= cumulativeWeight)
                    return item;
            }

            return _objects[^1];
        }


        public T ChoseRandom(float enemyStrength)
        {
            _objects.ForEach(x =>
                x.Weight = _maxBaseWeight / (x.BaseWeight + (enemyStrength * RarityMultiplier)));
            Normalize();
            return ChoseRandom();
        }

        private void Normalize()
        {
            var totalWeight = _objects.Sum(x => x.Weight);
            _objects.ForEach(x => x.Weight /= totalWeight);
        }
    }
}