using System.Collections.Generic;
using UnityEngine;

namespace ObjectPools
{
    public abstract class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private List<T> _pooledPrefabs;
        [SerializeField] private int _poolSize;

        private int _minPoolSize = 3;
        private List<T> _pooledObjects = new();
        private int _minIndex = 0;
        private int _minQuantity = 1;
        private int _currentIndex = 0;

        private void OnValidate()
        {
            if (_poolSize < _minPoolSize)
                _poolSize = _minPoolSize;
        }

        private void Awake()
        {
            InitializePool();
        }

        public T Get()
        {
            T instance = null;

            foreach (var obj in _pooledObjects)
            {
                if (!obj.gameObject.activeInHierarchy)
                {
                    instance = obj;
                    break;
                }
            }

            if (instance == null)
            {
                instance = Instantiate(CreateNewObject());
                _pooledObjects.Add(instance);
            }

            instance.gameObject.SetActive(true);
            return instance;
        }

        private void InitializePool()
        {
            for (int i = 0; i < _poolSize; i++)
            {
                T _poolobject = Instantiate(CreateNewObject());
                _poolobject.gameObject.SetActive(false);
                _pooledObjects.Add(_poolobject);
            }
        }

        private T CreateNewObject()
        {
            T instance = null;

            if (_pooledPrefabs.Count == _minQuantity)
            {
                instance = _pooledPrefabs[_minIndex];
            }
            else
            {
                if (_currentIndex >= _pooledPrefabs.Count)
                {
                    _currentIndex = _minIndex;
                }

                instance = _pooledPrefabs[_currentIndex];
                _currentIndex++;
            }

            return instance;
        }
    }
}