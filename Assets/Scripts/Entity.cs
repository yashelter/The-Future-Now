using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Entity : MonoBehaviour, Damageable
{
    public Weapon sword;
    public float moveSpeed;
    public float jumpForce;
    public float landMultiply;

    public Button attackBtn;

    public Joystick movingJoystick;

    public Transform feetPos1;
    public Transform feetPos2;
    public LayerMask ground;
    [HideInInspector]

    protected bool isRunning = false;
    protected bool isRotated = false;
    protected bool isJumping = false;
    protected bool isLanding = false;

    protected float timer = 0f;
    protected float timerTime = 0.5f;
    protected float minX = 0.4f;
    protected float minY = 0.6f;

    protected Animator playerAnimator;
    [HideInInspector]
    public Transform PlayerTransform;
    protected Rigidbody2D playerRB;


    private void Start()
    {
        Time.timeScale = 1;
       
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        PlayerTransform = GetComponent<Transform>();
    }


    private void FixedUpdate()
    {
        float x = 0, y = 0;
        if (playerAnimator.GetBool("Alive"))
        {
            Move(x, y);
        }
    }
    private void Move(float x, float y)
    {
        bool grounded = Physics2D.OverlapCircle(feetPos1.position, 1.7f, ground) ||
                        Physics2D.OverlapCircle(feetPos2.position, 1.7f, ground);

        if ((Mathf.Abs(x) > minX && !isRunning) || (Mathf.Abs(x) < minX && isRunning))
        {
            isRunning = !isRunning;
            playerAnimator.SetBool("isRunning", isRunning);
        }
        if ((x > 0 && isRotated) || (x < 0 && !isRotated))
        {
            Rotate();
        }
        if (Mathf.Abs(x) > minX)
        {
            if (isJumping || isLanding)
            {
                playerRB.velocity = new Vector3((x * moveSpeed) / landMultiply, playerRB.velocity.y, 0);
            }
            else
            {
                playerRB.velocity = new Vector3(x * moveSpeed, playerRB.velocity.y, 0);
            }
        }

        if (isJumping && timer <= 0)
        {
            timer = 0;
            if (grounded)
            {
                isJumping = false;
                playerAnimator.SetBool("Jumping", !isJumping);
            }
        }
        else if (timer == 0 && (grounded && y > minY))
        {
            playerRB.velocity = new Vector3(playerRB.velocity.x, jumpForce, 0);
            isJumping = true;
            playerAnimator.SetTrigger("Jump");
            playerAnimator.SetBool("Jumping", !isJumping);
            timer = timerTime;
        }
        else if (!isJumping && !isLanding && Mathf.Abs(playerRB.velocity.y) > 5f && !(grounded))
        {

            isLanding = !isLanding;
            playerAnimator.SetTrigger("Land");
            playerAnimator.SetBool("Landed", !isLanding);
        }
        else if (isLanding && grounded)
        {
            isLanding = !isLanding;
            playerAnimator.SetBool("Landed", !isLanding);
        }

    }
    private void Rotate()
    {
        isRotated = !isRotated;
        PlayerTransform.Rotate(new Vector3(0, 180, 0));
    }
    public void Attack()
    {
        if (!sword.inCombat)
        {
            sword.inCombat = true;
            playerAnimator.SetTrigger("Attack");
        }
    }
    public void EndAttack()
    {
        sword.inCombat = false;
    }
    public void GetDamage(int damage)
    {
        // infinity hp???
    }
    public void Death()
    {
        Destroy(gameObject);
    }

}
