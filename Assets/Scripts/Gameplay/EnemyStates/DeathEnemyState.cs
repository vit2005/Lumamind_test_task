using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class DeathEnemyState : MonoBehaviour, IUpdatable
{
     
    [SerializeField] private float flightDuration = 2f; // Час польоту
    [SerializeField] private float maxHeight = 1f;      // Максимальна висота
    private Transform _player;
    private Vector3 _startPoint;

    private float _elapsedTime = 0f;   // Лічильник часу
    private Action _onAnimetionFinished; 

    public void OnUpdate()
    {
        _elapsedTime += Time.deltaTime;

        // Нормалізуємо час
        float t = Mathf.Clamp01(_elapsedTime / flightDuration);

        // Рух по горизонталі
        Vector3 currentPos = Vector3.Lerp(_startPoint, _player.position, t);

        // Рух по вертикалі (параболічна траєкторія)
        float height = maxHeight * t * (1 - t); // Форма параболи

        // Додаємо вертикальний рух до позиції
        currentPos.y = Mathf.Lerp(_startPoint.y, _player.position.y, t) + height;

        // Задаємо нову позицію об'єкта
        transform.position = currentPos;

        // Зупинка руху після досягнення кінцевої точки
        if (_elapsedTime >= flightDuration)
        {
            _elapsedTime = 0f;
            _onAnimetionFinished?.Invoke();
        }
    }

    public void OnDestroyed(Transform player, Action OnFinished)
    {
        _player = player;
        _onAnimetionFinished = OnFinished;
        _startPoint = transform.position;
    }
}