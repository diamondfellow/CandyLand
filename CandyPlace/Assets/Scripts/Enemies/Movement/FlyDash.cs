using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyDash : EnemyCombat
{
    public float maximumMoveDistance;
    public float minMoveTime;
    public float maxMoveTime;
    public float speed;
    public float attackRadius;
    float timer;
    float fixTimer;
    float attackTimer;
    float dashTimer;
    float stunTimer;
    bool isMoving;
    bool stun;
    void Start()
    {

    }
    void Update()
    {
        timer += Time.deltaTime;
        fixTimer += Time.deltaTime;
        dashTimer += Time.deltaTime;
        attackTimer += Time.deltaTime;
        stunTimer += Time.deltaTime;
        if (stunTimer > 1)
        {
            stun = false;
        }
        if (searchPlayer && !stun)
        {
            AttackMove();
            return;
        }
        float checkTime = Random.Range(minMoveTime, maxMoveTime);

        if (timer > checkTime && !isMoving && !stun)
        {
            timer = 0;
            fixTimer = 0;
            RandomMove();
        }
    }
    void AttackMove()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > attackRadius)
        {
            Vector3 transfer = player.transform.position - transform.position;
            transfer = Vector3.Normalize(transfer);
            GetComponent<Rigidbody2D>().velocity = transfer * speed;
        }
        else if(!stun)
        {
            Dash();
        }
    }
    void Dash()
    {
        Vector3 transfer = player.transform.position - transform.position;
        transfer = Vector3.Normalize(transfer);
        dashTimer = 0;
        while (dashTimer < 1)
        {
            GetComponent<Rigidbody2D>().velocity = transfer * speed * 5;
        }
        stun = true;
        stunTimer = 0;
    }
    void RandomMove()
    {
        Vector3 randomPoint = transform.position;
        randomPoint.x = Random.Range(-maximumMoveDistance, maximumMoveDistance);
        randomPoint.y = Random.Range(-maximumMoveDistance, maximumMoveDistance);
        isMoving = true;
        while (randomPoint.x - transform.position.x > .1 && randomPoint.y - transform.position.y > .1 && isMoving)
        {
            Vector3 transfer = randomPoint - transform.position;
            transfer = Vector3.Normalize(transfer);
            GetComponent<Rigidbody2D>().velocity = transfer * speed;
        }
        isMoving = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" && fixTimer > 1)
        {
            isMoving = false;
        }
    }
}
