using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{
    private KeyCode moveUp = KeyCode.UpArrow;
    private KeyCode moveDown = KeyCode.DownArrow;
    private KeyCode moveRight = KeyCode.RightArrow;
    private KeyCode moveLeft = KeyCode.LeftArrow;
    private int speed = 10;
    public bool KillEnemyOn;
    public Text VirusesLeftText;
    private Vector3 ScaleIncrement;
    public GameObject RedEndLifeSprite;
    private AudioSource source;
    // Use this for initialization
    void Start()
    {
        float scale = 0.01f;
        ScaleIncrement=new Vector3(scale, scale, 0f);
        VirusesLeftText.text = "Viruses left: " + GameSetup.enemyQuantites.ToString();
        source = GetComponent<AudioSource>();
       RedEndLifeSprite.transform.position=new Vector3(0,(float)(-19.3+14.1 * GameSetup.enemyQuantites/GameSetup.maxEnemyQuantity), 0);
    }

    // Update is called once per frame
    void Update()
    {
        VirusesLeftText.text = "Viruses left: " + GameSetup.enemyQuantites.ToString();
        RedEndLifeSprite.transform.position = new Vector3(0, (float)(-19.3 + 14.1 * GameSetup.enemyQuantites / GameSetup.maxEnemyQuantity), 0);
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
        //col.contacts.First().normal;
        if (col.gameObject.tag == "Enemy")
        {
            if (KillEnemyOn)
            {
                Destroy(col.gameObject);
                GameSetup.enemyQuantites--;
                if( transform.localScale.x<=1 && transform.localScale.y <= 1)
                    transform.localScale += ScaleIncrement;
            }

            Debug.Log("Kolizja");
            source.Play();
        }
    }
}
