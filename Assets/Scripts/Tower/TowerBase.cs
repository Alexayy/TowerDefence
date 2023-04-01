using UnityEngine;

public abstract class TowerBase : MonoBehaviour
{
    [Header("Base Stats")]
    public int _cost;
    public int _sellPrice;
    public int _damage;
    public float _range;
    public float _fireInterval;
    public float _fireCountdown;
    
    [Header("Upgrade stats")]
    public int _upgradeCost;
    public int _damageUpgrade;
    public float _upgradeFireInterval;
    public float _upgradeRange;

    [Header("Other Stuff")] 
    [SerializeField] private Transform _target;
    [SerializeField] private float _fireTimer = 0f;
    public string enemyTag = "Enemy";

    public AudioClip shootSound;
    public AudioClip placedTurretSound;

    public int Cost => _cost;
    public int SellPrice => _sellPrice;
    public int UpgradePrice => _upgradeCost;

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
        GameObject bulletGameObj = ObjectPooler.Instance.SpawnFromPool("Bullet", transform.position, Quaternion.identity);
        Bullet bullet = bulletGameObj.GetComponent<Bullet>();
        if (bullet != null && _target != null)
        {
            bullet.SeekAndDamage(_target);
            _target.GetComponent<EnemyBase>().TakeDamage(_damage);
            SFXManager.Instance.PlaySound(shootSound);
        }

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

    public void Sell()
    {
        GameManager.Currency += _sellPrice;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}
