using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region variables
    public static PlayerMovement instance;
    [HideInInspector] public float horizontal;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [HideInInspector] public Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask water;
    [SerializeField] private LayerMask death;
    [HideInInspector] public Transform groundCheck;
    [HideInInspector] public bool canDoubleJump = false;
    [HideInInspector] public float jumpCountdown = 0f;
    [HideInInspector] public float dashCountdown = 1.5f;
    [HideInInspector] public float dashTime = 0.2f;
    [HideInInspector] public float coyoteTime = 0.2f;
    [HideInInspector] public Vector2 velocity;
    [SerializeField] private float accelerationForce;

    [HideInInspector] public bool inWater;
    [HideInInspector] public Vector2 checkpoint;

    private float horizontalVelocity;
    private bool input;
    
    [HideInInspector] public KeyCode[] keys = {KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D, KeyCode.Space, KeyCode.C};
    #endregion

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        rb = GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("GroundChecker");
        checkpoint = transform.position;
        string[] keyString = PlayerPrefs.GetString("Controls").Split(",");
        for (int i = 0; i < keyString.Length; i++)
        {
            keys[i] = (KeyCode)Enum.Parse(typeof(KeyCode), keyString[i]);
        }
    }

    private void Update()
    {
        #region horizontal
        bool left = Input.GetKey(keys[2]);
        bool right = Input.GetKey(keys[3]);
        if (left != right)
        {
            if (left)
                horizontal = -1;
            else
                horizontal = 1;
        }
        else
        {
            horizontal = 0;
        }
        #endregion
        if (horizontal != 0)
            Timer.instance.StartTimer();

        if (!InWater())
        {
            rb.gravityScale = 4f;
            if (Input.GetKeyDown(keys[0]) && jumpCountdown <= 0)
            {
                float multiplier = 1;
                if (IsGrounded() || coyoteTime > 0)
                {
                    canDoubleJump = true;
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce * multiplier);
                    jumpCountdown = 0.1f;
                }
                else if (canDoubleJump)
                {
                    canDoubleJump = false;
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce / 1.2f * multiplier);
                    jumpCountdown = 0.1f;
                }
            }
    
            if (Input.GetKeyDown(keys[4]) && dashCountdown <= 0)
            {
                dashTime = 0.2f;
                dashCountdown = 1.5f;
            }
    
            if (Input.GetKeyUp(keys[0]) && rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.7f);
            }
    
            if (!canDoubleJump && IsGrounded())
            {
                canDoubleJump = true;
            }
    
            if (IsGrounded())
            {
                coyoteTime = 0.2f;
            }
            else
            {
                coyoteTime -= Time.deltaTime;
            }
            
            dashTime = Mathf.Clamp(dashTime, 0, 0.2f);
            jumpCountdown -= Time.deltaTime;
            dashCountdown -= Time.deltaTime;
            dashTime -= Time.deltaTime;
        }
        else
        {
            rb.gravityScale = 0.1f;
            if (Input.GetKey(keys[0]))
            {
                rb.velocity = new Vector2(rb.velocity.x, 10);
                input = true;
            }
            else if (Input.GetKey(keys[1]))
            {
                rb.velocity = new Vector2(rb.velocity.x, -10);
                input = true;
            }
            else
            {
                input = false;
            }
            
            if (Input.GetKeyDown(keys[4]) && dashCountdown <= 0)
            {
                dashTime = 1f;
                dashCountdown = 1.5f;
            }
            
            dashCountdown -= Time.deltaTime;
            dashTime -= Time.deltaTime;
        }

        if (IsDead())
        {
            transform.position = checkpoint;
        }
    }

    private bool IsDead()
    {
        if (Physics2D.OverlapCircle(transform.position, 0.4f, death))
        {
            Achievements.instance.AddDeath();
            return true;
        }

        return false;
    }

    private bool IsGrounded()
    {
        foreach (Transform child in gameObject.transform)
        {
            if (Physics2D.OverlapCircle(child.position, 0.2f, groundLayer))
                return true;
        }

        return false;
    }

    private bool InWater()
    {
        if (Physics2D.OverlapCircle(transform.position, 0.4f, water))
        {
            return true;
        }

        return false;
    }

    private void FixedUpdate()
    {
        if (!InWater())
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -30, 20));
            velocity = rb.velocity;
            float multiplier = 1;
            if (dashTime > 0)
            {
                multiplier = 3;
            }
            if (horizontalVelocity < 0 && horizontal > 0 || horizontalVelocity > 0 && horizontal < 0)
            {
                if (horizontalVelocity > 1 || horizontalVelocity < -1)
                {
                    horizontalVelocity *= 5 *  Time.deltaTime;
                }
                else
                {
                    horizontalVelocity = 0;
                }
            }
            else if (horizontal == 0)
            {
                if (horizontalVelocity > 1 || horizontalVelocity < -1)
                {
                    horizontalVelocity *= 0.9f * Time.deltaTime;
                }
                else
                {
                    horizontalVelocity = 0;
                }
            }
            else if (Mathf.RoundToInt(horizontal) == 1)
            {
                horizontalVelocity = horizontalVelocity * Time.deltaTime * accelerationForce + 0.1f;
            }
            else if (Mathf.RoundToInt(horizontal) == -1)
            {
                horizontalVelocity = horizontalVelocity * Time.deltaTime * accelerationForce - 0.1f;
            }
            rb.velocity = new Vector2(horizontalVelocity * speed * multiplier, rb.velocity.y);
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -7.5f * multiplier, 7.5f * multiplier), Mathf.Clamp(rb.velocity.y, -30, 20));
            inWater = false;
        }
        else
        {
            float multiplier = 1;
            if (dashTime > 0)
            {
                multiplier = 2;
            }
            rb.velocity = new Vector2(horizontal * 10, Mathf.Clamp(rb.velocity.y * 0.9f, -10, 10));
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -5 * multiplier, 5 * multiplier), Mathf.Clamp(rb.velocity.y, -5 * multiplier, 5 * multiplier));
            velocity = rb.velocity;
            inWater = true;
        }
    }
} 