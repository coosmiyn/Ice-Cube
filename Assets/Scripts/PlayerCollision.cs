using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
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

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spike")
        {
            Debug.Log("hit spike");
            RigidBody.AddForce(new Vector2(-250, 250));
            CharacterController.m_AirControl = false;
            Timer timer = new Timer();
            timer.Interval = 2000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }
    }

    private void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
        CharacterController.m_AirControl = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerScript.SetHpLossRate((PlayerScript.GetHpLossRate() * -1) * 2.5f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerScript.SetHpLossRate((PlayerScript.GetHpLossRate() * -1) / 2.5f);
    }
}
