using System;
using UnityEngine;

public class PackageBox : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    private static PackageBox _highlighted;
    private LevelSwipeZone _levelSwipeZone;

    private void OnCollisionEnter(Collision other)
    {
        var target = other.gameObject.GetComponent<PackageTarget>();
        if (target == null) return;
        OnReachedCustomer(target);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(Consts.Tag_LevelSwipeZone)) return;
        _levelSwipeZone = other.GetComponent<LevelSwipeZone>();
        if (!_levelSwipeZone) Debug.LogError("No LevelZone component on swipeZone tag object!");
    }

    public void OnSwipe(Vector3 normalizedDelta, float time)
    {
        var force = _levelSwipeZone.GetFlingForce(normalizedDelta, time);
        Fling(force);
    }
    
    public void Fling(Vector3 force)
    {
        Debug.Log($"Fling({force})");
        _rb.AddForce(force, ForceMode.Impulse);
        Highlight(this);
    }

    private static void Highlight(PackageBox packageBox)
    {
        if (_highlighted) SetColor(_highlighted, Color.white);
        SetColor(packageBox, Color.white);
        _highlighted = packageBox;
    }

    private static void SetColor(PackageBox package, Color color)
    {
        package.GetComponent<Renderer>().material.color = color;
    }

    private void OnReachedCustomer(PackageTarget target)
    {
        Debug.Log("Cusomer!");
        target.OnPackageHit(this);
        Destroy(gameObject);
    }
}
