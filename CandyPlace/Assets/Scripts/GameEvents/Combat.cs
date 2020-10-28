using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public int baseADamage;
    public int health;
    public float attackRange;
    public float APdistance;
    public GameObject attackPoint;
    public int framesTimerReset;
    public string target;


    protected int direction; //direction facing, 0 right, 1 left, 2 up 3 down
    Animator animator;
    bool isframes;
    //public int 
    // Start is called before the first frame update
    void Start()
    {
        attackPoint = transform.Find("attackPoint").gameObject;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Attack()
    {
        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackRange);
        foreach(Collider2D hitTarget in hitTargets)
        {
             GameObject hitT = hitTarget.gameObject;
            if(hitT.tag == target)
            {
                hitT.GetComponent<Combat>().TakeDamage(baseADamage);
            }
        }
    }
    public virtual void DetermineAttackArea()
    {
         
        Vector3 transfer = transform.position;
        if (direction == 0)
        {
            transfer.x = APdistance;
        }
        else if (direction == 1)
        {
            transfer.x = -APdistance;
        }
        else if (direction == 2)
        {
            transfer.y = APdistance;
        }
        else if (direction == 3)
        {
            transfer.y = -APdistance;
        }
        attackPoint.transform.position = transfer;
    }
    public void TakeDamage(int damage)
    {
        if (!isframes)
        {
            animator.SetTrigger("damage");
            isframes = true;
            health -= damage;
        }
    }
    public void ResetFrames()
    {
        isframes = false;
    }
}
