using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class GameSetup : MonoBehaviour
{
    private List<List<Vector4>> LevelsEnemysList = new List<List<Vector4>>();
    private List<GameObject> EnemysList = new List<GameObject>();
    private List<GameObject> SceneList = new List<GameObject>();


    public Camera mainCam;// = new Camera();
    public Animator animator;
    public bool killGame = false;
    public Text BestLevelText;
    public Text BestLevelTextHeader;
    public Text LevelText;
    public Text LevelText1;

    public GameObject menuGameObject;
    //public BoxCollider2D topWall;// = new BoxCollider2D();
    //public BoxCollider2D bottomWall;// = new BoxCollider2D();
    //public BoxCollider2D rightWall;// = new BoxCollider2D();
    //public BoxCollider2D leftWall;// = new BoxCollider2D();

    public Transform Player01;
    // public static int enemyQuantites = 10;
    public static int maxEnemyQuantity = 40;

    private static int bestLevel = 1;
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

    public Sprite MutedSprite;
    public Sprite UnmutedSprite;
    public GameObject MuteButton;

    public Sprite PausePause;
    public Sprite PausePlay;
    public GameObject PauseButton;
    public GameObject PausedText;
    public GameObject Pills;
    
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
        moveBackground();
    }

    public void moveBackground()
    {
        RedBackground.transform.position = new Vector3(0, Mathf.Min((float)(-14.3 + 14.3 * Enemy.EnemyQuantity / GameSetup.maxEnemyQuantity),0), 0);
    }
    // Update is called once per frame
    public static bool LossFlag = false;

    public void changeLevel()
    {
        Enemy.multiplicationPeriodConstMiliSecond = (int)(Enemy.multiplicationPeriodConstMiliSecond * 0.9);
       
        if (!(LoopedLevel < LevelCount))
        {
            LoopedLevel -= LevelCount;

            
        }


        Destroy(Scene);


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
        LossFlag = false;
    }


    public void MuteClick()
    {
        GetComponent<AudioSource>().mute = !GetComponent<AudioSource>().mute;
        if (GetComponent<AudioSource>().mute == true)
        {
            MuteButton.GetComponent<Image>().sprite = MutedSprite;
            Player01.GetComponent<PlayerControls>().muted = true;
        }
        else
        {
            MuteButton.GetComponent<Image>().sprite = UnmutedSprite;
            Player01.GetComponent<PlayerControls>().muted = false;
        }

    }

    private bool paused = false;
    public void Pause()
    {
        paused = !paused;
        //PauseButton.GetComponent<Animator>().SetBool("Pressed", true);
        //PauseButton.GetComponent<Animator>().Play("PausePress");

        if (paused)
        {
            PauseButton.GetComponent<Button>().image.sprite = PausePlay;

            Player01.GetComponent<PlayerControls>().Freeze = true;
            Time.timeScale = 0;
            MuteButton.SetActive(true);
            PausedText.SetActive(true);
        }
        else
        {
            PauseButton.GetComponent<Button>().image.sprite = PausePause;

            Player01.GetComponent<PlayerControls>().Freeze = false;
            Time.timeScale = 1;
            MuteButton.SetActive(false);
            PausedText.SetActive(false);
        }
        // PauseButton.GetComponent<Animator>().SetTrigger("Pressed");
        //  PauseButton.GetComponent<Animator>().Play("PausePress");
        PauseButton.GetComponent<Animator>().SetBool("Pressed", true);
    }
    public static bool NoPillFlag = true;
    void Update()
    {



        if (killGame == true)
        {
            killGame = false;
            for (int i = 0; i < 40; i++)
            {
                Instantiate(enemy1);
            }
        }



        if (Enemy.EnemyQuantity <= 0)
        {
            changeLevel();
        }

        if (Loss.gameObject.activeSelf == false)
        {
            //RedBackground.transform.position = new Vector3(0,
            //    (float) (-14.5 + 14.3 * Enemy.EnemyQuantity / GameSetup.maxEnemyQuantity), 0);
            moveBackground();// RedBackground.transform.position = new Vector3(0, (float)(-14.3 + 14.3 * Enemy.EnemyQuantity / GameSetup.maxEnemyQuantity), 0);
        }

        if (Player01.GetComponent<PlayerControls>().VirusesKilled % 20 == 0 && NoPillFlag && Player01.GetComponent<PlayerControls>().VirusesKilled != 0)
        {
            NoPillFlag = false;

            //var p= Instantiate(Pills, GameObject.FindGameObjectWithTag("Enemy").transform.position, Quaternion.identity);

            var p = Instantiate(Pills, new Vector3(UnityEngine.Random.Range(-2.1f, 2.1f), UnityEngine.Random.Range(-2f, 0.2f), 0), Quaternion.identity);

            p.GetComponent<SpriteRenderer>().color = EnemysList[UnityEngine.Random.Range(0, EnemysList.Count)].GetComponentsInChildren<SpriteRenderer>()[1].color;
           

        }



        if (Enemy.EnemyQuantity >= maxEnemyQuantity)
        {

            if (!LossFlag)
            {

                moveBackground();//RedBackground.transform.position = new Vector3(0, (float)(-14.3 + 14.3 * Enemy.EnemyQuantity / GameSetup.maxEnemyQuantity), 0);
                LossFlag = true;
                

                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject enemy in enemies)
                {
                    enemy.GetComponent<Enemy>().Freeze = true;
                }
                Player01.GetComponent<PlayerControls>().Freeze = true;



                //Debug.Log("Loss.gameObject.SetActive(true); Loseflag" + LossFlag);

                animator.SetInteger("Viruses", 0);
                //Invoke("LoadLossMenu",5f);
                menuGameObject.gameObject.GetComponent<MenuControl>().ChangeElementStatus(false);
                BestLevelText.text = "Best level: " + bestLevel.ToString();

                LevelText.text = "Level: " + Level.ToString();

                PauseButton.SetActive(false);
                MuteButton.SetActive(true);

                if (Level > bestLevel)
                {
                    BestLevelTextHeader.gameObject.SetActive(true);
                }
                else
                {

                    BestLevelTextHeader.gameObject.SetActive(false);
                }
                Loss.gameObject.SetActive(true);
                bestLevel = Math.Max(Level, bestLevel);

            }
        }
        else
        {
            if (Loss.gameObject.activeSelf == false)
            {
                animator.SetInteger("Viruses", Enemy.EnemyQuantity);
            }

        }
      //  Enemy.EnemyQuantity = GameObject.FindGameObjectsWithTag("Enemy").Length;
      //  Debug.Log("Enemy quantity: "+Enemy.EnemyQuantity);
    }

    //public void LoadLossMenu()
    //{
    //    menuGameObject.gameObject.GetComponent<MenuControl>().ChangeElementStatus(false);
    //    BestLevelText.text = "Best level: " + bestLevel.ToString();
    //    bestLevel = Math.Max(Level, bestLevel);
    //    LevelText.text = "Level: " + Level.ToString();
      
    //    PauseButton.SetActive(false);
    //    MuteButton.SetActive(true);
    //    Loss.gameObject.SetActive(true);
    //}    //public void LoadLossMenu()
    //{
    //    menuGameObject.gameObject.GetComponent<MenuControl>().ChangeElementStatus(false);
    //    BestLevelText.text = "Best level: " + bestLevel.ToString();
    //    bestLevel = Math.Max(Level, bestLevel);
    //    LevelText.text = "Level: " + Level.ToString();
      
    //    PauseButton.SetActive(false);
    //    MuteButton.SetActive(true);
    //    Loss.gameObject.SetActive(true);
    //}
}
