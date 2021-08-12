using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    // Stats
    [SerializeField]
    float regenRate = 2.0f;
    [SerializeField]
    float spikeDamage = 5.0f;
    [SerializeField]
    float spikeForceX = -250.0f;
    [SerializeField]
    float spikeForceY = 250.0f;

    // Player Components
    GameObject Player;
    PlayerScript PlayerScript;
    Rigidbody2D RigidBody;
    CharacterController2D CharacterController;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerScript = Player.GetComponent<PlayerScript>();
        RigidBody = Player.GetComponent<Rigidbody2D>();
        CharacterController = Player.GetComponent<CharacterController2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If collides with spike add force and disable air control
        if (collision.gameObject.tag == "Spike")
        {
            // Add force and disable air control until it reaches the ground
            RigidBody.velocity = Vector3.zero;
            RigidBody.angularVelocity = 0.0f;
            //PlayerScript.runSpeed = 0.0f;
            CharacterController.m_AirControl = false;

            PlayerScript.TakeDamage(spikeDamage);
            RigidBody.AddForce(new Vector2(spikeForceX, spikeForceY));
            //StartCoroutine(Wait(0.1f));
        }
        else if (collision.gameObject.tag == "Ground")
        {
            PlayerScript.isGrunded = true;
            CharacterController.m_AirControl = true;
            PlayerScript.runSpeed = PlayerScript.defaultRunSpeed;
        }
    }

    private void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
        CharacterController.m_AirControl = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Entered regen trigger, update HP regen
        PlayerScript.hpLossRate = (PlayerScript.hpLossRate * -1) * regenRate;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Exited regen trigger, update HP regen
        PlayerScript.hpLossRate = (PlayerScript.hpLossRate * -1) / regenRate;
    }

    IEnumerator Wait(float value)
    {
        yield return new WaitForSeconds(value);
    }
}
