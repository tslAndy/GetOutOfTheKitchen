using UnityEngine;
using System;

namespace Pooling
{
    public abstract class PoolObject<T> : MonoBehaviour where T : class
    {
        [SerializeField] private ScriptableTag tagScriptable;
        public ScriptableTag TagScriptable => tagScriptable;

        protected static Action<T> DestroyAction;

        public void SetDestroyAction(Action<T> destroyAction) => DestroyAction = destroyAction;
        public abstract void OnPoolDestroy();
        public abstract void BeforeReturnToPool();
    }
}
