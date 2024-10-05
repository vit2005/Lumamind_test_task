using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagebleArea : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    private const string TARGET_TAG = "Enemy";

    private Dictionary<Collider, HealthHolder> _cachedHealthHolders = new Dictionary<Collider, HealthHolder>();


    private void OnTriggerEnter(Collider other)
    {
        Damage(other);
    }

    private void OnTriggerStay(Collider other)
    {
        Damage(other);
    }

    private void OnTriggerExit(Collider other)
    {
        Damage(other);
    }

    private void Damage(Collider collision)
    {
        var gameObject = collision.gameObject;

        if (!gameObject.CompareTag(TARGET_TAG)) 
            return;

        if (!_cachedHealthHolders.ContainsKey(collision))
            _cachedHealthHolders.Add(collision, gameObject.GetComponent<HealthHolder>());

        _cachedHealthHolders[collision].Damage(damage);
    }
}
