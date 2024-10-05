using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthVisualizer : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private HealthHolder healthHolder;

    private float _initialWidth;

    private void Start()
    {
        healthHolder.DamagedAction += OnDamage;
        healthHolder.RevivedAction += OnRevive;
        _initialWidth = target.localScale.x;
    }

    private void OnRevive()
    {
        target.localScale = new Vector3(_initialWidth, target.localScale.y, target.localScale.z);
    }

    private void OnDamage(float percentage)
    {
        target.localScale = new Vector3(percentage * _initialWidth, target.localScale.y, target.localScale.z);
    }

    

}
