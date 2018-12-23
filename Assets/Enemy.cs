using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityScript.Steps;
using Debug = UnityEngine.Debug;

public class Enemy : MonoBehaviour
{


    public int RunAwaySpeed;
    public int RandomSpeed;
    public bool EnemyCanReproduceHimself;
    private Vector2 randomDirection;

    private Vector3 randomSpeedVector;
    private Vector3 runAwaySpeedVector;
    public Camera mainCam;// = new Camera();



    private Vector2 randomSpeedVector2d
    {
        get {return new Vector2(randomSpeedVector.x, randomSpeedVector.y);}
        set { randomSpeedVector=new Vector3(value.x, value.y,0);}
    }
    // Use this for initialization
    void Start()
    {
        randomSpeedVector = new Vector2(Random.Range(100, -100), Random.Range(100, -100)).normalized * RandomSpeed;
        GetComponent<Rigidbody2D>().AddForce(randomSpeedVector);
        InvokeRepeating("changeDirection", 5f, 5f);

        float multiplicationPeriod = Random.Range(15, 25)/10f;
        InvokeRepeating("multiplicate", multiplicationPeriod, multiplicationPeriod);
    }

    // Update is called once per frame
    void Update()
    {

        //   GetComponent<Rigidbody2D>().velocity = (transform.position - Player.transform.position).normalized * RunAwaySpeed + new Vector3(randomDirection.x , randomDirection.y , 0)* RandomSpeed;

        // runAwaySpeedVector = (transform.position - Player.transform.position).normalized * RunAwaySpeed;
        //  GetComponent<Rigidbody2D>().velocity = randomSpeedVector;// + runAwaySpeedVector;
       // WatchDog();


    }

    //void WatchDog()
    //{
    //   var temp= mainCam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
    //    if (transform.position.x > temp.x || transform.position.x < -temp.x || transform.position.y > temp.y || transform.position.y < -temp.y) 
    //    {
    //        Destroy(gameObject);
    //        GameSetup.enemyQuantites--;
    //        Debug.Log("Watch dog delete object");
    //    }
      
    //}

    void changeDirection()
    {
        randomDirection = new Vector2(Random.Range(100, -100), Random.Range(100, -100)).normalized;
        randomSpeedVector = new Vector3(randomDirection.x * RandomSpeed, randomDirection.y * RandomSpeed, 0);

        GetComponent<Rigidbody2D>().AddForce(randomSpeedVector);
        Debug.Log(Time.time + "change direction x:" + randomDirection.x + " y: " + randomDirection.y);


    }

    void multiplicate()
    {
        if (EnemyCanReproduceHimself && GameSetup.enemyQuantites < GameSetup.maxEnemyQuantity)
        {
            Instantiate(this);
            GameSetup.enemyQuantites++;
        }
    }

    //void OnCollisionEnter2D(Collision2D col)
    //{
      //  GetComponent<Rigidbody2D>().velocity=new Vector2(0,0);
      //  if (col.gameObject.tag == "HorizontalWall")
      //  {
      //      //randomDirection=new Vector2(randomDirection.x, -randomDirection.y);
      //      // GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -GetComponent<Rigidbody2D>().velocity.y);
      //      randomSpeedVector = new Vector3(randomSpeedVector.x, -randomSpeedVector.y, 0);
      //      //    runAwaySpeedVector = new Vector3(runAwaySpeedVector.x, -runAwaySpeedVector.y, 0);
      //      Debug.Log("Enemy-HorizontalWall collision");
      //  }

      //  if (col.gameObject.tag == "VerticalWall")
      //  {
      //      //randomDirection=new Vector2(-randomDirection.x, randomDirection.y);
      //      // GetComponent<Rigidbody2D>().velocity = new Vector2(-GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y);
      //      randomSpeedVector = new Vector3(-randomSpeedVector.x, randomSpeedVector.y, 0);
      //      //  runAwaySpeedVector = new Vector3(-runAwaySpeedVector.x, runAwaySpeedVector.y, 0);
      //      Debug.Log("Enemy-HorizontalWall collision");
      //  }


      //  if (col.gameObject.tag == "Wall")
      //  {
      //      Vector2 temp = (new Vector2(transform.position.x, transform.position.y)) - col.GetContact(0).point;
      //    Vector2 n= col.GetContact(0).normal.normalized;

      //      randomSpeedVector2d = randomSpeedVector2d - 2 * (Vector2.Dot(randomSpeedVector2d, n) * n);
      //      //<Rigidbody2D>().transform.position = GetComponent<Rigidbody2D>().transform.position + (new Vector3(n.x, n.y,0))*0.1f;
      //      //transform.position = transform.position +new Vector3(n.x, n.y, 0)* 2f;
      //      Debug.Log("Enemy-Wall collision, normal:"+n + "compare to manual calculated normal"+temp);
      ////  Destroy(gameObject);
      //  }
  //  }
}
