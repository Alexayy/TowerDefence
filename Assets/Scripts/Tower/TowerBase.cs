using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class TowerBase : MonoBehaviour
{
    [Header("Base Stats")]
    [SerializeField] private int _cost;
    [SerializeField] private int _sellPrice;
    [SerializeField] private int _damage;
    [SerializeField] private float _range;
    [SerializeField] private float _fireInterval;
    [SerializeField] private float _fireCountdown;
    
    [Header("Upgrade stats")]
    [SerializeField] private int _upgradeCost;
    [SerializeField] private int _damageUpgrade;
    [SerializeField] private float _upgradeFireInterval;
    [SerializeField] private float _upgradeRange;

    [Header("Other Stuff")] 
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Transform _target;
    [SerializeField] private float _fireTimer = 0f;
    public string enemyTag = "Enemy";

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, .5f);
        Debug.Log($"Turret: {gameObject.name}");
    }

    private void Update()
    {
        if (_target == null)
            return;

        Vector3 direction = _target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = lookRotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (_fireCountdown <= 0f)
        {
            Shoot();
            _fireCountdown = 1f / _fireInterval;
        }

        _fireCountdown -= Time.deltaTime;
    }

    private void Shoot()
    {
        Debug.Log($"Shoot!");
    }
    
    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= _range)
            _target = nearestEnemy.transform;
        else
            _target = null;
    }
    
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}
