using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Entity
{
    // playerTransform - Transmorm текущей штуки - public
    public float warriorEyeRange = 25f;
    public float attackVision = 7f;
    protected Transform targetTransform;
    public AITypes AIType;
    protected float offset = 6f;
    public enum AITypes
    {
        Warrior, Defender
    }

    protected override void Start()
    {
        base.Start();
        targetTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
    }
    protected override void FixedUpdate()
    {
        if (AIType == AITypes.Warrior)
        {
            warriorBrain();
        }
        else
        {
            defenderBrain();
        }
    }
    protected void defenderBrain()
    {
        // меняем тут
        int mod = 0;
        if (targetTransform.position.x < playerTransform.position.x &&
            playerTransform.position.x < targetTransform.position.x + warriorEyeRange)
        {
            mod = -1;
        }
        else if (targetTransform.position.x > playerTransform.position.x &&
            playerTransform.position.x + warriorEyeRange > targetTransform.position.x)
        {
            mod = 1;
        }
        Move(moveSpeed * mod, 0);
    }
    protected void warriorBrain()
    {
        // меняем тут
        int mod = 0;
        if(targetTransform.position.x - offset < playerTransform.position.x &&
            playerTransform.position.x < targetTransform.position.x + offset)
        {
            mod = 0;
            Attack();
        }
        else if (targetTransform.position.x < playerTransform.position.x &&
            playerTransform.position.x < targetTransform.position.x + warriorEyeRange)
        {
            mod = -1;
            CheckAttack();
        }
        else if (targetTransform.position.x > playerTransform.position.x &&
            playerTransform.position.x + warriorEyeRange > targetTransform.position.x)
        {
            mod = 1;
            CheckAttack();
        }
        Move(moveSpeed * mod, 0);
    }
    protected void CheckAttack()
    {
        if (targetTransform.position.x < playerTransform.position.x &&
            playerTransform.position.x < targetTransform.position.x + attackVision)
        {
            Attack();
        }
        else if (targetTransform.position.x > playerTransform.position.x &&
            playerTransform.position.x + attackVision > targetTransform.position.x)
        {
            Attack();
        }
        
    }

}
