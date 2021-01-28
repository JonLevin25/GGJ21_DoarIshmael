using System.Collections.Generic;
using UnityEngine;

namespace Utils.ScriptableObjects.Bus
{
    public abstract class RuntimeSet<T> : ScriptableObject
    {
        protected readonly HashSet<T> Items = new HashSet<T>();

        public void Add(T thing)
        {
            Items.Add(thing);
        }

        public void Remove(T thing)
        {
            Items.Remove(thing);
        }
    }
}