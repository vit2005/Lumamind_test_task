using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyUnit : MonoBehaviour, IUpdatable
{
    [SerializeField] private HealthHolder healthHolder;

    [SerializeField] private DeathEnemyState deathState;
    [SerializeField] private IdleEnemyState idleState;

    private IUpdatable _currentState;
    private Transform _player;
    private EnemySpawner _spawner;

    public void Init(Transform mainTarget, EnemySpawner spawner)
    {
        _player = mainTarget;
        _spawner = spawner;
        healthHolder.DestroyedAction += OnDestroyed;
        _currentState = idleState;
    }

    private void OnDestroyed(HealthHolder holder)
    {
        healthHolder.DestroyedAction -= OnDestroyed;
        deathState.OnDestroyed(_player, () => {
            holder.Revive();
            _spawner.UnregisterUnit(this); 
        });
        _currentState = deathState;
    }

    public void OnUpdate()
    {
        _currentState.OnUpdate();
    }
}
