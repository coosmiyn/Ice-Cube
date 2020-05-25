using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Player Components
    GameObject Player;
    PlayerScript PlayerScript;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerScript = Player.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Health drainer
        PlayerScript.ChangeHealth(PlayerScript.hpLossRate);
    }
}
