using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : Combat
{
    protected bool searchPlayer;
    public float activationDistance;
    protected GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        if(dist < activationDistance)
        {
            searchPlayer = true;
        }
        else
        {
            searchPlayer = false;
        }
    }
    public virtual void DetectPlayer()
    {
        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackRange);
        foreach (Collider2D hitTarget in hitTargets)
        {
            GameObject hitT = hitTarget.gameObject;
            if (hitT.tag == target)
            {
                Attack();
            }
        }
    }
}
