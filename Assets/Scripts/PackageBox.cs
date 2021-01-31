using System;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

public class PackageBox : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    private static PackageBox _highlighted;
    private LevelSwipeZone _levelSwipeZone;

    [SerializeField] private GameObject[] views;
    [SerializeField] private GameObject currentView;
    [SerializeField] private float pacakgeDelayTime;    

    void OnEnable()
    {
        currentView.SetActive(false);
        var view = views.Choice();
        view.SetActive(true);
        view.transform.localScale = new Vector3(Random.Range(2f, 4f), Random.Range(2f, 4f), Random.Range(2f, 4f));
        currentView = view;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        var target = other.gameObject.GetComponent<PackageTarget>();
        if (target == null) return;
        OnReachedCustomer(target);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Consts.Tag_LevelSwipeZone))
        {
            _levelSwipeZone = other.GetComponent<LevelSwipeZone>();
            if (!_levelSwipeZone) Debug.LogError("No LevelZone component on swipeZone tag object!");
        } else if (other.CompareTag(Consts.Tag_PackageDestroya))
        {
            Destroy(gameObject, pacakgeDelayTime);
        }
    }

    public void OnSwipe(Vector3 normalizedDelta, float time)
    {
        var force = _levelSwipeZone.GetFlingForce(normalizedDelta, time);
        Fling(force);
    }
    
    public void Fling(Vector3 force)
    {
        Debug.Log($"Fling({force})");
        _rb.velocity = Vector3.zero;
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
