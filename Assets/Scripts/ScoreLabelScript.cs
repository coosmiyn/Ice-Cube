using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreLabelScript : MonoBehaviour
{
    // Player components
    public GameObject gameManager;
    public GameManager managerScript;

    //Text label components
    public Text ScoreText;
    // Start is called before the first frame update
    void Start()
    {
        //gameManager = GameObject.FindGameObjectWithTag("GameManager");
        //managerScript = gameManager.GetComponent<GameManager>();
        //ScoreText = GetComponent<Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ScoreText.text = managerScript.GetScore();
    }
}
