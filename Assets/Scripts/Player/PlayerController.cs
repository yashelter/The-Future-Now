﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : Entity
{
    public Button attackBtn;
    public Joystick movingJoystick;

    public controlTypes controlType;
    [HideInInspector]
    public enum controlTypes{
        PC, Phone
    }
    private AdsManager ads;

    [HideInInspector]
    public int locationId;
    public Canvas canvas;
    
    protected override void Start()
    {
        base.Start();
        Time.timeScale = 1;
        movingJoystick = FindObjectOfType<Joystick>();
        attackBtn = GameObject.FindGameObjectWithTag("AttackBtn").GetComponent<Button>();
        if (controlType == controlTypes.PC)
        {
            movingJoystick.gameObject.SetActive(false);
            attackBtn.gameObject.SetActive(false);
        }
        sword = GameObject.Find("Sword").GetComponent<Weapon>();
        locationId = SceneManager.GetActiveScene().buildIndex;
        ads = FindObjectOfType<AdsManager>();
    }


    protected override void FixedUpdate()
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
    
    private void Save()
    {
        SaveSystem.SavePlayer(this);
    }
    
    public override void GetDamage(int damage)
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
    public override void Death()
    {
        //Умирает, рестарт левла
        SceneManager.LoadScene(locationId);
    }
    public void ReturnAlive()
    {
        // анимация оживления, временное бессмертие
        ads.Show("rewardedVideo");
        Debug.Log("Show rewarded video");
        playerAnimator.SetTrigger("Reborn");
        attackBtn.gameObject.SetActive(true);
        movingJoystick.gameObject.SetActive(true);
    }
    public bool GetLife()
    {
        return playerAnimator.GetBool("Alive");
    }
}
