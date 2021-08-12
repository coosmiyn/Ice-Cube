using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{

    // movement stats
    private float horizontalMove = 0.0f;
    private bool jump = false;
    private float _health { get; set; } = 100;

    // public vars
    public float health { get {  return _health; } }
    public float hpLossRate { get; set; } = 0.1f;
    public bool isGrunded { get; set; } = false;
    public float runSpeed { get; set; } = 100.0f;
    public float defaultRunSpeed { get; } = 145.0f;

    public Joystick joystick;

    // Player Components
    public CharacterController2D controller;
    public Rigidbody2D RigidBody;

    // Update is called once per frame
    void Update()
    {
        if (joystick.Horizontal >= 0.2f)
            horizontalMove = joystick.Horizontal * runSpeed;
        else if (joystick.Horizontal <= 0.2f)
            horizontalMove = joystick.Horizontal * runSpeed;
        else
            horizontalMove = 0;

        //float verticalMove = joystick.Vertical;

        //if (verticalMove >= 0.35f)
        //{
        //    jump = true;
        //    isGrunded = false;
        //}
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.deltaTime, false, jump);
        jump = false;
    }

    public void ChangeHealth(float rate)
    {
        _health -= rate;
        _health = Mathf.Clamp(health, 0, 100);

        if (health == 0)
            Die();
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
    }

    // Return string HP for display
    public string GetHealth()
    {
        var healthInt = (int)health;
        return healthInt.ToString();
    }

    public void Die()
    {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

    public void Jump()
    {
        jump = true;
    }
}
