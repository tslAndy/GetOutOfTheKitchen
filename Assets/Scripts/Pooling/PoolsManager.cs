using Enemies;
using Enemies.Hamburger;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pooling
{
    public class PoolsManager : Singleton<PoolsManager>
    {
        private Dictionary<Type, object> _pools;

        public PoolsManager()
        {
            _pools = new Dictionary<Type, object>();
        }

        public PoolMemory<T> GetPoolMemory<T>() where T : PoolObject<T>
        {
            Type tempType = typeof(T);
            if (!_pools.ContainsKey(tempType))
            {
                PoolMemory<T> newPoolMemory = new PoolMemory<T>();
                _pools.Add(tempType, newPoolMemory);
            }
            return (PoolMemory<T>) _pools[tempType];
        }

        // TODO : make PoolMemory deleting
        // TODO : automatically creat scriptables by gameobj name
        // TODO : prefabs load and unload with addressables ?
    }
}
