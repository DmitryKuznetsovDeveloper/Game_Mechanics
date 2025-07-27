using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool
{
    public interface IPool<T>
    {
        T Get();
        void Return(T item);
    }

    public class ObjectPool<T> : IPool<T> where T : MonoBehaviour
    {
        private readonly Queue<T> _pool = new Queue<T>();
        private readonly T _prefab;
        private readonly Transform _container;

        public ObjectPool(T prefab, int initialSize, Transform container)
        {
            _prefab = prefab;
            _container = container;
            for (int i = 0; i < initialSize; i++)
            {
                T obj = Object.Instantiate(_prefab, _container);
                obj.gameObject.SetActive(false);
                _pool.Enqueue(obj);
            }
        }

        public T Get()
        {
            if (_pool.Count > 0)
            {
                T obj = _pool.Dequeue();
                obj.gameObject.SetActive(true);
                return obj;
            }
            T newObj = Object.Instantiate(_prefab, _container);
            return newObj;
        }

        public void Return(T item)
        {
            item.gameObject.SetActive(false);
            item.transform.SetParent(_container);
            _pool.Enqueue(item);
        }
    }
}