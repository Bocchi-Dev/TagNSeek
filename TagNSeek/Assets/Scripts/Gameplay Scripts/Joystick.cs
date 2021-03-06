using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Joystick : MonoBehaviour
{
    public Transform player;
    public float speed = 5.0f;

    private bool touchStart = false;
    private Vector2 pointA;
    private Vector2 pointB;

    //Joysticks
    public Transform innerCircle;
    public Transform outerCircle;

    void Update()
    {
        //calculate direction
        if (Input.GetMouseButtonDown(0))
        {
            pointA = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z);
            
            if(Input.mousePosition.x < Screen.width/2)
            {
                innerCircle.transform.position = pointA * -1;
                outerCircle.transform.position = pointA * 1;

                innerCircle.GetComponent<Image>().enabled = true;
                outerCircle.GetComponent<Image>().enabled = true;
            }
            
        }

        if (Input.GetMouseButton(0))
        {
            if(Input.mousePosition.x < Screen.width / 2)
            {
                touchStart = true;
                pointB = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z);
            }
        }
        else
        {
            touchStart = false;
        }
    }


    private void FixedUpdate()
    {
        //move character
        if (touchStart)
        {
            Vector2 offset = pointB - pointA;
            Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);
            moveCharacter(direction * 1);

            innerCircle.transform.position = new Vector2(pointA.x + direction.x, pointA.y + direction.y) * 1;
        }
        else
        {
            innerCircle.GetComponent<Image>().enabled = false;
            outerCircle.GetComponent<Image>().enabled = false;
        }
    }

    //move character function
    void moveCharacter(Vector2 direction)
    {
        player.Translate(direction * speed * Time.deltaTime);
    }
}
