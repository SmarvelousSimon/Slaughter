using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace slaughter.de.Pooling
{
    public class ObjectPoolBase
    {
        private Queue<GameObject> pool = new Queue<GameObject>();
        private GameObject prefab;

        public ObjectPoolBase(GameObject prefab, int size)
        {
            this.prefab = prefab;
            for (int i = 0; i < size; i++)
            {
                AddObject();
            }
        }

        public GameObject Get()
        {
            if (pool.Count == 0)
                AddObject();

            var obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }

        public void Return(GameObject objectToReturn)
        {
            objectToReturn.SetActive(false);
            pool.Enqueue(objectToReturn);
        }

        private void AddObject()
        {
            var newObject = GameObject.Instantiate(prefab);
            newObject.SetActive(false);
            pool.Enqueue(newObject);
        }
    }

}
