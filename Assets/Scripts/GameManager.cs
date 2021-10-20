using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public float scoringRate;
    private float score = 0.0f;

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

        score += scoringRate;
    }

    // Return string HP for display
    public string GetScore()
    {
        var scoreInt = (int)score;
        return scoreInt.ToString();
    }
}
