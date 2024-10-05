using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] Joystick inputHandler;
    [SerializeField] PlayerMovementController movementController;

    void Awake()
    {
        movementController.inputHandler = inputHandler;
    }
}
