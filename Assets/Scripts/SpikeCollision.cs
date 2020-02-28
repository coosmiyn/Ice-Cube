using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "RegenObject")
        {
            try
            {
                Debug.Log("Collision on spike. Fixing");
                float x = collision.gameObject.transform.position.x;
                x--;
                float y = collision.gameObject.transform.position.y;
                collision.gameObject.transform.position = new Vector3(x, y, 1);
            }
            catch (Exception ex)
            { };
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "RegenObject")
        {
            try
            {
                Debug.Log("Collision on spike. Fixing");
                float x = collision.gameObject.transform.position.x;
                x--;
                float y = collision.gameObject.transform.position.y;
                collision.gameObject.transform.position = new Vector3(x, y, 1);
            }
            catch (Exception ex) 
            { };
        }
    }
}
