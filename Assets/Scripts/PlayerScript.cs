using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Types;

public class PlayerScript : MonoBehaviour
{
    // movement stats
    private float horizontalMove = 0.0f;
    private bool jump = false;

    // public vars
    public float health { get {  return _health; } }
    [SerializeField] public float hpLossRate = 0.1f;
    [SerializeField] public float fireHpLossRate = 0.15f;
    public float defaultHpLossRate = 0.1f;
    public bool isGrunded { get; set; } = false;
    public float runSpeed = 150.0f;
    [SerializeField] public float defaultRunSpeed { get { return _defaultRunSpeed; } set { _defaultRunSpeed = value; } }
    [SerializeField] public float _defaultRunSpeed = 145.0f;

    // power ups
    public bool isInvincible { get; set; } = false;
    public bool canLoseHealth { get; set; } = true;
    public float _defaultHitPoints { get { return defaultHitPoints; } set { defaultHitPoints = value; } }
    [SerializeField] private float defaultHitPoints = 1.0f;
    private float defaultRandomMax { get; } = 80.0f;
    [SerializeField] private float hitPoints = 1.0f;
    public float _randomMax { get { return randomMax; } set { randomMax = value; } }
    [SerializeField] private float randomMax { get; set; } = 80.0f;
    private float regenPoints { get; set; } = 0.0f;
    private bool _canSpawnRegenPowerUp { get; set; } = true;

    // private vars
    private float _health { get; set; } = 100;

    public Joystick joystick;

    // Player Components
    public CharacterController2D controller;
    public Rigidbody2D RigidBody;

    public PowerUpSpawnerScript powerUpSpawnerScript;

    private void Start()
    {
        defaultHitPoints = _defaultHitPoints;
        defaultRunSpeed = _defaultRunSpeed;
        runSpeed = defaultRunSpeed;
        hitPoints = defaultHitPoints;
        randomMax = defaultRandomMax;
    }

    // Update is called once per frame
    void Update()
    {
        //if (joystick.Horizontal >= 0.2f)
        //    horizontalMove = joystick.Horizontal * runSpeed;
        //else if (joystick.Horizontal <= 0.2f)
        //    horizontalMove = joystick.Horizontal * runSpeed;
        //else
        //    horizontalMove = 0;

        horizontalMove = Input.GetAxis("Horizontal") * runSpeed;
        jump = Input.GetKey("space");

        //float verticalMove = joystick.Vertical;

        //if (verticalMove >= 0.35f)
        //{
        //    jump = true;
        //    isGrunded = false;
        //}
    }

    void FixedUpdate()
    {
        //horizontalMove = Input.GetAxis("Horizontal") * runSpeed;
        //jump = Input.GetKey("space");
        controller.Move(horizontalMove * Time.deltaTime, false, jump);
        jump = false;
    }

    public void ChangeHealth(float rate)
    {
        if (canLoseHealth)
        {
            _health -= rate;
            _health = Mathf.Clamp(health, 0, 100);

            //if (health <= 0)
            //    Die();
        }

        if (rate > 0 && _health < 75 && _canSpawnRegenPowerUp)
        {
            regenPoints += .1f;
        }

        if (rate > 0 && _health > 85 && regenPoints > 0 && _canSpawnRegenPowerUp)
        {
            regenPoints -= .075f;
        }

        if (regenPoints > 25.0f && _canSpawnRegenPowerUp)
        {
            _canSpawnRegenPowerUp = false;
            powerUpSpawnerScript.SpawnPowerUp(EPowerUps.Regen);
        }
    }

    public void TakeDamage(float damage)
    {
        float randomNumber = Random.Range(5, 50);
        hitPoints += hitPoints + (randomNumber / 2);
        randomMax *= 0.9f;
        if (randomMax <= hitPoints)
        {
            randomMax = defaultRandomMax;
            hitPoints = defaultHitPoints;
            powerUpSpawnerScript.SpawnPowerUp(EPowerUps.Invincible);
        }
        _health -= damage;
        if (health < 0)
            Die();
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

    public void TriggerPowerUp2(EPowerUps power, float time)
    {
        switch (power)
        {
            case EPowerUps.Invincible:
                isInvincible = true;
                isInvincible = false;
                break;
            case EPowerUps.Regen:
                canLoseHealth = false;
                canLoseHealth = true;
                break;
        }
    }

    public IEnumerator TriggerPowerUp(EPowerUps power, float value)
    {
        switch (power)
        {
            case EPowerUps.Invincible:
                isInvincible = true;
                Debug.Log("Starting");
                yield return new WaitForSeconds(value);
                Debug.Log("Ending");
                isInvincible = false;
                break;
            case EPowerUps.Regen:
                canLoseHealth = false;
                Debug.Log("Starting");
                yield return new WaitForSeconds(value);
                Debug.Log("Ending");
                canLoseHealth = true;
                break;
            default:
                yield return null;
                break;
        }
    }

    public IEnumerator ChangeBool()
    {
        yield return new WaitForSeconds(10.0f);
        _canSpawnRegenPowerUp = true;
    }
}
