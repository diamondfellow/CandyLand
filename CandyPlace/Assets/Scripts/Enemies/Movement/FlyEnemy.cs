using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class FlyEnemy : EnemyCombat
{
    public float maximumMoveDistance;
    public float minMoveTime;
    public float maxMoveTime;
    public float speed;
    float timer;
    float fixTimer;
    bool isMoving;
    void Start()
    {
        
    }
    void Update()
    {
        timer += Time.deltaTime;
        fixTimer += Time.deltaTime;
        if (searchPlayer)
        {
            AttackMove();
            return;
        }
        float checkTime = Random.Range(minMoveTime, maxMoveTime);
        
        if(timer > checkTime && !isMoving)
        {
            timer = 0;
            fixTimer = 0;
            RandomMove();
        }
    }
    void AttackMove()
    {
        Vector3 transfer = player.transform.position - transform.position;
        transfer = Vector3.Normalize(transfer);
        GetComponent<Rigidbody2D>().velocity = transfer * speed;
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
    public override void DetectPlayer()
    {
        
    }
}
