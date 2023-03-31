using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private EnemyBase[] _enemyPrefabs;
    public Transform spawnLocation;

    [Header("Player Stats")] 
    public static int Health;
    public int startingHealth = 50;
    public static int Currency;
    public int startMoneyAmount = 500;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        Health = startingHealth;
        Currency = startMoneyAmount;
        InvokeRepeating("SpawnAtInterval", 0f, 5f);
    }

    private void EndGame()
    {
        CancelInvoke("SpawnAtInterval");
        
        // Lives for lose / win
    }

    public void TakeDamage(int damage)
    {
        if (Health <= 0)
            Debug.Log("DIE PLAYER");

        Health -= damage;
    }

    private void SpawnAtInterval()
    {
        int enemyIndex = Random.Range(1, _enemyPrefabs.Length);

        GameObject enemy = ObjectPooler.Instance.SpawnFromPool($"Enemy{enemyIndex}", spawnLocation.position, Quaternion.identity);
        enemy.GetComponent<EnemyBase>().SetWaypoints();
    }
    
    public void PauseGame ()
    {
        Time.timeScale = 0;
    }
    
    public void ResumeGame ()
    {
        Time.timeScale = 1;
    }
}
