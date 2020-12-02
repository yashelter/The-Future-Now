using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    public controlTypes controlType;
    public float moveSpeed;
    public float jumpForce;
    public float landMultiply;
    
        
    public Joystick movingJoystick;
    public Joystick attackJoystick;
    public Transform feetPos1;
    public Transform feetPos2;
    public LayerMask ground;

    [HideInInspector]
    public enum controlTypes{
        PC, Phone
    }
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
        if(controlType == controlTypes.PC)
        {
            movingJoystick.gameObject.SetActive(false);
            attackJoystick.gameObject.SetActive(false);
        }
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        PlayerTransform = GetComponent<Transform>();
        locationId = SceneManager.GetActiveScene().buildIndex;
    }


    private void FixedUpdate()
    {
        float x = 0, y = 0;
        if(controlType == controlTypes.PC)
        {
            x = Input.GetAxis("Horizontal"); y = Input.GetAxis("Vertical") > 0 ? 1 : 0; ;
        }else if(controlType == controlTypes.Phone)
        {
            x = movingJoystick.Horizontal; y = movingJoystick.Vertical;
        }
        Move(x, y);
    }
   
    private void Move(float x, float y)
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if(Mathf.Abs(x) > minX && !isRunning)
        {
            isRunning = !isRunning;
            playerAnimator.SetBool("isRunning", isRunning);
        }
        else if(Mathf.Abs(x) < minX && isRunning)
        {
            isRunning = !isRunning;
            playerAnimator.SetBool("isRunning", isRunning);
        }
        if((x > 0 && isRotated) ||(x < 0 && !isRotated))
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
            if(Physics2D.OverlapCircle(feetPos1.position, .06f, ground))
            {
                isJumping = false;
                playerAnimator.SetBool("Jumping", true);
            }
        }
        else if(((Physics2D.OverlapCircle(feetPos1.position, .1f, ground) && y > minY) ||
            (Physics2D.OverlapCircle(feetPos2.position, .1f, ground) && y > minY)) && timer == 0)
        {
            playerRB.velocity = new Vector3(playerRB.velocity.x, jumpForce, 0);
            isJumping = true;
            playerAnimator.SetTrigger("Jump");
            playerAnimator.SetBool("Jumping", false);
            timer = timerTime;
        }
        else if(!(Physics2D.OverlapCircle(feetPos2.position, .3f, ground) &&
            (Physics2D.OverlapCircle(feetPos1.position, .3f, ground)))
             && Mathf.Abs(playerRB.velocity.y) > 5f && !isJumping && !isLanding)
        {
            
            isLanding = !isLanding;
            playerAnimator.SetTrigger("Land");
            playerAnimator.SetBool("Landed", !isLanding);
        }
        else if((Physics2D.OverlapCircle(feetPos2.position, .1f, ground) || 
            (Physics2D.OverlapCircle(feetPos1.position, .1f, ground))) && isLanding)
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
}
