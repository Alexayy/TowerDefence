using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
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

    public int numberOfSpawnLevels = 10;
    public int waveNumber = 0;

    public AudioClip youWinSound;
    public AudioClip youLoseSound;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Health = startingHealth;
        Currency = startMoneyAmount;
        
        StartGame();
    }

    public void StartGame()
    {
        InvokeRepeating("SpawnAtInterval", 0f, 5f);
    }
    
    public void EndGame()
    {
        CancelInvoke("SpawnAtInterval");
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        
        if (Health <= 0)
        {
            UIManager.Instance.YouLose();
            SFXManager.Instance.PlaySound(youLoseSound);
            PauseGame();
            Debug.Log("DIE PLAYER");
            EndGame();
        }
    }

    private void SpawnAtInterval()
    {
        int enemyIndex = Random.Range(1, _enemyPrefabs.Length + 1);
        
        GameObject enemy =
            ObjectPooler.Instance.SpawnFromPool($"Enemy{enemyIndex}", spawnLocation.position, Quaternion.identity);
        enemy.GetComponent<EnemyBase>().SetWaypoints();
        
        waveNumber++;
        
        if (waveNumber >= numberOfSpawnLevels && Health > 0)
        {
            UIManager.Instance.YouWin();
            SFXManager.Instance.PlaySound(youWinSound);
            PauseGame();
            EndGame();
        }
    }
    
    public void PauseGame()
    {
        Time.timeScale = 0; 
    }
    
    public void ResumeGame ()
    {
        Time.timeScale = 1;
    }
}
