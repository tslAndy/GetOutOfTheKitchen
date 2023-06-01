using UnityEngine;
using System;

namespace Pooling
{
    public abstract class PoolObject<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private ScriptableTag tagScriptable;
        public ScriptableTag TagScriptable => tagScriptable;

        protected Action<T> DestroyAction;

        private void Awake()
        {
            DestroyAction = new Action<T>(_ => BeforeReturnToPool());
        }
        
        public void SetDestroyAction(Action<T> destroyAction)
        {
            DestroyAction += destroyAction;
        }

        public abstract void OnPoolDestroy();
        protected abstract void BeforeReturnToPool();
    }
}


