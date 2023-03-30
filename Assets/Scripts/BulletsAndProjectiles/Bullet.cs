using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject
{
    private Transform _target;
    private EnemyBase _enemy;
    public float speed = 50f;
    [SerializeField] private float lifespan = 2.0f;
    
    public void SeekAndDamage(Transform target)
    {
        _target = target;
        _enemy = _target.GetComponent<EnemyBase>();
    }

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

    private void HitTarget()
    {
        Debug.Log($"HIT");
        // Destroy(gameObject);
    }

    public void OnObjectSpawn()
    {
        Debug.Log("AAA");
        Invoke("Despawn", lifespan);
    }

    public void OnObjectDespawn()
    {
        CancelInvoke();
    }
}
