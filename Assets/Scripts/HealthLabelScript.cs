using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthLabelScript : MonoBehaviour
{
    // Player components
    GameObject Player;
    PlayerScript PlayerScript;

    //Text label components
    Text HealthText;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerScript = Player.GetComponent<PlayerScript>();
        HealthText = GetComponent<Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Health display
        HealthText.text = PlayerScript.GetHealth();
    }
}
