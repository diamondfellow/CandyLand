using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBasicEnemy : MonoBehaviour
{
    public float moveDistance;
    public float speed;
    //Vector3 originPoint;
    Vector3 leftPoint;
    Vector3 RightPoint;
    bool movingRight;
    private void Awake()
    {
        leftPoint = transform.position;
        RightPoint = transform.position;
        leftPoint.x -= moveDistance;
        RightPoint.x += moveDistance;
    }
    void Start()
    {
        movingRight = true;
    }
    void Update()
    {
        if(transform.position.x > RightPoint.x)
        {
            movingRight = false;
        }
        else if(transform.position.x < leftPoint.x)
        {
            movingRight = true;
        }
        Vector2 transfer = GetComponent<Rigidbody2D>().velocity;
        if (movingRight)
        {
            transfer.x = speed;
        }
        else
        {
            transfer.x = -speed;
        }

        GetComponent<Rigidbody2D>().velocity = transfer;
    }
}
