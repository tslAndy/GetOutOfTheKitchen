using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using System;
using Object = UnityEngine.Object;

namespace Pooling
{
    public class PoolMemory<T> where T : PoolObject<T>
    {
        private readonly Dictionary<ScriptableTag, ObjectPool<T>> _pools;
        private readonly Dictionary<ScriptableTag, List<T>> _spawnedObjectsPool;

        public PoolMemory()
        {
            _pools = new Dictionary<ScriptableTag, ObjectPool<T>>();
            _spawnedObjectsPool = new Dictionary<ScriptableTag, List<T>>();
        }

        public T GetPoolObject(T poolObjectPrefab)
        {
            ObjectPool<T> objectPool;
            if (!_pools.TryGetValue(poolObjectPrefab.TagScriptable, out objectPool))
                objectPool = InitObjectPool(poolObjectPrefab);


            T poolObject = objectPool.Get();
            _spawnedObjectsPool[poolObject.TagScriptable].Add(poolObject);
            return poolObject;
        }

        private ObjectPool<T> InitObjectPool(T poolObjectPrefab)
        {
            poolObjectPrefab.SetDestroyAction(DestroyPoolObject);

            ObjectPool<T> objectPool = new ObjectPool<T>(
                () => Object.Instantiate(poolObjectPrefab),
                poolObj => poolObj.gameObject.SetActive(true),
                poolObj => poolObj.gameObject.SetActive(false),
                poolObj => Object.Destroy(poolObj.gameObject),
                false, 100, 200
            );
            _pools.Add(poolObjectPrefab.TagScriptable, objectPool);

            List<T> spawnedObjects = new List<T>();
            _spawnedObjectsPool.Add(poolObjectPrefab.TagScriptable, spawnedObjects);

            return objectPool;
        }

        private void DestroyPoolObject(T poolObject)
        {
            poolObject.BeforeReturnToPool();
            _spawnedObjectsPool[poolObject.TagScriptable].Remove(poolObject);
            _pools[poolObject.TagScriptable].Release(poolObject);
        }

        public void ClearObjects(T poolObject)
        {
            ScriptableTag tagScriptable = poolObject.TagScriptable;

            if (_pools.ContainsKey(tagScriptable))
            {
                _pools[tagScriptable].Clear();
                _pools.Remove(tagScriptable);
            }
            if (_spawnedObjectsPool.ContainsKey(tagScriptable))
            {
                _spawnedObjectsPool[tagScriptable].ForEach(poolObj => poolObj.OnPoolDestroy());
                _spawnedObjectsPool[tagScriptable].Clear();
                _spawnedObjectsPool.Remove(tagScriptable);
            }
        }

        public void AddObjectToPool(T poolObj, bool active=true)
        {
            poolObj.SetDestroyAction(DestroyPoolObject);

            ScriptableTag scriptableTag = poolObj.TagScriptable;
            ObjectPool<T> objectPool;
            if (!_pools.TryGetValue(scriptableTag, out objectPool))
                objectPool = InitObjectPool(poolObj);

            if (active)
                _spawnedObjectsPool[poolObj.TagScriptable].Add(poolObj);
            else
                _pools[poolObj.TagScriptable].Release(poolObj);
        }

        public void ClearWholePool()
        {
            List<ScriptableTag> keys = new List<ScriptableTag>(_pools.Keys);

            for (int i = 0; i < _pools.Keys.Count; i++)
            {
                ScriptableTag scriptableTag = keys[i];
                T prefab = _pools[scriptableTag].Get();
                prefab.gameObject.SetActive(false);
                ClearObjects(prefab);
                Object.Destroy(prefab.gameObject);
            }
        }
    }
}
