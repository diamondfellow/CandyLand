using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name == "Boulder(Clone)")
        {
            transform.position = new Vector3(transform.position.x, -2, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.name == "Boulder(Clone)" && collision.gameObject.name != "Player")
        {
            Destroy(gameObject);
        }
        else if (gameObject.name != "Boulder(Clone)")
        {
            Destroy(gameObject);
        }
    }
}
