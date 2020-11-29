using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public controlTypes controlType;
    public float moveSpeed;
    public float jumpForce;
    public Joystick movingJoystick;
    public Joystick attackJoystick;
    public Transform feetPos1;
    public Transform feetPos2;
    public LayerMask ground;

    [HideInInspector]
    public enum controlTypes{
        PC, Phone
    }
    private bool isRotated = false;
    private float minX = 0.4f;
    private float minY = 0.6f;

    private Animator playerAnimator;
    private Transform PlayerTransform;
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
        
    }


    private void Update()
    {
        float x = 0, y = 0;
        if(controlType == controlTypes.PC)
        {
            x = Input.GetAxis("Horizontal"); y = Input.GetAxis("Vertical");
        }else if(controlType == controlTypes.Phone)
        {
            x = movingJoystick.Horizontal; y = movingJoystick.Vertical;
        }
        Move(x, y);
    }
   
    private void Move(float x, float y)
    {
        if((x > 0 && isRotated) ||(x < 0 && !isRotated))
        {
            Rotate();
        }
        if(Mathf.Abs(x) > minX)
        {
            playerRB.velocity = new Vector3(x * moveSpeed, playerRB.velocity.y, 0);
        }
        if((Physics2D.OverlapCircle(feetPos1.position, .1f, ground) && y > minY) ||
            (Physics2D.OverlapCircle(feetPos2.position, .1f, ground) && y > minY))
        {
            playerRB.AddForce(new Vector3(0, jumpForce, 0));
            
        }

    }
    private void Rotate()
    {
        isRotated = !isRotated;
        PlayerTransform.Rotate(new Vector3(0, 180, 0));
    }
}
