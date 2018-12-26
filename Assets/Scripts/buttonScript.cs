using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonScript : MonoBehaviour
{



    private KeyCode moveUp = KeyCode.UpArrow;
    private KeyCode moveDown = KeyCode.DownArrow;
    private KeyCode moveRight = KeyCode.RightArrow;
    private KeyCode moveLeft = KeyCode.LeftArrow;
    private int speed = 10;
    //public BoxCollider2D topWall;// = new BoxCollider2D();
    //public BoxCollider2D bottomWall;// = new BoxCollider2D();
    //public BoxCollider2D rightWall;// = new BoxCollider2D();
    //public BoxCollider2D leftWall;// = new BoxCollider2D();

    public static int enemyQuantites = 10;
    public static int maxEnemyQuantity = 40;

    // Use this for initialization
    void Start()
    {
        //topWall.size = new Vector2(mainCam.ScreenToWorldPoint(new Vector3(Screen.width * 2f, 0f, 0f)).x, 1f);
        //topWall.offset = new Vector2(0f, mainCam.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f)).y + 0.5f);

        //bottomWall.size = new Vector2(mainCam.ScreenToWorldPoint(new Vector3(Screen.width * 2f, 0f, 0f)).x, 1f);
        //bottomWall.offset = new Vector2(0f, mainCam.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).y - 0.5f);

        //rightWall.size = new Vector2(1f, mainCam.ScreenToWorldPoint(new Vector3(0f, Screen.height * 2f, 0f)).y);
        //rightWall.offset = new Vector2(mainCam.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x + 0.5f, 0f);

        //leftWall.size = new Vector2(1f, mainCam.ScreenToWorldPoint(new Vector3(0f, Screen.height * 2f, 0f)).y);
        //leftWall.offset = new Vector2(-mainCam.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x - 0.5f, 0f);


    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(moveUp) || Input.GetKey(moveDown) || Input.GetKey(moveRight) || Input.GetKey(moveLeft))
        {
            StartGame();
        }

    }

    public void StartGame()
    {
        SceneManager.LoadScene("1");
    }
}


