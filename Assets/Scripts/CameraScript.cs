using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Camera stats
    [SerializeField] private float cameraSpeed = 6f;
    [SerializeField] private float safeZone = 10.0f;

    // Camera components
    private Transform Transform;

    // Player components
    GameObject Player;
    PlayerScript PlayerScript;

    float cameraWidth;


    // Start is called before the first frame update
    void Start()
    {
        Transform = GetComponent<Transform>();
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerScript = Player.GetComponent<PlayerScript>();
        cameraWidth = Camera.main.orthographicSize * 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Camera movement
        Transform.Translate(Vector3.right * Time.deltaTime * cameraSpeed);

        float deletePosition = (transform.position.x - (cameraWidth / 2) - safeZone);
        if (Player.transform.position.x < deletePosition)
        {
            PlayerScript.Die();
        }
    }


}
