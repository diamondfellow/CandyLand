using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCombat : MonoBehaviour
{
    public float speed;
    public float distance;
    public int damage;
    public string target;// The name of the tag that the projectile looks for

    Vector3 originalPosition;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Vector3.Distance(originalPosition, transform.position) > distance)
        {
            Destroy(gameObject);
        }
        MoveProjectile();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Combat targetCombat;
        if(collision.gameObject.tag == target && TryGetComponent<Combat>(out targetCombat))
        {
            targetCombat.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
    public virtual void MoveProjectile()
    {

    }
}
