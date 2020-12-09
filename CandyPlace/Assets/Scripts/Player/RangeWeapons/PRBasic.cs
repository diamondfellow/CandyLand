using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PRBasic : RangeCombat
{
    public int direction;
    public GameObject player;
    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(player.GetComponent<SpriteRenderer>().flipX)
        {
            direction = 1;
        }
        else
        {
            direction = 0;
        }
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public override void MoveProjectile()
    {
        Vector2 transfer = GetComponent<Rigidbody2D>().velocity;
        if (direction == 0)
        {         
            transfer.x = speed;
        }
        else if(direction == 1)
        {
            transfer.x = -speed;
        }
    }
}
