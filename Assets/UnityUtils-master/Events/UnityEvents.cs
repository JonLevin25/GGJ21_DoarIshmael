using System;
using UnityEngine.Events;

namespace Utils.Events
{
    [Serializable]
    public class UnityEventAction : UnityEvent {}
    
    [Serializable]
    public class UnityIntEvent : UnityEvent<int> {}
}