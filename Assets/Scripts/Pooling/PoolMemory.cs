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
            poolObjectPrefab.SetDestroyAction(DestroyPoolObject);

            if (!_pools.TryGetValue(poolObjectPrefab.TagScriptable, out ObjectPool<T> objectPool))
            {
                objectPool = new ObjectPool<T>(
                    () => Object.Instantiate(poolObjectPrefab),
                    poolObj => poolObj.gameObject.SetActive(true),
                    poolObj => poolObj.gameObject.SetActive(false),
                    poolObj => Object.Destroy(poolObj.gameObject),
                    false, 100, 200
                );
                _pools.Add(poolObjectPrefab.TagScriptable, objectPool);

                List<T> spawnedObjects = new List<T>();
                _spawnedObjectsPool.Add(poolObjectPrefab.TagScriptable, spawnedObjects);
            }

            T poolObject = objectPool.Get();
            _spawnedObjectsPool[poolObject.TagScriptable].Add(poolObject);
            return poolObject;
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
