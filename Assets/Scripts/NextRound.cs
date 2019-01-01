using UnityEngine;
using UnityEngine.UI;

public class NextRound : MonoBehaviour
{


    private float time = 3;

    public Text remainingTimeText;
    // Use this for initialization
    void Start()
    {
        
        ChangeElementStatus(false);


        InvokeRepeating("EnableLockedElements", time, time);
        InvokeRepeating("remainingTimeUpdate", 0, 1);

    }

    void remainingTimeUpdate()
    {
        remainingTimeText.text = "Start in " + ((int)time).ToString() +"s";
        time -= 1;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }
    }


    void ChangeElementStatus(bool var)
    {
        GameObject.FindGameObjectsWithTag("Player")[1].GetComponent<PlayerControls>().Freeze = !var;
        Enemy.EnemyCanReproduceHimself = var;
     
    }

    void EnableLockedElements()
    {
        ChangeElementStatus(true);
        Destroy(this.gameObject);
    }
}
