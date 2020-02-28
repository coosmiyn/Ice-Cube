using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Camera stats
    private float cameraSpeed = 6f;

    // Camera components
    private Transform Transform;

    // Start is called before the first frame update
    void Start()
    {
        Transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Transform.Translate(Vector3.right * Time.deltaTime * cameraSpeed);
    }


}
