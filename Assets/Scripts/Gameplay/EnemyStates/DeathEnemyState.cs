using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class DeathEnemyState : MonoBehaviour, IUpdatable
{
     
    [SerializeField] private float flightDuration = 2f; // ��� �������
    [SerializeField] private float maxHeight = 1f;      // ����������� ������
    private Transform _player;
    private Vector3 _startPoint;

    private float _elapsedTime = 0f;   // ˳������� ����
    private Action _onAnimetionFinished; 

    public void OnUpdate()
    {
        _elapsedTime += Time.deltaTime;

        // ���������� ���
        float t = Mathf.Clamp01(_elapsedTime / flightDuration);

        // ��� �� ����������
        Vector3 currentPos = Vector3.Lerp(_startPoint, _player.position, t);

        // ��� �� �������� (���������� ��������)
        float height = maxHeight * t * (1 - t); // ����� ��������

        // ������ ������������ ��� �� �������
        currentPos.y = Mathf.Lerp(_startPoint.y, _player.position.y, t) + height;

        // ������ ���� ������� ��'����
        transform.position = currentPos;

        // ������� ���� ���� ���������� ������ �����
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