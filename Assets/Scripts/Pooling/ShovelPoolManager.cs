using UnityEngine;

namespace slaughter.de.Pooling
{
    public class ShovelPoolManager : MonoBehaviour
    {
        public GameObject prefab;


        // Definiere Pools für verschiedene Waffentypen
        private ObjectPoolBase _poolBase;
        public static ShovelPoolManager Instance { get; private set; }
        private int poolsize { get; set; }

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