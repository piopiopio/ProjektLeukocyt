using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject Player;

    private int speed=5;
    public Vector3 randomDirection;
    private Vector3 tempSpeed;
	// Use this for initialization
	void Start ()
	{
	    InvokeRepeating("changeDirection", 6f, 6f);
    }
	
	// Update is called once per frame
	void Update ()
	{

	    GetComponent<Rigidbody2D>().velocity = (transform.position - Player.transform.position).normalized * speed*50/((transform.position - Player.transform.position ).magnitude)+ randomDirection*speed*20;// + new Vector3(Random.Range(-speed, speed), Random.Range(-speed, speed),0);



    }

    void changeDirection()
    {
        randomDirection= new Vector3(Random.Range(1, -1), Random.Range(1, -1),0);
        Debug.Log("-");
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Wall")
        {
            randomDirection=new Vector3(-randomDirection.x, -randomDirection.x,0);
            Debug.Log("change direction");
        }
    }
}
