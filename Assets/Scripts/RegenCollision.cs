using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenCollision : MonoBehaviour
{
    // Regen components
    GameObject regen;
    Transform transform;

    // Start is called before the first frame update
    void Start()
    {
        regen = GameObject.FindGameObjectWithTag("RegenObject");
        transform = regen.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spike")
        {
            try
            {
                Debug.Log("Collision on spike. Fixing");
                float x = transform.position.x;
                x--;
                float y = transform.position.y;
                transform.position = new Vector3(x, y, 1);
            }
            catch (Exception ex)
            { };
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        try
        {
            if (collision.gameObject.tag == "Spike")
            {
                Debug.Log("Collision on spike. Fixing");
                float x = transform.position.x;
                x--;
                float y = transform.position.y;
                transform.position = new Vector3(x, y, 1);
            }
        }
        catch (Exception ex)
        { };
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        try
        {
            Debug.Log("Collision on spike. Fixing");
            float x = transform.position.x;
            x--;
            float y = transform.position.y;
            transform.position = new Vector3(x, y, 1);
        }
        catch (Exception ex) 
        { };
    }
}
