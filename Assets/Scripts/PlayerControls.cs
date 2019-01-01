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
    public int Speed = 10;
    public bool KillEnemyOn;

    public bool _cheat = false;


    public Text VirusesLeftText;
    public Text VirusesKilledText;
    public int VirusesKilled = 0;
    //private Vector3 ScaleIncrement;
    //public GameObject RedEndLifeSprite;
    private AudioSource source;
    public Animator animator;
    public GameObject PlayerSprite;
    public TrailRenderer trail;

    public bool muted = false;
    // Use this for initialization
    void Start()
    {
        //   float scale = 0.01f;
        //  ScaleIncrement = new Vector3(scale, scale, 0f);

        // VirusesLeftText.text = "Viruses left: " + GameSetup.enemyQuantites.ToString();
        VirusesLeftText.text = "Viruses left: " + Enemy.EnemyQuantity.ToString();
        VirusesKilledText.text = "Viruses killed: " + VirusesKilled;
        source = GetComponent<AudioSource>();
      //  RedEndLifeSprite.transform.position = new Vector3(0, (float)(-14.5 + 14.3 * GameSetup.enemyQuantites / GameSetup.maxEnemyQuantity), 0);
        //RedEndLifeSprite.transform.position = new Vector3(0, (float)(-14.3 + 14.1 * Enemy.EnemyQuantity / GameSetup.maxEnemyQuantity), 0);

        thisRigidBody = GetComponent<Rigidbody2D>();
        // trail.startWidth = 3;
    }


    bool isInMove = false;
    bool isInMoveHistory = false;
    // Update is called once per frame

    void Update()
    {
        if (_cheat == true)
        {
            PlayerSprite.transform.localScale = new Vector3(4f, 4f, 4f);
        }
        else
        {
            PlayerSprite.transform.localScale = new Vector3(1f, 1f, 1f);
        }





        if (!_freeze)
        {
            isInMove = false;

           // VirusesLeftText.text = "Viruses left: " + GameSetup.enemyQuantites.ToString();
            VirusesLeftText.text = "Viruses left: " + Enemy.EnemyQuantity.ToString();


            if (Input.GetKey(moveUp))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, Speed);
                isInMove = true;
            }
            else if (Input.GetKey(moveDown))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, -Speed);
                isInMove = true;
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

            }

            if (Input.GetKey(moveRight))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(Speed, GetComponent<Rigidbody2D>().velocity.y);
                isInMove = true;
            }
            else if (Input.GetKey(moveLeft))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-Speed, GetComponent<Rigidbody2D>().velocity.y);
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
            KillEnemyOn = !_freeze;
        }
    }


    void OnCollisionEnter2D(Collision2D col)
    {


        if (col.gameObject.tag == "Pills")
        {
            GameSetup.NoPillFlag = true;
            GameObject[] tempCollection = GameObject.FindGameObjectsWithTag("Enemy");

            KillEnemyOn = false;
            Enemy.EnemyCanReproduceHimself = false;
            var sameColorsEnemies = tempCollection.Where(enemy=>enemy.GetComponentsInChildren<SpriteRenderer>()[1].color ==
                                                                col.gameObject.GetComponent<SpriteRenderer>().color).ToList();
            //for (int i = 0; i < tempCollection.GetLength(0); i++)
            //{
            //    if (tempCollection[i].GetComponentsInChildren<SpriteRenderer>()[1].color ==
            //        col.gameObject.GetComponent<SpriteRenderer>().color)
            //    {
            //        tempCollection[i].gameObject.GetComponent<Enemy>().Destroy();
            //        //   GameSetup.enemyQuantites--;
            //    }
            //}
            sameColorsEnemies.ForEach(enemy=>enemy.GetComponent<Enemy>().Destroy());
            KillEnemyOn = true;
            Enemy.EnemyCanReproduceHimself = true;
            //VirusesKilled += temp;
            Destroy(col.gameObject);

            return;
        }
        //col.contacts.First().normal;
        if (col.gameObject.tag == "Enemy")
        {
            if (KillEnemyOn)
            {

                col.gameObject.GetComponent<Enemy>().Destroy();
              // GameSetup.enemyQuantites--;
                if (!muted)
                {
                    source.Play();
                }


                VirusesKilled++;
                VirusesKilledText.text = "Viruses killed: " + VirusesKilled;

                //if (PlayerSprite.transform.localScale.x <= MaxScale && PlayerSprite.transform.localScale.y <= MaxScale)
                //    //  transform.localScale += ScaleIncrement;
                //   // PlayerSprite.transform.localScale += ScaleIncrement;
                //    PlayerSprite.transform.localScale = new Vector3(4f,4f,4f);

            }
        }

    }

        
    

    public void Initialise()
    {
        PlayerSprite.transform.localScale = new Vector3(1, 1, 0);
        VirusesKilled = 0;
        VirusesKilledText.text = "Viruses killed: " + VirusesKilled;
    }
}
