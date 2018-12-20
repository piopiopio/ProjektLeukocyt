using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private KeyCode moveUp = KeyCode.UpArrow;
    private KeyCode moveDown = KeyCode.DownArrow;
    private KeyCode moveRight = KeyCode.RightArrow;
    private KeyCode moveLeft = KeyCode.LeftArrow;

    private int speed = 10;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(moveUp))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
        }
        else if (Input.GetKey(moveDown))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
        }
        else
        {

            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }

        if (Input.GetKey(moveRight))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
        }
        else if (Input.GetKey(moveLeft))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);
        }
        else
        {

            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Destroy(col.gameObject);
            Debug.Log("Kolizja");
        }
    }
}
