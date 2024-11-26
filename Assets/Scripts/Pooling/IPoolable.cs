using UnityEngine;

namespace slaughter.de.Pooling
{
    public interface IPoolable<T> where T : MonoBehaviour
    {
        public ObjectPool<T> Pool { get; set; }
    }
}