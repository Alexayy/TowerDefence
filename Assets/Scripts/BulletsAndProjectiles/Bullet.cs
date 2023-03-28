using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform _target;
    public float speed = 50f;
    
    public void SeekAndDamage(Transform target)
    {
        _target = target;
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
        Debug.Log("HIT");
        Destroy(gameObject);
        Destroy(_target.gameObject);
    }
}
