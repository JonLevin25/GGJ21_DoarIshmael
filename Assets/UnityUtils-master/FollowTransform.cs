using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FollowTransform : MonoBehaviour
{

    [SerializeField] private Transform follow;
    [SerializeField] private bool onLateUpdate;
    

    private Transform _myTransform;

    private void Awake()
    {
        OnValidate();
    }
    
    private void OnValidate()
    {
        _myTransform = transform;
    }

    void Update()
    {
        if (!onLateUpdate)
        {
            SetLocation();
        }
    }

    private void SetLocation()
    {
        _myTransform.position = follow.position;
        _myTransform.rotation = follow.rotation;
    }

    private void LateUpdate()
    {
        if (onLateUpdate)
        {
            SetLocation();
        }
    }
}
