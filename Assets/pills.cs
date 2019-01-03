using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pills : MonoBehaviour
{
    public GameObject particls;
	// Use this for initialization
	void Start () {
        Instantiate(particls, transform.position, Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Instantiate(particls, transform.position, Quaternion.identity);
        }
    }
}
