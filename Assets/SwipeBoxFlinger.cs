using Lean.Touch;
using UnityEngine;

public class SwipeBoxFlinger : MonoBehaviour
{
    [SerializeField] private float _flingBasePower;
    [SerializeField] private PackageBox _boxPrefab;
    [SerializeField] private Transform _packageSpawner;
    [SerializeField] private GameObject test;
    
    // Called from scene
    public void OnLeanFinger(LeanFinger finger)
    {
        Debug.Log($"Swipe: scaledDelta: {finger.SwipeScreenDelta} Age: {finger.Age}");
        var swipeScreenDelta = finger.SwipeScreenDelta;
        var normalizedDelta = new Vector2(
            swipeScreenDelta.x / Screen.width,
            swipeScreenDelta.y / Screen.height);
        
        OnSwipe(normalizedDelta, finger.Age);
    }

    private void Fling(Vector3 force)
    {
        test.transform.rotation = Quaternion.FromToRotation(Vector3.forward, force);
        var box = Instantiate(_boxPrefab, _packageSpawner, false);
        box.Fling(force);
    }

    private void OnSwipe(Vector2 normalizedDelta, float time)
    {
        Vector3 flingForce = GetFlingForce(normalizedDelta, time);
        Fling(flingForce);
    }

    private Vector3 GetFlingForce(Vector2 viewPortDelta, float time)
    {
        var force = new Vector3(viewPortDelta.x, 0, viewPortDelta.y);
        return force * _flingBasePower;
    }
}
