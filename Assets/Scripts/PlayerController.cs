using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public controlTypes controlType;
    public float moveSpeed;
    public float jumpForce;
    [HideInInspector]
    public enum controlTypes{
        PC, Phone
    }

    private Animator playerAnimator;
    private Transform PlayerTransform;
    private Rigidbody2D playerRB;

    private void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }


    private void Update()
    {
        if(controlType == controlTypes.PC)
        {
            PCMove();
        }else if(controlType == controlTypes.Phone)
        {

        }
    }
    private void PCMove()
    {
        playerRB.velocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, playerRB.velocity.y, 0);
    }
}
