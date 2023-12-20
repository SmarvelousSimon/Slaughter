using System.Collections.Generic;
using UnityEngine;
namespace slaughter.de.Pooling
{
    public class ObjectPoolBase
    {
        readonly Queue<GameObject> pool = new Queue<GameObject>();
        readonly GameObject prefab;
        private HashSet<GameObject> activeObjects = new HashSet<GameObject>();


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
            activeObjects.Add(obj);
            return obj;
        }

        public void Return(GameObject objectToReturn)
        {
            objectToReturn.SetActive(false);
            pool.Enqueue(objectToReturn);
            activeObjects.Remove(objectToReturn);

        }

        void AddObject()
        {
            var newObject = Object.Instantiate(prefab);
            newObject.SetActive(false);
            pool.Enqueue(newObject);
        }
        
        public List<GameObject> GetAllActiveObjects()
        {
            return new List<GameObject>(activeObjects);
        }
        
    }

}
