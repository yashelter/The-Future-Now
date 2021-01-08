using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float eyeRange = 5f;
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
        if(playerTransform.position.x < position.position.x  &&
            position.position.x < playerTransform.position.x + eyeRange)
        {
            Debug.Log("I coming");
        }
    }

}
