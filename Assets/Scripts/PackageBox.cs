using System;
using UnityEngine;

public class PackageBox : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    private static PackageBox _highlighted;

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

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag(Consts.Tag_Customer)) return;
        OnReachedCustomer();
    }

    private void OnReachedCustomer()
    {
        Debug.Log("Cusomer!");
        Destroy(gameObject);
    }
}