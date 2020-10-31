using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLocking : MonoBehaviour
{
    public GameObject player;

    public float x;
    public float negX;
    public float y;
    public float negY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
        if (transform.position.x > x)
        {
            transform.position = new Vector3(x, transform.position.y, -10);
        }
        else if (transform.position.x < negX)
        {
            transform.position = new Vector3(negX, transform.position.y, -10);
        }
        if (transform.position.y > y)
        {
            transform.position = new Vector3(transform.position.x, y, -10);
        }
        else if (transform.position.y < negY)
        {
            transform.position = new Vector3(transform.position.x, negY, -10);
        }
    }
}
