using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace Utilities
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private Queue<T> pool = new Queue<T>();
        private HashSet<T> activeObjects = new HashSet<T>();
        private T prefab;
        private Transform parent;

        public ObjectPool(T prefab, int initialCount, Transform parent = null)
        {
            this.prefab = prefab;
            this.parent = parent;

            for (int i = 0; i < initialCount; i++)
            {
                T obj = GameObject.Instantiate(prefab, parent);
                obj.gameObject.SetActive(false);
                pool.Enqueue(obj);
            }
        }
        public T Get()
        {
            Debug.Log("TotalCount is "+ TotalCount);
            Debug.Log(" activeObjects.Count is " + activeObjects.Count);
            Debug.Log("pool.Count is " + pool.Count);
            T obj;

            if (pool.Count > 0)
            {
                obj = pool.Dequeue();
            }
            else
            {
                obj = GameObject.Instantiate(prefab, parent);
            }

            obj.gameObject.SetActive(true);
            activeObjects.Add(obj);
            return obj;
        }

        public void ReturnToPool(T obj)
        {
            obj.gameObject.SetActive(false);
            activeObjects.Remove(obj);
            pool.Enqueue(obj);
            Debug.Log("Returned object to pool is " + pool.Count);
        }

        public int TotalCount => pool.Count + activeObjects.Count;
    }
}
