using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public abstract class EnemyBase : MonoBehaviour, IPooledObject
{
    [SerializeField] private float _speed;
    [FormerlySerializedAs("_health")] [SerializeField] private int _maxHealth;
    private int _currentHealth;
    [SerializeField] private int _damage;
    [SerializeField] private int _reward;

    [SerializeField] private Slider healthSlider;

    private Vector3[] _wayPoints;
    private int currentWayPointIndex;

    public GameObject deathEffect;

    public AudioClip dieSound;
    public AudioClip spawnSound;
    
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

    public void TakeDamage(int damageTaken)
    {
        if (_currentHealth <= 0)
        {
            Die();
            return;
        }
        
        _currentHealth -= damageTaken;
        healthSlider.value = (float)_currentHealth / (float)_maxHealth;
    }

    protected virtual void Die()
    {
        // play death animation coroutine
        SFXManager.Instance.PlaySound(dieSound);
        OnObjectDespawn();
    }

    public void OnObjectSpawn()
    {
        currentWayPointIndex = 0;
        _currentHealth = _maxHealth;
        SFXManager.Instance.PlaySound(spawnSound);
        Debug.Log($"{name} is spawned!");
    }

    public void OnObjectDespawn()
    {
        GameManager.Currency += _reward;
        ObjectPooler.Instance.ReturnToPool(gameObject);
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        
        Debug.Log($"{name} is despawned!");
    }

    public void SetWaypoints()
    {
        _wayPoints = WaypointManager.Instance.GetWaypoints();
        if (_wayPoints == null)
            return;
    }
}
