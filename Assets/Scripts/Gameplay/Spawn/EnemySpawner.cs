using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, IUpdatable
{
    [SerializeField] private EnemyPool enemyPool;
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] private float spawnInterval = 5f;
    //[SerializeField] private BulletPool bulletPool;
    [SerializeField] private Transform mainTarget;

    public int enemiesToSpawn;

    public List<EnemyUnit> enemies = new List<EnemyUnit>();
    public List<EnemyUnit> enemiesToRemove = new List<EnemyUnit>();

    private float nextSpawnTime;

    private void Start()
    {
        nextSpawnTime = Time.time;
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy();
        }
    }

    public void OnUpdate()
    {
        if ((enemiesToSpawn > enemies.Count) && Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval;
        }

        foreach (EnemyUnit e in enemies) {
            e.OnUpdate();
        }

        foreach (var e in enemiesToRemove)
        {
            enemyPool.ReleaseEnemy(e.gameObject);
            enemies.Remove(e);
        }
        enemiesToRemove.Clear();
    }

    private void SpawnEnemy()
    {
        int spawnIndex = UnityEngine.Random.Range(0, spawnPoints.Count);
        EnemyUnit enemy = enemyPool.GetEnemy();
        enemy.transform.position = spawnPoints[spawnIndex].position;
        enemy.Init(mainTarget, this);
        enemies.Add(enemy);
    }

    public void UnregisterUnit(EnemyUnit unit)
    {
        enemiesToRemove.Add(unit);
        //enemies.Remove(unit);
    }
}
