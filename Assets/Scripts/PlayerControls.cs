using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{
    const double MaxScale = 2;
    private Rigidbody2D thisRigidBody;
    private KeyCode moveUp = KeyCode.UpArrow;
    private KeyCode moveDown = KeyCode.DownArrow;
    private KeyCode moveRight = KeyCode.RightArrow;
    private KeyCode moveLeft = KeyCode.LeftArrow;
    private int speed = 10;
    public bool KillEnemyOn;
    public Text VirusesLeftText;
    public Text VirusesKilledText;
    private int VirusesKilled = 0;
    private Vector3 ScaleIncrement;
    public GameObject RedEndLifeSprite;
    private AudioSource source;
    public Animator animator;
    public GameObject PlayerSprite;
    public TrailRenderer trail;

    // Use this for initialization
    void Start()
    {
        float scale = 0.01f;
        ScaleIncrement = new Vector3(scale, scale, 0f);
        VirusesLeftText.text = "Viruses left: " + GameSetup.enemyQuantites.ToString();
        VirusesKilledText.text = "Viruses killed: " + VirusesKilled;
        source = GetComponent<AudioSource>();
        RedEndLifeSprite.transform.position = new Vector3(0, (float)(-14.5 + 14.3 * GameSetup.enemyQuantites / GameSetup.maxEnemyQuantity), 0);

        thisRigidBody = GetComponent<Rigidbody2D>();

    }


    bool isInMove = false;
    bool isInMoveHistory = false;
    // Update is called once per frame

    void Update()
    {
        if (!_freeze)
        {
            isInMove = false;

            VirusesLeftText.text = "Viruses left: " + GameSetup.enemyQuantites.ToString();
            RedEndLifeSprite.transform.position = new Vector3(0,
                (float) (-14.5 + 14.3 * GameSetup.enemyQuantites / GameSetup.maxEnemyQuantity), 0);

            if (Input.GetKey(moveUp))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
                isInMove = true;
            }
            else if (Input.GetKey(moveDown))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
                isInMove = true;
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

            }

            if (Input.GetKey(moveRight))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
                isInMove = true;
            }
            else if (Input.GetKey(moveLeft))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);
                isInMove = true;
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);

            }


            var v = thisRigidBody.velocity;
            var angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;

            if (angle < -90 || angle > 90)
            {
                angle -= 180;
                transform.localScale =
                    new Vector3(-Mathf.Abs(transform.localScale.x), -Mathf.Abs(transform.localScale.y), 1);
            }
            else
            {
                transform.localScale =
                    new Vector3(Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y), 1);
            }

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


            //Debug.Log("Angle:" + angle);
            if (isInMove != isInMoveHistory)
            {
                animator.SetBool("Move", isInMove);
                isInMoveHistory = isInMove;
            }


        }
    }

    private bool _freeze = false;
    public bool Freeze
    {
        get { return _freeze; }
        set
        {
            _freeze = value;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<Rigidbody2D>().angularVelocity = 0;
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

                source.Play();
                VirusesKilled++;
                VirusesKilledText.text = "Viruses killed: " + VirusesKilled;

                if (PlayerSprite.transform.localScale.x <= MaxScale && PlayerSprite.transform.localScale.y <= MaxScale)
                    //  transform.localScale += ScaleIncrement;
                    PlayerSprite.transform.localScale += ScaleIncrement;

            }
        }
    }
}
