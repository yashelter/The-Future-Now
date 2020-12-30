using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, Damageable
{
    public Weapon sword;
    public controlTypes controlType;
    public float moveSpeed;
    public float jumpForce;
    public float landMultiply;

    public Button attackBtn;

    public Joystick movingJoystick;
    
    public Transform feetPos1;
    public Transform feetPos2;
    public LayerMask ground;
    [HideInInspector]
    public enum controlTypes{
        PC, Phone
    }
    private AdsManager ads;

    private bool isRunning = false;
    private bool isRotated = false;
    private bool isJumping = false;
    private bool isLanding = false;

    private float timer = 0f;
    private float timerTime = 0.5f;
    private float minX = 0.4f;
    private float minY = 0.6f;
    
    [HideInInspector]
    public int locationId;
    private Animator playerAnimator;
    [HideInInspector]
    public Transform PlayerTransform;
    private Rigidbody2D playerRB;
    
    
    private void Start()
    {
        Time.timeScale = 1;
        if(controlType == controlTypes.PC)
        {
            movingJoystick.gameObject.SetActive(false);
            attackBtn.gameObject.SetActive(false);
        }
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        PlayerTransform = GetComponent<Transform>();
        sword = GameObject.Find("Sword").GetComponent<Weapon>();
        locationId = SceneManager.GetActiveScene().buildIndex;
        ads = FindObjectOfType<AdsManager>();
    }


    private void FixedUpdate()
    {
        if (playerAnimator.GetBool("Alive"))
        {
            float x = 0, y = 0;
            if (controlType == controlTypes.PC)
            {
                x = Input.GetAxis("Horizontal"); y = Input.GetAxis("Vertical") > 0 ? 1 : 0; ;
            }
            else if (controlType == controlTypes.Phone)
            {
                x = movingJoystick.Horizontal; y = movingJoystick.Vertical;
            }
            // timer for jump
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            // all player movements

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
        if ((x > 0 && isRotated) ||(x < 0 && !isRotated))
        {
            Rotate();
        }
        if(Mathf.Abs(x) > minX)
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
            if(grounded)
            {
                isJumping = false;
                playerAnimator.SetBool("Jumping", !isJumping);
            }
        }
        else if(timer == 0 && (grounded && y > minY))
        {
            playerRB.velocity = new Vector3(playerRB.velocity.x, jumpForce, 0);
            isJumping = true;
            playerAnimator.SetTrigger("Jump");
            playerAnimator.SetBool("Jumping", !isJumping);
            timer = timerTime;
        }
        else if(!isJumping && !isLanding && Mathf.Abs(playerRB.velocity.y) > 5f && !(grounded))
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
    private void Save()
    {
        SaveSystem.SavePlayer(this);
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
        OnDeath();
    }
    public void OnDeath()
    {
        //если чел хочет, смотрит рекламу
        playerAnimator.SetTrigger("Death");
        playerAnimator.SetBool("Alive", false);
        attackBtn.gameObject.SetActive(false);
        movingJoystick.gameObject.SetActive(false);
    }
    public void Death()
    {
        //Умирает, рестарт левела
        ads.Show("rewardedVideo");
        SceneManager.LoadScene(locationId);
    }
    public void ReturnAlive()
    {
        // анимация оживления, временное бессмертие
        attackBtn.gameObject.SetActive(true);
        movingJoystick.gameObject.SetActive(true);
    }
}
