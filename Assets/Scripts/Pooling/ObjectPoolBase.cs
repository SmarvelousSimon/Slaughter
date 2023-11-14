using System.Collections.Generic;
using UnityEngine;
namespace slaughter.de.Pooling
{
    public class ObjectPoolBase
    {
        readonly Queue<GameObject> pool = new Queue<GameObject>();
        readonly GameObject prefab;

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

        void AddObject()
        {
            var newObject = Object.Instantiate(prefab);
            newObject.SetActive(false);
            pool.Enqueue(newObject);
        }
    }

}
