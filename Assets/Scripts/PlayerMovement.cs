//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerMovement : MonoBehaviour
//{
//    public CharacterController2D controller;

//    public Transform barrel;

//    [SerializeField]
//    float horizontalMove = 0f;
    
//    public Joystick joystick;

//    [SerializeField]
//    public float runSpeed = 40f;

//    bool jump = false;
//    bool crouch = false;
//    // Update is called once per frame
//    void Update()
//    {

//        if (joystick.Horizontal >= .2f)
//        {
//            horizontalMove = runSpeed;
//        }
//        else if (joystick.Horizontal <= -.2f)
//        {
//            horizontalMove = -runSpeed;
//        }
//        else
//        {
//            horizontalMove = 0f;
//        }

//        float verticalMove = joystick.Vertical;

//        if (verticalMove >= 0.6f)
//        {
//            jump = true;
//        }

//        if (verticalMove <= -0.3f)
//        {
//            crouch = true;
//        }
//        else
//        {
//            crouch = false;
//        }
//    }

//    private void FixedUpdate()
//    {
//        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
//        jump = false;
//    }
//}
