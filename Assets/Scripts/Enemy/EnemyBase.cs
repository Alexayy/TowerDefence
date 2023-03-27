using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private int _reward;

    private Vector3[] _wayPoints;
    private int currentWayPointIndex;
    
    public int Reward { get { return _reward; } }
    public int Damage { get { return _damage; } }
    
    private void Awake()
    {
        _wayPoints = WaypointManager.Instance.GetWaypoints();
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

        playerHealth -= _damage;
    }

    public virtual void TakeDamage(int damageTaken)
    {
        if (_health <= 0)
        {
            Die();
            return;
        }
        
        _health -= damageTaken;
    }

    protected virtual void Die()
    {
        // play death animation coroutine
        Destroy(gameObject);
    }
}
