using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyCorn : MonoBehaviour
{
    public float detectionRange;
    public float undetectedRange;
    List<string> objects = new List<string>();
    bool freakOut;
    bool grounded;
    float groundedTimer;
    GameObject player;
    public float speed;
    int health = 1;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(transform.position, detectionRange);
        foreach (Collider2D detected in detectedObjects)
        {
            if (detected.tag == "Player")
            {
                freakOut = true;
            }
        }

        objects.Clear();
        detectedObjects = Physics2D.OverlapCircleAll(transform.position, undetectedRange);
        foreach (Collider2D detected in detectedObjects)
        {
            objects.Add(detected.tag);
        }
        if (!objects.Contains("Player"))
        {
            freakOut = false;
        }

        objects.Clear();
        detectedObjects = Physics2D.OverlapCircleAll(transform.position - new Vector3(0, .1f, 0), .5f);
        foreach (Collider2D detected in detectedObjects)
        {
            objects.Add(detected.tag);
        }
        if (objects.Contains("Ground"))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        if (grounded)
        {
            groundedTimer += Time.deltaTime;
        }
        else
        {
            groundedTimer = 0;
        }

        if (freakOut && grounded && health == 1)
        {
            if (transform.position.y >= player.transform.position.y - .75f || groundedTimer > .5f)
            {
                if (player.transform.position.x < transform.position.x)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(speed, speed + 4), Random.Range(5, 11));
                }
                else
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(speed - 4, speed), Random.Range(5, 11));
                }
                grounded = false;
            }
            else if (player.transform.position.x < transform.position.x)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(speed, speed + 4), GetComponent<Rigidbody2D>().velocity.y);
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(speed - 4, speed), GetComponent<Rigidbody2D>().velocity.y);
            }
        }
        else if (health < 1)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        }
    }
}
