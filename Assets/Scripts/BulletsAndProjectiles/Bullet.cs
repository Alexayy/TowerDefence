using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject
{
    [Header("Bullet Speed")]
    public float speed = 50f;
    
    [Header("Enemy Fields")]
    private Transform _target;
    private EnemyBase _enemy;
    
    [Header("Properties and Other")]
    [SerializeField] private float lifespan = 2.0f;
    
    private void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = _target.position - transform.position;
        float distance = speed * Time.deltaTime;

        if (direction.magnitude <= distance)
        {
            HitTarget();
            return;
        }
            
        transform.Translate(direction.normalized * distance, Space.World);
    }

    #region Bullet Hit Methods

    public void SeekAndDamage(Transform target)
    {
        _target = target;
        _enemy = _target.GetComponent<EnemyBase>();
    }

    private void HitTarget()
    {
        // Debug.Log($"HIT");
        Destroy(gameObject);
    }

    #endregion

    #region Implemented

    public void OnObjectSpawn()
    {
        // Debug.Log("AAA");
        Invoke("Despawn", lifespan);
    }

    public void OnObjectDespawn()
    {
        CancelInvoke();
    }

    #endregion
   
}
