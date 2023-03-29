using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public abstract class EnemyBase : MonoBehaviour, IPooledObject
{
    [SerializeField] private float _speed;
    [FormerlySerializedAs("_health")] [SerializeField] private int _maxHealth;
    private int _currentHealth;
    [SerializeField] private int _damage;
    [SerializeField] private int _reward;

    private Vector3[] _wayPoints;
    private int currentWayPointIndex;
    
    public int Reward { get { return _reward; } }
    public int Damage { get { return _damage; } }
    
    private void Awake()
    {
        // _wayPoints = WaypointManager.Instance.GetWaypoints();
    }

    private void Update()
    {
        if (currentWayPointIndex < _wayPoints.Length)
        {
            transform.position = Vector3.MoveTowards(transform.position, _wayPoints[currentWayPointIndex],
                _speed * Time.deltaTime);

            if (transform.position == _wayPoints[currentWayPointIndex])
                currentWayPointIndex++;
        }
        else
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
            GameManager.Instance.TakeDamage(_damage);
        }
    }

    protected virtual void DoDamage(int playerHealth)
    {
        if (playerHealth <= 0)
        {
            // Player loses;
            Debug.Log("Player lost!");
            return;
        }
    }

    public virtual void TakeDamage(int damageTaken)
    {
        if (_maxHealth <= 0)
        {
            Die();
            return;
        }
        
        _maxHealth -= damageTaken;
    }

    protected virtual void Die()
    {
        // play death animation coroutine
        OnObjectDespawn();
    }

    public void OnObjectSpawn()
    {
        currentWayPointIndex = 0;
        _currentHealth = _maxHealth;
        Debug.Log($"{name} is spawned!");
    }

    public void OnObjectDespawn()
    {
        ObjectPooler.Instance.ReturnToPool(gameObject);
        Debug.Log($"{name} is despawned!");
    }

    public void SetWaypoints()
    {
        _wayPoints = WaypointManager.Instance.GetWaypoints();
        if (_wayPoints == null)
            return;
    }
}
