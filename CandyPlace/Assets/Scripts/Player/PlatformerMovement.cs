using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlatformerMovement : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float jumpSpeed = 1.0f;

    bool invincible = false;
    bool invincibleFreeze = false;
    float invincibleTimer;
    public float invincibleTime;
    public float knockback;

    bool grounded = false;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask isGround;

    bool wall = false;
    float wallTimer = 0;
    public Transform wallCheck;
    public float wallSlidingSpeed;
    Rigidbody2D rb;

    bool wallJumping = false;
    string wallJumpDirection;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;
    float wallJumpTimer;

    bool doubleJump = false;

    bool dash = false;
    bool diagDash = false;
    public float dashSpeed;
    bool dashFreeze;
    float dashFreezeTimer;
    public float dashFreezeTime;
    public float diagDashFreezeTime;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        PlayerPrefs.SetInt("CanWallJump", 1); //Remove before build
        PlayerPrefs.SetInt("CanDoubleJump", 1); //Remove before build
    }

    void Update()
    {
        if (invincible)
        {
            Debug.Log(rb.velocity.x + ", " + rb.velocity.y);
            invincibleTimer += Time.deltaTime;
            if (invincibleTimer > invincibleTime)
            {
                invincible = false;
                gameObject.layer = 0;
                invincibleTimer = 0;
            }
            else if (invincibleTimer > invincibleTime / 4.5f)
            {
                invincibleFreeze = false;
            }
            else
            {
                rb.velocity = rb.velocity * .99f;
            }
        }
        wallTimer += Time.deltaTime;
        if (wallJumpDirection != "")
        {
            wallJumpTimer += Time.deltaTime;
            if (wallJumpTimer > wallJumpTime)
            {
                wallJumpDirection = "";
                wallJumpTimer = 0;
                wallJumping = false;
            }
        }
        if (dashFreeze)
        {
            dashFreezeTimer += Time.deltaTime;
            if (diagDash)
            {
                if (dashFreezeTimer > diagDashFreezeTime)
                {
                    dashFreeze = false;
                    diagDash = false;
                    dashFreezeTimer = 0;
                    rb.gravityScale = 1.5f;
                }
                else if (dashFreezeTimer > diagDashFreezeTime / 2)
                {
                    rb.velocity = new Vector2(0, 0);
                }
            }
            else
            {
                if (dashFreezeTimer > dashFreezeTime)
                {
                    dashFreeze = false;
                    dashFreezeTimer = 0;
                    rb.gravityScale = 1.5f;
                }
                else if (dashFreezeTimer > dashFreezeTime / 2)
                {
                    rb.velocity = new Vector2(0, 0);
                }
            }
        }

        if (!dashFreeze && !invincibleFreeze)
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            Vector2 velocity = rb.velocity;
            velocity.x = moveX * moveSpeed;
            rb.velocity = velocity;
            if (moveX > 0)
            {
                rb.AddForce(new Vector2(5, 0));
                GetComponent<SpriteRenderer>().flipX = false;
                wallCheck.position = new Vector3(transform.position.x + .2f, transform.position.y, 0);
            }
            if (moveX < 0)
            {
                rb.AddForce(new Vector2(-5, 0));
                GetComponent<SpriteRenderer>().flipX = true;
                wallCheck.position = new Vector3(transform.position.x - .2f, transform.position.y, 0);
            }

            grounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, isGround);
            if (grounded || wall)
            {
                doubleJump = true;
                dash = true;
            }

            if (Input.GetKeyDown(KeyCode.Z) && grounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(new Vector2(0, 100 * jumpSpeed));
            }

            wall = Physics2D.OverlapCircle(wallCheck.position, checkRadius, isGround);
            if (wall)
            {
                wallTimer = 0;
            }
            if (((GetComponent<SpriteRenderer>().flipX == false && moveX > 0) || (GetComponent<SpriteRenderer>().flipX == true && moveX < 0)) && wallTimer < .1f && grounded == false && PlayerPrefs.GetInt("CanWallJump") == 1)
            {
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    wallJumping = true;
                }
            }
            if (wallJumping && GetComponent<SpriteRenderer>().flipX == true)
            {
                wallJumpDirection = "right";
            }
            else if (wallJumping)
            {
                wallJumpDirection = "left";
            }
            if (wallJumpDirection == "right")
            {
                rb.velocity = new Vector2(xWallForce, yWallForce);
            }
            else if (wallJumpDirection == "left")
            {
                rb.velocity = new Vector2(-xWallForce, yWallForce);
            }

            if (grounded || wall)
            {
                doubleJump = true;
            }
            if (!grounded && !wall && PlayerPrefs.GetInt("CanDoubleJump") == 1 && doubleJump && Input.GetKeyDown(KeyCode.Z))
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(new Vector2(0, 100 * jumpSpeed));
                doubleJump = false;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.LeftShift) && dash && !invincibleFreeze)
        {
            rb.gravityScale = 0;
            if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow))
            {
                rb.velocity = new Vector2(dashSpeed, dashSpeed);
                diagDash = true;
            }
            else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow))
            {
                rb.velocity = new Vector2(dashSpeed, -dashSpeed);
                diagDash = true;
            }
            else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow))
            {
                rb.velocity = new Vector2(-dashSpeed, dashSpeed);
                diagDash = true;
            }
            else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow))
            {
                rb.velocity = new Vector2(-dashSpeed, -dashSpeed);
                diagDash = true;
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                rb.velocity = new Vector2(0, dashSpeed);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                rb.velocity = new Vector2(0, -dashSpeed);
            }
            else if (Input.GetKey(KeyCode.RightArrow) || GetComponent<SpriteRenderer>().flipX == false)
            {
                rb.velocity = new Vector2(dashSpeed, 0);
            }
            else
            {
                rb.velocity = new Vector2(-dashSpeed, 0);
            }
            dash = false;
            dashFreeze = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer > 9 && !invincible)
        {
            invincible = true;
            invincibleFreeze = true;
            gameObject.layer = 9;
            if (collision.gameObject.transform.position.x > transform.position.x)
            {
                rb.velocity = new Vector2(-knockback, knockback);
            }
            else
            {
                rb.velocity = new Vector2(knockback, knockback);
            }
        }
    }
}