using UnityEngine;
namespace slaughter.de.Pooling
{
    public class EnemyPoolManager: MonoBehaviour
    {
        public static EnemyPoolManager Instance { get; private set; }
        public GameObject prefab;
        private int poolsize { get; set; }


        // Definiere Pools für verschiedene Waffentypen
        private ObjectPoolBase _poolBase;

        private void Awake()
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

        // Weitere Methoden für andere Waffentypen...
    }
}
