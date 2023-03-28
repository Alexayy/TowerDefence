using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private EnemyBase[] _enemyPrefabs;
    [SerializeField] private TowerBase[] _towerPrefabs;
    [SerializeField] private Transform _spawnLocation;

    [Header("Player")] 
    public int health;
    public int currency;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        InvokeRepeating("SpawnAtInterval", 0f, 5f);
    }

    public void TakeDamage(int damage)
    {
        if (health <= 0)
            Debug.Log("DIE PLAYER");

        health -= damage;
    }

    private void SpawnAtInterval()
    {
        // yield return new WaitForSeconds(1f);
        ObjectPooler.Instance.SpawnFromPool($"Enemy{Random.Range(1, 5)}", _spawnLocation.position, Quaternion.identity);
    }
}
