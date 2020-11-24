using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    float xStart;
    float yStart;
    public float xSpeed;
    public float ySpeed;
    public float xDistance;
    public float yDistance;
    bool forward = true;
    // Start is called before the first frame update
    void Start()
    {
        xStart = transform.position.x;
        yStart = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (forward)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(xSpeed, ySpeed);
            if ((transform.position.x > xStart + xDistance && xDistance > 0) || (transform.position.x < xStart + xDistance && xDistance < 0))
            {
                forward = false;
            }
            else if ((transform.position.y > yStart + yDistance && yDistance > 0) || (transform.position.y < yStart + yDistance && yDistance < 0))
            {
                forward = false;
            }
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-xSpeed, -ySpeed);
            if ((transform.position.x < xStart && xDistance > 0) || (transform.position.x > xStart && xDistance < 0))
            {
                forward = true;
            }
            else if ((transform.position.y < yStart && yDistance > 0) || (transform.position.y > yStart && yDistance < 0))
            {
                forward = true;
            }
        }
    }
}
