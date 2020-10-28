using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : Combat
{
    bool canAttack;
    float timer;
    public float rangedTimer;
    public GameObject rangedProjectile;
    public int rangeDamage;
    // Start is called before the first frame update
    void Start()
    {
        APdistance = Mathf.Abs(APdistance);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            DetermineAttackArea();
            Attack();
            canAttack = false;
        }
        if(Input.GetMouseButtonDown(1)/*placeholder*/ &&  timer > rangedTimer)
        {

        }
    }
    void CanAttack()
    {
        canAttack = true;
    }

}
