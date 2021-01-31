using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Lean.Touch;
using UnityEngine;
using Utils;
using Utils.ScriptableObjects.Audio;

public class SwipeBoxFlinger : MonoBehaviour
{
    // [SerializeField] private PackageSpawner[] _spawners;
    // [SerializeField] private RangedFloat[] _screenSplitXs;
    // [SerializeField] private float _flingXZPower;
    //
    // [SerializeField] private float _baseFlingArc;
    // [SerializeField] private float _flingArcSpeedMult;

    // private PackageSpawner[] _spawnersInScene;

    private void Awake()
    {
        // _spawnersInScene = FindObjectsOfType<PackageSpawner>();
    }

    // Called from scene
    public void OnLeanFinger(LeanFinger finger)
    {
        Debug.Log($"Swipe: scaledDelta: {finger.SwipeScreenDelta} Age: {finger.Age}");
        OnSwipe(finger.StartScreenPosition, finger.ScreenPosition, finger.Age);
    }

    private void OnSwipe(Vector2 screenStart, Vector2 screenEnd, float time)
    {
        var viewportStart = Camera.main.ScreenToViewportPoint(screenStart);
        var viewportEnd = Camera.main.ScreenToViewportPoint(screenEnd);
        var normalizedDelta = viewportEnd - viewportStart;

        var packages = RaycastPackages(viewportStart, viewportEnd);
        foreach (var package in packages)
        {
            package.OnSwipe(normalizedDelta, time);
        }
    }

    private IEnumerable<PackageBox> RaycastPackages(Vector3 viewportStart, Vector3 viewportEnd)
    {
        viewportStart.z = 1f;
        var ray = Camera.main.ViewportPointToRay(viewportStart);
        var hitInfos = GetRays(viewportStart, viewportEnd, Camera.main)
            .SelectMany(ray => Physics.RaycastAll(ray,
            100f,
            layerMask: LayerMask.GetMask(Consts.Layer_BoxSwipeZone), QueryTriggerInteraction.Collide)
        ).ToArray();

        if (!hitInfos.Any()) return null; // oh no!
        return hitInfos.Select(hitInfo => hitInfo.collider.transform.parent.GetComponent<PackageBox>());
    }

    private IEnumerable<Ray> GetRays(Vector3 viewPortStart, Vector3 viewPortEnd, Camera cam, int rayCount = 6)
    {
        var deltaT = 1f / rayCount;
        for (var i = 0; i < rayCount; i++)
        {
            var t = deltaT * i;
            var point = Vector3.Lerp(viewPortStart, viewPortEnd, t);
            yield return cam.ViewportPointToRay(point);
        }
    }

    // private PackageSpawner FindClosestSpawner(Vector3 viewportPos)
    // {
    //     Debug.Log($"Find close ({viewportPos}");
    //     // var sampleDist = _spawnersInScene[0].transform.position.z;
    //     // viewportPos.z = -sampleDist;
    //
    //     var i = FindIndexContaining(_screenSplitXs, viewportPos.x);
    //     return _spawners[i];
    //     //
    //     // var worldPoint = Camera.main.ViewportToWorldPoint(viewportPos);
    //     // DebugStuff.Instance.DrawPoint(worldPoint, 2f);
    //     // float SpawnerSqrDist(PackageSpawner spawner)
    //     // {
    //     //     var spawnerPos = spawner.transform.position;
    //     //     var ray = worldPoint - spawnerPos;
    //     //     
    //     //     Debug.DrawRay(worldPoint, 15f * ray, Color.green, 1.5f);
    //     //     return ray.sqrMagnitude;
    //     // }
    //     //
    //     // PackageSpawner minSpawn = null;
    //     // var minDist = float.PositiveInfinity;
    //     // foreach (var packageSpawner in _spawnersInScene)
    //     // {
    //     //     if (SpawnerSqrDist(packageSpawner) < minDist) minSpawn = packageSpawner;
    //     // }
    //     //
    //     // return minSpawn;
    // }

    private int FindIndexContaining(RangedFloat[] ranges, float val)
    {
        for (var i = 0; i < ranges.Length; i++)
        {
            var range = ranges[i];
            if (range.minValue <= val && val <= range.maxValue) return i;
        }

        return -1;
    }

    // private Vector3 GetFlingForce(Vector2 viewPortDelta, float time)
    // {
    //     var force = new Vector3(viewPortDelta.x, 0, viewPortDelta.y);
    //
    //     var viewportSpeed = viewPortDelta.magnitude / time;
    //     var arc = _baseFlingArc + _flingArcSpeedMult * viewportSpeed; // this number is magic
    //     return force * _flingXZPower + arc * Vector3.up;
    // }
}

public class DebugStuff : Singleton<DebugStuff>
{
    public void DrawPoint(Vector3 point, float dur)
    {
        StartCoroutine(ShowFor(point, dur));
    }

    private IEnumerator ShowFor(Vector3 point, float dur)
    {
        var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        go.transform.position = point;
        yield return new WaitForSeconds(dur);
        Destroy(go);
    }
}