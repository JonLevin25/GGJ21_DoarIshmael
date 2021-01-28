using UnityEngine;

namespace Utils
{
    public class ActionThrottle
    {
        private float _lastTrigger = 0;
        
        public ActionThrottle(float interval)
        {
            Interval = interval;
        }

        public float Interval { get; set; }

        public void Trigger()
        {
            _lastTrigger = Time.time;
        }

        public bool HasPassed()
        {
            return Time.time > _lastTrigger + Interval;
        }
        
    }
}