using UnityEngine;

public class PackageBox : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    
    public void Fling(Vector3 force)
    {
        _rb.AddForce(force, ForceMode.Impulse);
    }
}