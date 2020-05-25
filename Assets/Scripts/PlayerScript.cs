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
    private float minHealth = 0f;
    private float maxHealth = 100f;
    private float _health = 100f;
    private float _hpLossRate = 0.1f;

    // public vars
    public float health { get { return _health; } set { _health = value; } }
    public float hpLossRate { get { return _hpLossRate; } set { _hpLossRate = value; } }

    // Player Components
    public CharacterController2D controller;

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
        _health -= rate;
        if (_health > maxHealth)
        {
            _health = maxHealth;
        }
        else if (_health < minHealth)
        {
            _health = minHealth;
        }
    }

    // Return string HP for display
    public string GetHealth()
    {
        var healthInt = (int)health;
        return healthInt.ToString();
    }
}
