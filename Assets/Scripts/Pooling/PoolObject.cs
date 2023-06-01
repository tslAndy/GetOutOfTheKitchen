using UnityEngine;
using System;

namespace Pooling
{
    public class PoolObject<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private ScriptableTag tagScriptable;
        public ScriptableTag TagScriptable { get => tagScriptable; }

        protected Action<T> DestroyAction;
        public void SetDestroyAction(Action<T> destroyAction) => DestroyAction = destroyAction;
    }
}


