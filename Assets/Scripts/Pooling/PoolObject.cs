using UnityEngine;
using System;

namespace Pooling
{
    public abstract  class PoolObject<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private ScriptableTag tagScriptable;
        public ScriptableTag TagScriptable => tagScriptable;

        protected Action<T> DestroyAction;
        public void SetDestroyAction(Action<T> destroyAction) => DestroyAction = destroyAction;

        public abstract void OnPoolDestroy();
    }
}


