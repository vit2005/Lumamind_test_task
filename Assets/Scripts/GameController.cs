using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] Joystick inputHandler;
    [SerializeField] PlayerMovementController movementController;

    [SerializeField] List<EnemySpawner> enemySpawners = new List<EnemySpawner>();

    void Awake()
    {
        movementController.inputHandler = inputHandler;
    }

    private void Update()
    {
        foreach (var spawner in enemySpawners)
        {
            spawner.OnUpdate();
        }
    }
}
