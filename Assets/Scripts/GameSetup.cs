using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSetup : MonoBehaviour
{
    private List<List<Vector4>> LevelsEnemysList = new List<List<Vector4>>();
    private List<GameObject> EnemysList = new List<GameObject>();
    private List<GameObject> SceneList = new List<GameObject>();


    public Camera mainCam;// = new Camera();
    public Animator animator;

    public Text BestLevelText;
    public Text LevelText;
    public Text LevelText1;

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
    private static int LoopedLevel = 1;
    private static int LevelCount;

    public GameObject Loss;
    public GameObject NextRoundBanner;
    public GameObject RedBackground;


    public GameObject Scene;

    public GameObject Scene0;
    public GameObject Scene1;
    public GameObject Scene2;
    public GameObject Scene3;
    public GameObject Scene4;
    public GameObject Scene5;
    public GameObject Scene6;
    public GameObject Scene7;


    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    // Use this for initialization
    void Start()
    {
        LevelText.text = "Level: " + Level.ToString();
        EnemysList.Add(enemy1);
        EnemysList.Add(enemy2);
        EnemysList.Add(enemy3);
        //Level 0
        var temp = new List<Vector4>();
        temp.Add(new Vector4(0f, 1.7f, 2.6f, 0f));
        temp.Add(new Vector4(0f, -2.1f, 2.5f, 0f));
        temp.Add(new Vector4(0f, -1.1f, 2.4f, 0f));
        temp.Add(new Vector4(1f, -0.4f, -2f, 0f));
        temp.Add(new Vector4(1f, -1.6f, -2.4f, 0f));
        temp.Add(new Vector4(1f, 0.1f, -2f, 0f));
        temp.Add(new Vector4(2f, -2.4f, -1.8f, 0f));
        temp.Add(new Vector4(2f, 2.6f, -2.5f, 0f));
        temp.Add(new Vector4(2f, -1f, -3f, 0f));
        temp.Add(new Vector4(2f, 1.3f, -1.7f, 0f));

        LevelsEnemysList.Add(temp);
        LevelsEnemysList.Add(temp);
        LevelsEnemysList.Add(temp);
        LevelsEnemysList.Add(temp);
        LevelsEnemysList.Add(temp);
        LevelsEnemysList.Add(temp);
        LevelsEnemysList.Add(temp);
        LevelsEnemysList.Add(temp);

        SceneList.Add(Scene0);
        SceneList.Add(Scene1);
        SceneList.Add(Scene2);
        SceneList.Add(Scene3);
        SceneList.Add(Scene4);
        SceneList.Add(Scene5);
        SceneList.Add(Scene6);
        SceneList.Add(Scene7);


        LevelCount = Mathf.Min(LevelsEnemysList.Count, SceneList.Count);
    }

    // Update is called once per frame
    public static bool LossFlag = false;

    public void changeLevel()
    {

        if (!(LoopedLevel < LevelCount))
        {
            LoopedLevel -= LevelCount;
            Enemy.multiplicationPeriodConstMiliSecond = Enemy.multiplicationPeriodConstMiliSecond / 2;
            //Player01.GetComponent<PlayerControls>().Speed = Player01.GetComponent<PlayerControls>().Speed / 2;
        }


        Destroy(Scene);
        enemyQuantites = 10;

        foreach (var item in LevelsEnemysList[LoopedLevel])
        {
            Instantiate(EnemysList[(int)item[0]], new Vector3(item[1], item[2], item[3]), Quaternion.identity);
        }


        Scene = Instantiate(SceneList[LoopedLevel]);


        Player01.transform.position = new Vector3(0f, 0f, 0f);
        Level++;
        LoopedLevel++;
        LevelText1.text = "Level: " + Level.ToString();

        Instantiate(NextRoundBanner);


    }

    public void resetGame()
    {
        Level = 1;
        LoopedLevel = 1;
        Destroy(Scene);
        Scene = Instantiate(SceneList[0]);
        LevelText1.text = "Level: " + Level.ToString();
        
        
    }
    
    void Update()
    {

        if (enemyQuantites == 0)
        {
            changeLevel();
        }

        RedBackground.transform.position = new Vector3(0, (float)(-14.5 + 14.3 * GameSetup.enemyQuantites / GameSetup.maxEnemyQuantity), 0);


        if (enemyQuantites >= maxEnemyQuantity)
        {
            animator.SetInteger("Viruses", 0);
            if (!LossFlag)
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
                bestLevel = Math.Max(Level, bestLevel);
                LevelText.text = "Level: " + Level.ToString();


            }
        }
        else
        {
            animator.SetInteger("Viruses", enemyQuantites);
        }

    ;
    }
}
