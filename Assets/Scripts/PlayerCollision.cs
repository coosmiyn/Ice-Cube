using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using static Types;

public class PlayerCollision : MonoBehaviour
{
    // Stats
    [SerializeField] float regenRate = 2.0f;
    [SerializeField] float spikeDamage = 5.0f;
    [SerializeField] float spikeForceX = -250.0f;
    [SerializeField] float spikeForceY = 250.0f;
    [SerializeField] float spikeDisableTime = 2.0f;
    [SerializeField] float fireRegenTime = 2.0f;

    // Player Components
    GameObject Player;
    PlayerScript playerScript;
    Rigidbody2D RigidBody;
    CharacterController2D CharacterController;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerScript = Player.GetComponent<PlayerScript>();
        RigidBody = Player.GetComponent<Rigidbody2D>();
        CharacterController = Player.GetComponent<CharacterController2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If collides with spike add force and disable air control
        if (collision.gameObject.tag == "Spike")
        {
            if (!playerScript.isInvincible)
            {
                //// Add force and disable air control until it reaches the ground
                //RigidBody.velocity = Vector3.zero;
                //RigidBody.angularVelocity = 0.0f;
                ////PlayerScript.runSpeed = 0.0f;
                //CharacterController.m_AirControl = false;

                //playerScript.TakeDamage(spikeDamage);
                ////RigidBody.AddForce(new Vector2(spikeForceX, spikeForceY));
                //RigidBody.AddForce(new Vector2(spikeForceX, spikeForceY));
                ////StartCoroutine(Wait(0.1f));
                ///
                CharacterController.m_AirControl = false;
                playerScript.TakeDamage(spikeDamage);
                RigidBody.AddForce(new Vector2(0, spikeForceY));
                StartCoroutine(RegainAirControl(spikeDisableTime));
            }
        }
        else if (collision.gameObject.tag == "Ground")
        {
            playerScript.isGrunded = true;
            CharacterController.m_AirControl = true;
            //playerScript.runSpeed = playerScript.defaultRunSpeed;
        }
    }

    private void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
        CharacterController.m_AirControl = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Entered regen trigger, update HP regen
        switch(collision.tag)
        {
            case "RegenObject":
                playerScript.hpLossRate = (playerScript.hpLossRate * -1) * regenRate;
                break;
            case "PowerUpInvincible":
                StartCoroutine(playerScript.TriggerPowerUp(EPowerUps.Invincible, 5.0f));
                Destroy(collision.gameObject);
                break;
            case "PowerUpRegen":
                StartCoroutine(playerScript.TriggerPowerUp(EPowerUps.Regen, 5.0f));
                Destroy(collision.gameObject);
                break;
            case "Fire":
                playerScript.hpLossRate = playerScript.fireHpLossRate;
                StartCoroutine(RegainHPRegen(fireRegenTime));
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Exited regen trigger, update HP regen
        switch (collision.tag)
        {
            case "RegenObject":
                playerScript.hpLossRate = (playerScript.hpLossRate * -1) / regenRate;
                break;
            case "Fire":
                //playerScript.hpLossRate = playerScript.defaultHpLossRate;
                break;
        }
    }

    IEnumerator RegainAirControl(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        CharacterController.m_AirControl = true;
        CharacterController.m_AirControl = true;
    }

    IEnumerator RegainHPRegen(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        playerScript.hpLossRate = playerScript.defaultHpLossRate;
    }

    IEnumerator Wait(float value)
    {
        yield return new WaitForSeconds(value);
    }
}
