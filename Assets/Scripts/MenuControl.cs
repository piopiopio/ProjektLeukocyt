using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MenuControl : MonoBehaviour
{
    public GameObject Player;
    public GameObject canvas1;

    public GameObject enemy1 ;
    public GameObject enemy2 ;
    public GameObject enemy3 ;

    private KeyCode moveUp = KeyCode.UpArrow;
    private KeyCode moveDown = KeyCode.DownArrow;
    private KeyCode moveRight = KeyCode.RightArrow;
    private KeyCode moveLeft = KeyCode.LeftArrow;


    // Use this for initialization
    void Start ()
    {
        ChangeElementStatus(false);
        SetUpEnemies();

    }
	
	// Update is called once per frame
	void Update () {

	    if (Input.GetKey(moveUp) || Input.GetKey(moveDown) || Input.GetKey(moveRight) || Input.GetKey(moveLeft))
	    {
	        ChangeElementStatus(true);
           // Destroy(this.gameObject);
           this.gameObject.SetActive(false);
	        Player.GetComponent<PlayerControls>().Freeze = false;
	    }
    }

    public void ChangeElementStatus(bool var)
    {
        Enemy.EnemyCanReproduceHimself = var;
        Player.GetComponent<PlayerControls>().KillEnemyOn = var;
        canvas1.SetActive(var);
    }

    public void SetUpEnemies()
    {

        Instantiate(enemy1, new Vector3(4.7f, 2.6f, 0f), Quaternion.identity);
        Instantiate(enemy1, new Vector3(-2.1f, 2.5f, 0f), Quaternion.identity);
        Instantiate(enemy1, new Vector3(-5.1f, 2.4f, 0f), Quaternion.identity);

        Instantiate(enemy2, new Vector3(-0.4f, -2f, 0f), Quaternion.identity);
        Instantiate(enemy2, new Vector3(-1.6f, -2.4f, 0f), Quaternion.identity);
        Instantiate(enemy2, new Vector3(0.1f, -4f, 0f), Quaternion.identity);
        Instantiate(enemy2, new Vector3(-2.4f, -4.8f, 0f), Quaternion.identity);

        Instantiate(enemy3, new Vector3(2.6f, -4.5f, 0f), Quaternion.identity);
        Instantiate(enemy3, new Vector3(-6f, -3f, 0f), Quaternion.identity);
        Instantiate(enemy3, new Vector3(8.3f, -1.7f, 0f), Quaternion.identity);

        //  GameSetup.enemyQuantites= GameObject.FindGameObjectsWithTag("Enemy").Count();

        GameSetup.enemyQuantites = 10;
        Player.transform.localScale=new Vector3(0.3f,0.3f,0.3f);
        Player.GetComponent<PlayerControls>().Initialise();

    }
}
