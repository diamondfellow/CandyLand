using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormBoss : MonoBehaviour
{
    public GameObject player;
    public GameObject candyRock;
    public GameObject candyBoulder;
    public GameObject gummyBear;

    int phase = 0;
    int oldPhase;
    int random;
    int digCounter;

    int health = 100;
    public float speed;
    public bool upDown = false;

    float timer;
    bool timerGoing;

    float spewTimer;
    public float spewTimerMax;
    bool spewTimerGoing;
    bool spewing = false;
    int spewCounter;
    string spewDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timerGoing)
        {
            timer += Time.deltaTime;
        }
        if (spewTimerGoing)
        {
            spewTimer += Time.deltaTime;
        }

        if (phase == 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            PhaseInitiate();
        }
        else if (phase == 1)
        {
            timerGoing = true;
            DigThrough();
        }
        else if (phase == 2)
        {
            timerGoing = true;
            if (spewCounter < 4)
            {
                spewing = true;
            }
            UpSpew();
        }
        else
        {
            timerGoing = true;
            if (spewCounter < 4)
            {
                spewing = true;
            }
            SideSpew();
        }
    }

    void PhaseInitiate()
    {
        spewCounter = 0;
        if (phase == 1)
        {
            if (digCounter > 3 || digCounter > Random.Range(0, 3))
            {
                do
                {
                    phase = Random.Range(1, 4);
                } while (phase == oldPhase);
                oldPhase = phase;
                digCounter = 0;
                timer = 0;
            }
        }
        else
        {
            do
            {
                phase = Random.Range(1, 4);
            } while (phase == oldPhase);
            oldPhase = phase;
            digCounter = 0;
            timer = 0;
        }

        if (phase == 1) //Dive through phase
        {
            random = Random.Range(1, 5);
            Debug.Log(digCounter);
            if (random == 1)
            {
                transform.position = new Vector3(player.transform.position.x, 13, 0);
                transform.eulerAngles = new Vector3(0, 0, 90);
                upDown = true;
            }
            else if (random == 2)
            {
                transform.position = new Vector3(player.transform.position.x, -11, 0);
                transform.eulerAngles = new Vector3(0, 0, 90);
                upDown = true;
            }
            else if (random == 3)
            {
                transform.position = new Vector3(20, player.transform.position.y, 0);
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                transform.position = new Vector3(-19, player.transform.position.y, 0);
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
        else if (phase == 2) //Up
        {
            transform.position = new Vector3(Random.Range(-9.0f, 10.0f), -11, 0);
            transform.eulerAngles = new Vector3(0, 0, 90);
            upDown = true;
        }
        else
        {
            if (Random.Range(1, 3) == 1)
            {
                spewDirection = "right";
                transform.position = new Vector3(19, -2, 0);
            }
            else
            {
                spewDirection = "left";
                transform.position = new Vector3(-19, -2, 0);
            }
            transform.eulerAngles = new Vector3(0, 0, 0);
            upDown = true;
        }
    }

    void DigThrough()
    {
        if (timer > 2 + (health / 100))
        {
            if (random == 1)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
                if (transform.position.y < -11)
                {
                    digCounter++;
                    phase = 0;
                    upDown = false;
                    timer = 0;
                    timerGoing = false;
                }
            }
            else if (random == 2)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
                if (transform.position.y > 13)
                {
                    digCounter++;
                    phase = 0;
                    upDown = false;
                    timer = 0;
                    timerGoing = false;
                }
            }
            else if (random == 3)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
                if (transform.position.x < -19)
                {
                    digCounter++;
                    phase = 0;
                    timer = 0;
                    timerGoing = false;
                }
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
                if (transform.position.x > 20)
                {
                    digCounter++;
                    phase = 0;
                    timer = 0;
                    timerGoing = false;
                }
            }
        }
    }

    void UpSpew()
    {
        upDown = true;
        if (spewing)
        {
            if (timer > 2 && transform.position.y < -5)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                if (timer > 2)
                {
                    spewTimerGoing = true;
                }
            }

            if (spewTimer > spewTimerMax)
            {
                spewTimer = 0;
                Spew();
            }
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
            if (transform.position.y < -11)
            {
                phase = 0;
                timer = 0;
                timerGoing = false;
                spewTimer = 0;
                spewTimerGoing = false;
            }
        }
    }

    void Spew()
    {
        GameObject projectile;
        if (upDown)
        {
            if (health < 50 && Random.Range(1, 5) == 1)
            {
                projectile = Instantiate(gummyBear, transform.position + new Vector3(0, 6.3f, 0), Quaternion.identity);
            }
            else
            {
                projectile = Instantiate(candyRock, transform.position + new Vector3(0, 6.3f, 0), Quaternion.identity);
            }
            if (Random.Range(1, 3) == 1)
            {
                projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-7, -1), Random.Range(4, 11));
            }
            else
            {
                projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(2, 8), Random.Range(4, 11));
            }
        }
        else
        {
            if (spewDirection == "right")
            {
                if (health < 50 && Random.Range(1, 5) == 1)
                {
                    projectile = Instantiate(gummyBear, transform.position + new Vector3(-5.5f, 0, 0), Quaternion.identity);
                    projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-7, -3), 0);
                }
                else
                {
                    projectile = Instantiate(candyBoulder, transform.position + new Vector3(-5.5f, 0, 0), Quaternion.identity);
                    projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-7, -3), 0);
                }
            }
            else
            {
                if (health < 50 && Random.Range(1, 5) == 1)
                {
                    projectile = Instantiate(gummyBear, transform.position + new Vector3(5.5f, 0, 0), Quaternion.identity);
                    projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(2, 8), 0);
                }
                else
                {
                    projectile = Instantiate(candyBoulder, transform.position + new Vector3(5.5f, 0, 0), Quaternion.identity);
                    projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(2, 8), 0);
                }
            }
        }
        spewCounter++;
        if (spewCounter > 7 && Random.Range(1, 4) == 1)
        {
            spewing = false;
        }
    }

    void SideSpew()
    {
        upDown = false;
        if (spewing)
        {
            if (timer > 2 && spewDirection == "right" && transform.position.x > 15)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
            }
            else if (timer > 2 && spewDirection == "left" && transform.position.x < -15)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                if (timer > 2)
                {
                    spewTimerGoing = true;
                }
            }

            if (spewTimer > spewTimerMax * 3)
            {
                spewTimer = 0;
                Spew();
            }
        }
        else
        {
            if (spewDirection == "right")
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
                if (transform.position.x > 19) 
                {
                    phase = 0;
                    timer = 0;
                    timerGoing = false;
                    spewTimer = 0;
                    spewTimerGoing = false;
                }
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
                if (transform.position.x < -19)
                {
                    phase = 0;
                    timer = 0;
                    timerGoing = false;
                    spewTimer = 0;
                    spewTimerGoing = false;
                }
            }
        }
    }

    /*
    void OnCollisionStay2D(Collision2D collision)
    {
        if ((GetComponent<Rigidbody2D>().velocity.y > 0 && player.transform.position.y > transform.position.y) || (GetComponent<Rigidbody2D>().velocity.y < 0 && player.transform.position.y < transform.position.y))
        {
            if (player.transform.position.x > transform.position.x)
            {
                player.transform.position = player.transform.position + new Vector3(.1f, 0, 0);
            }
            else
            {
                player.transform.position = player.transform.position + new Vector3(-.1f, 0, 0);
            }
        }
    }
    */
}
