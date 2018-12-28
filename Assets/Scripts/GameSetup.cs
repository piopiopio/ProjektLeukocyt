using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSetup : MonoBehaviour
{
    public Camera mainCam;// = new Camera();
    public Animator animator;

    public Text BestLevelText;
    public Text LevelText;

    public GameObject menuGameObject;
    //public BoxCollider2D topWall;// = new BoxCollider2D();
    //public BoxCollider2D bottomWall;// = new BoxCollider2D();
    //public BoxCollider2D rightWall;// = new BoxCollider2D();
    //public BoxCollider2D leftWall;// = new BoxCollider2D();

    public Transform Player01;
    public static int enemyQuantites = 10;
    public static int maxEnemyQuantity = 40;

    public static int bestLevel = 0;
    public static int Level = 1;

    public static int bestScore = 0;

    public GameObject Loss;
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
    public static bool LossFlag = false;
    void Update()
    {


        animator.SetInteger("Viruses", enemyQuantites);
        if (enemyQuantites >= maxEnemyQuantity && !LossFlag)
        {
            menuGameObject.gameObject.GetComponent<MenuControl>().ChangeElementStatus(false);

            LossFlag = true;     
            Loss.gameObject.SetActive(true);
            
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<Enemy>().Freeze = true;
            }

            
            Player01.GetComponent<PlayerControls>().Freeze = true;
            BestLevelText.text = "Best level: " + bestLevel.ToString();
            LevelText.text = "Level: " + Level.ToString();
        }
    }
}
