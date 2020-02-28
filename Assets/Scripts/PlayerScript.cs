using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    // movement stats
    private float runSpeed = 145f;
    private float horizontalMove = 0f;
    private bool jump = false;

    // hp
    private float health = 100f;
    private float minHealth = 0f;
    private float maxHealth = 100f;
    private float hpLossRate = 0.1f;

    public float GetHpLossRate()
    {
        return hpLossRate;
    }

    public void SetHpLossRate(float value)
    {
        hpLossRate = value;
    }

    // Player Components
    public CharacterController2D controller;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal") * runSpeed;
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.deltaTime, false, jump);
        jump = false;
    }

    public void ChangeHealth(float rate)
    {
        health -= rate;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        else if (health < minHealth)
        {
            health = minHealth;
        }
    }

    public string GetHealth()
    {
        var healthInt = (int)health;
        return healthInt.ToString();
    }
}
