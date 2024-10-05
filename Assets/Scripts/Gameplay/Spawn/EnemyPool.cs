using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int initialPoolSize = 10;
    public int maxPoolSize = 20;

    private IObjectPool<GameObject> enemyPool;

    private void Awake()
    {
        enemyPool = new ObjectPool<GameObject>(CreateEnemy, OnGetEnemy, OnReleaseEnemy, OnDestroyEnemy, false, initialPoolSize, maxPoolSize);
    }

    private GameObject CreateEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab);
        enemy.SetActive(false);
        return enemy;
    }

    private void OnGetEnemy(GameObject enemy)
    {
        enemy.SetActive(true);
    }

    private void OnReleaseEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
    }

    private void OnDestroyEnemy(GameObject enemy)
    {
        Destroy(enemy);
    }

    public EnemyUnit GetEnemy()
    {
        return enemyPool.Get().GetComponent<EnemyUnit>();
    }

    public void ReleaseEnemy(GameObject enemy)
    {
        enemyPool.Release(enemy);
    }
}