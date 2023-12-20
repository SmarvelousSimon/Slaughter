using UnityEngine;
namespace slaughter.de.Pooling
{
    public class EnemyPoolManager : MonoBehaviour
    {
        public GameObject prefab;


        // Definiere Pools für verschiedene Waffentypen
        ObjectPoolBase _poolBase;
        public static EnemyPoolManager Instance { get; private set; }
        int poolsize { get; set; }

        void Awake()
        {
            Instance = this;
            _poolBase = new ObjectPoolBase(prefab, 1); // Initialisiere den Shovel Pool
        }

        public GameObject Get()
        {
            return _poolBase.Get();
        }

        public void Return(GameObject gameObject)
        {
            _poolBase.Return(gameObject);
        }
        
        public void ResetPool()
        {
            Debug.Log("Resetting Enemy Pool");

            foreach (var obj in _poolBase.GetAllActiveObjects())
            {
                obj.SetActive(false);
                _poolBase.Return(obj);
            }
        }


        // Weitere Methoden für andere Waffentypen...
    }
}
