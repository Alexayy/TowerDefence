using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private EnemyBase[] _enemyPrefabs;
    [SerializeField] private TowerBase[] _towerPrefabs;
    public Transform spawnLocation;

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

    private void EndGame()
    {
        CancelInvoke("SpawnAtInterval");
        
        // Lives for lose / win
    }

    public void TakeDamage(int damage)
    {
        if (health <= 0)
            Debug.Log("DIE PLAYER");

        health -= damage;
    }

    private void SpawnAtInterval()
    {
        int enemyIndex = Random.Range(0, _enemyPrefabs.Length);

        GameObject enemy = ObjectPooler.Instance.SpawnFromPool($"Enemy{enemyIndex}", spawnLocation.position, Quaternion.identity);
        enemy.GetComponent<EnemyBase>().SetWaypoints();
    }
}
