using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public JoystickPrototype movement;
    public float playerSpeed = 5.0f;
    public Rigidbody2D rb;


    void FixedUpdate()
    {
        if (movement.joystickVector.y != 0)
        {
            rb.velocity = new Vector2(movement.joystickVector.x * playerSpeed, movement.joystickVector.y * playerSpeed);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}
