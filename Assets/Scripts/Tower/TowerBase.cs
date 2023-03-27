using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : MonoBehaviour
{
    [Header("Base Stats")]
    [SerializeField] private int _cost;
    [SerializeField] private int _sellPrice;
    [SerializeField] private int _damage;
    [SerializeField] private float _range;
    [SerializeField] private float _fireInterval;
    
    [Header("Upgrade stats")]
    [SerializeField] private int _upgradeCost;
    [SerializeField] private int _damageUpgrade;
    [SerializeField] private float _upgradeFireInterval;
    [SerializeField] private float _upgradeRange;

    [Header("Other Stuff")] 
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Transform _target;
    [SerializeField] private float _fireTimer = 0f;

    protected virtual void Fire()
    {
        _target.GetComponent<EnemyBase>().TakeDamage(_damage);
    }

    protected virtual void Upgrade()
    {
        _damage += _damageUpgrade;
        _range += _upgradeRange;
        _fireInterval += _upgradeFireInterval;
    }
}
