using UnityEngine;

public class LevelSwipeZone : MonoBehaviour
{
    [SerializeField] private float _flingXZPower;
    
    [SerializeField] private float _baseFlingArc;
    [SerializeField] private float _flingArcSpeedMult;
    
    public Vector3 GetFlingForce(Vector2 viewPortDelta, float time)
    {
        var force = new Vector3(viewPortDelta.x, 0, viewPortDelta.y);

        var viewportSpeed = viewPortDelta.magnitude / time;
        var arc = _baseFlingArc + _flingArcSpeedMult * viewportSpeed; // this number is magic
        return force * _flingXZPower + arc * Vector3.up;
    }
}