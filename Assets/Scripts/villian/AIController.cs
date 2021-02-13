using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Entity
{
    // playerTransform - Transmorm текущей штуки - public
    public float warriorEyeRange = 25f;
    public float attackVision = 7f;

    public Transform maxPosLeft;
    public Transform maxPosRight;

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
        if (targetTransform.gameObject.GetComponent<PlayerController>().GetLife())
        {
            if (AIType == AITypes.Warrior)
            {
                WarriorBrain();
            }
            else
            {
                DefenderBrain();
            }
        }
        else
        {
            // если герой умер, предпринимать действия тк 
            // 1 - он может реснуться после рекламы
            // 2 - игрок увидет действия
        }
    }
    protected void DefenderBrain()
    {
        // заглушка
        WarriorBrain();
    }
    protected void WarriorBrain()
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
        if(mod != 0 && (gameObject.transform.position.x + moveSpeed * mod < maxPosRight.position.x &&
                        gameObject.transform.position.x + moveSpeed * mod > maxPosLeft.position.x))
        {
            Move(moveSpeed * mod, 0);
        }
        else
        {
            Move(0, 0);
        }
        
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
