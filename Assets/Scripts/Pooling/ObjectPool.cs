using System;
using System.Collections.Generic;
using UnityEngine;

namespace slaughter.de.Pooling
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private event Action OnAllObjectsInactive;
        
        private readonly HashSet<T> _activeObjects = new();

        private readonly Queue<T> _pool = new();

        private readonly T _prefab;
        private readonly Spawner _spawner;

        public ObjectPool(T prefab, Spawner spawner, int minSize = 0)
        {
            _spawner = spawner;
            _prefab = prefab;

            if (minSize <= 0) return;

            for (var i = 0; i < minSize; i++)
            {
                var poolObject = _spawner.SpawnObject(_prefab, Vector3.zero);
                poolObject.gameObject.SetActive(false);
                _pool.Enqueue(poolObject);
            }
        }

        public T SpawnObject(Vector3 position)
        {
            T spawnedObject;
            if (_pool.Count == 0)
            {
                spawnedObject = _spawner.SpawnObject(_prefab, position);
                if (spawnedObject is IPoolable<T> poolable) poolable.Pool = this;

                _activeObjects.Add(spawnedObject);
                return spawnedObject;
            }

            spawnedObject = _pool.Dequeue();
            spawnedObject.transform.position = position;
            _activeObjects.Add(spawnedObject);
            spawnedObject.gameObject.SetActive(true);

            return spawnedObject;
        }

        public void ReturnObject(T spawnedObject)
        {
            _activeObjects.Remove(spawnedObject);
            _pool.Enqueue(spawnedObject);
            spawnedObject.gameObject.SetActive(false);
            if(_activeObjects.Count == 0) OnAllObjectsInactive?.Invoke();
        }

        public void ReturnAll()
        {
            foreach (var spawnedObject in _activeObjects)
            {
                _pool.Enqueue(spawnedObject);
                spawnedObject.gameObject.SetActive(false);
            }
            _activeObjects.Clear();
        }

        public async Awaitable WaitForObjectsInactive()
        {
            if (_activeObjects.Count == 0) return;
            
            AwaitableCompletionSource completionSource = new AwaitableCompletionSource();
            OnAllObjectsInactive += completionSource.SetResult;
            await completionSource.Awaitable;
            OnAllObjectsInactive -= completionSource.SetResult;
        }
    }
}