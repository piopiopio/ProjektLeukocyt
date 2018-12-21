using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject Player;

    public int RunAwaySpeed;
    public int RandomSpeed;
    public Vector2 randomDirection;

	// Use this for initialization
	void Start ()
	{
	    randomDirection=new Vector2(Random.Range(1, -1), Random.Range(1, -1)).normalized;
        InvokeRepeating("changeDirection", 3f, 3f);
    }
	
	// Update is called once per frame
	void Update ()
	{

	//   GetComponent<Rigidbody2D>().velocity = (transform.position - Player.transform.position).normalized * RunAwaySpeed + new Vector3(randomDirection.x , randomDirection.y , 0)* RandomSpeed;
	//   GetComponent<Rigidbody2D>().velocity =  new Vector3(randomDirection.x , randomDirection.y , 0)* RandomSpeed;
        


    }

    void changeDirection()
    {
        randomDirection= new Vector2(Random.Range(100, -100), Random.Range(100, -100)).normalized;
        Debug.Log("change direction x:"+ randomDirection.x+" y: " + randomDirection.y);
        GetComponent<Rigidbody2D>().velocity = randomDirection * RandomSpeed;
        Debug.Log(Time.time);
    }

    float elapsed = 100f;
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "HorizontalWall" )
        {
            //randomDirection=new Vector2(randomDirection.x, -randomDirection.y);
            GetComponent<Rigidbody2D>().velocity=new Vector2(GetComponent<Rigidbody2D>().velocity.x, -GetComponent<Rigidbody2D>().velocity.y);
            randomDirection = GetComponent<Rigidbody2D>().velocity;
            Debug.Log("Enemy-HorizontalWall collision");
        }

        if (col.gameObject.tag == "VerticalWall")
        {
            //randomDirection=new Vector2(-randomDirection.x, randomDirection.y);
            GetComponent<Rigidbody2D>().velocity = new Vector2(-GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y);
            randomDirection = GetComponent<Rigidbody2D>().velocity;
            Debug.Log("Enemy-HorizontalWall collision" );
        }
    }
}
