using UnityEngine;

namespace slaughter.de.Pooling
{
    public class Spawner : MonoBehaviour
    {
        public T SpawnObject<T>(T prefab, Vector3 position) where T : MonoBehaviour
        {
            return Instantiate(prefab, position, prefab.transform.rotation);
        }
    }
}