using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


namespace Pooling
{
    public class PoolMemory<T> where T : PoolObject<T>
    {
        private readonly Dictionary<ScriptableTag, ObjectPool<T>> _pools;

        public PoolMemory()
        {
            _pools = new Dictionary<ScriptableTag, ObjectPool<T>>();
        }

        public T GetPoolObject(T poolObjectPrefab)
        {
            if (!_pools.TryGetValue(poolObjectPrefab.TagScriptable, out ObjectPool<T> objectPool))
            {
                objectPool = new ObjectPool<T>(
                    () =>
                    {
                        T poolObj = Object.Instantiate(poolObjectPrefab);
                        poolObj.SetDestroyAction(DestroyPoolObject);
                        return poolObj;
                    },
                    poolObj => poolObj.gameObject.SetActive(true),
                    poolObj => poolObj.gameObject.SetActive(false),
                    poolObj => Object.Destroy(poolObj.gameObject),
                    false, 100, 200
                );
                _pools.Add(poolObjectPrefab.TagScriptable, objectPool);
            }

            T poolObject = objectPool.Get();

            return poolObject;
        }

        private void DestroyPoolObject(T poolObject)
        {
            _pools[poolObject.TagScriptable].Release(poolObject);
        }
    }
}

