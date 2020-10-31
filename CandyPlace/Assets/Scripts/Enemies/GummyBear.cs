using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GummyBear : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -2.5f)
        {
            if (player.transform.position.x > transform.position.x)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(1.0f, 5.0f), Random.Range(5, 11));
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-4.0f, 0), Random.Range(5, 11));
            }
        }
    }
}
