using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LossControl : MonoBehaviour
{






    public GameObject MenuControls;
    public GameObject _gm;

    
      // Use this for initialization
    void Start()
    {
        //     ChangeElementStatus(false);


    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Destroy(this.gameObject);

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                //GameObject.Destroy(enemy);
                enemy.GetComponent<Enemy>().Destroy();

               // enemy.GetComponent<Enemy>().DestroyExecution();
            }



            MenuControls.gameObject.GetComponent<MenuControl>().SetUpEnemies();
            MenuControls.gameObject.SetActive(true);
            MenuControls.gameObject.GetComponent<MenuControl>().ChangeElementStatus(false);


           
            


            _gm.GetComponent<GameSetup>().resetGame();


            gameObject.SetActive(false);

            Enemy.multiplicationPeriodConstMiliSecond = 2000;
            // GameSetup.LossFlag = false;
        }
    }

    //    void ChangeElementStatus(bool var)
    //    {
    //        Player.GetComponent<PlayerControls>().KillEnemyOn = var;
    //        Enemy.EnemyCanReproduceHimself = var;
    //        canvas1.SetActive(var);
    //    }
}
