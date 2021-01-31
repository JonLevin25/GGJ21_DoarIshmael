using System.Collections;
using UnityEngine;
using Utils.ScriptableObjects.Audio;

public class PackageSpawner : MonoBehaviour
{
    // trigger shows 
    [SerializeField] private Transform spawnPoint;
    [MinMaxRange(0.2f, 10f)]
    [SerializeField] private RangedFloat spawnRateRange;
    [SerializeField] private bool spawnIfAlreadyPackage;
    [SerializeField] private PackageBox[] boxPrefabs;
    [Space]
    [SerializeField] private GameObject testDirection;
    
    private PackageBox _currPackage;
    private GameObject _currPackageGameObject;

    private void OnEnable() => StartCoroutine(SpawnRoutine());

    private void OnDisable() => StopAllCoroutines();

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRateRange.Random());
            if (!spawnIfAlreadyPackage && _currPackage) continue;
            var box = Instantiate(boxPrefabs.RandomItem(), spawnPoint, false);
        }
        // ReSharper disable once IteratorNeverReturns
    }
    //
    // /// <summary>
    // /// If has box - will fling
    // /// </summary>
    // public void TryFling(Vector3 force)
    // {
    //     if (testDirection) testDirection.transform.rotation = Quaternion.FromToRotation(Vector3.forward, force);
    //     if (!_currPackage) return;
    //     _currPackage.Fling(force);
    // }

    private void OnTriggerEnter(Collider other)
    {
        if (_currPackage != null) return;
        if (!other.CompareTag(Consts.Tag_Package)) return;
        
        var package = other.GetComponentInParent<PackageBox>();
        if (package == null) return;

        _currPackage = package;
        _currPackageGameObject = other.gameObject;
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (_currPackage && _currPackageGameObject == other.gameObject)
        {
            _currPackage = null;
            _currPackageGameObject = null;
        }
    }
}
