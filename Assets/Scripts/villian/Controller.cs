using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float eyeRange = 10f;
    private Transform playerTransform;
    private Transform position;

    private Rigidbody2D shape;
    private Animator animator;

    void Start()
    {
        shape = GetComponent<Rigidbody2D>();
        position = GetComponent<Transform>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); ;
    }

    void FixedUpdate()
    {
        if(Mathf.Abs(playerTransform.position.x) < eyeRange + position.position.x)
        {
            shape.AddForce(new Vector2(-50, 0));
        }
    }

}
