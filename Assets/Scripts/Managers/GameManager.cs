using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private EnemyBase[] _enemyPrefabs;
    [SerializeField] private TowerBase[] _towerPrefabs;

    [Header("Player")] 
    [SerializeField] private int health;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        Instantiate(_enemyPrefabs[0]);
    }

    public void TakeDamage(int damage)
    {
        if (health <= 0)
            Debug.Log("DIE PLAYER");

        health -= damage;
    }
}
