using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject Player;

    private int speed=5;

    private Vector3 tempSpeed;
	// Use this for initialization
	void Start ()
	{
	
    }
	
	// Update is called once per frame
	void Update ()
	{

	    GetComponent<Rigidbody2D>().velocity = (transform.position - Player.transform.position).normalized * speed;// + new Vector3(Random.Range(-speed, speed), Random.Range(-speed, speed),0);


	}
}
