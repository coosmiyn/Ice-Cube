using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Camera stats
    [SerializeField] private float cameraSpeed = 6f;
    [SerializeField] private float safeZone = 10.0f;

    // Camera components
    [SerializeField] private Camera MainCamera;
    private Transform Transform;

    // Player components
    GameObject Player;
    PlayerScript PlayerScript;
    [SerializeField] MilkShake.Shaker shake;
    [SerializeField] MilkShake.ShakePreset ShakePreset;

    float cameraWidth;


    // Start is called before the first frame update
    void Start()
    {
        MainCamera.enabled = true;
        Transform = MainCamera.GetComponent<Transform>();
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerScript = Player.GetComponent<PlayerScript>();
        cameraWidth = Camera.main.orthographicSize * 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Camera movement
        Transform.Translate(Vector3.right * Time.deltaTime * cameraSpeed);

        float deletePosition = (Transform.position.x - (cameraWidth / 2) - safeZone);
        if (Player.transform.position.x < deletePosition)
        {
            PlayerScript.Die();
        }
    }


}
