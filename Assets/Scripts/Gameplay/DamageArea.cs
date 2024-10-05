using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private int enemiesAmount = 3;

    private Dictionary<Collider, HealthHolder> _cachedHealthHolders = new Dictionary<Collider, HealthHolder>();
    private List<Collider> _hittingColliders = new List<Collider>();


    private void OnTriggerEnter(Collider other)
    {
        if (_hittingColliders.Count >= enemiesAmount) 
            return;

        if (Damage(other) && !_hittingColliders.Contains(other)) 
            _hittingColliders.Add(other);
    }

    private void OnTriggerStay(Collider other)
    {
        if (_hittingColliders.Count >= enemiesAmount && !_hittingColliders.Contains(other))
            return;

        if (Damage(other) && !_hittingColliders.Contains(other))
            _hittingColliders.Add(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (Damage(other) && _hittingColliders.Contains(other))
            _hittingColliders.Clear();
    }

    private bool Damage(Collider collision)
    {
        var gameObject = collision.gameObject;

        if (!gameObject.CompareTag(Tags.ENEMY_TAG)) 
            return false;

        if (!_cachedHealthHolders.ContainsKey(collision))
        {
            _cachedHealthHolders.Add(collision, gameObject.GetComponent<HealthHolder>());
            _cachedHealthHolders[collision].DestroyedAction += Remove;
        }
            

        _cachedHealthHolders[collision].Damage(damage);
        return true;
    }

    private void Remove(HealthHolder holder)
    {
        _hittingColliders.Clear();
    }
}
