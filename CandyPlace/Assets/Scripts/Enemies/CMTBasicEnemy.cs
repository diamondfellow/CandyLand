using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : EnemyCombat
{

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (searchPlayer)
        {
            DetermineAttackArea();
            DetectPlayer();
        }
    }
    public override void DetermineAttackArea()
    {
        if(!GetComponent<SpriteRenderer>().flipX)
        {
            direction = 0;
        }
        else
        {
            direction = 1;
        }
        base.DetermineAttackArea();
    }
}
