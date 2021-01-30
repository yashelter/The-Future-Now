using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBtn : MonoBehaviour
{
    private PlayerController pc;
    void Start()
    {
        pc = FindObjectOfType<PlayerController>();
    }

    public void OnClick()
    {
        pc.Attack();
    }
}
