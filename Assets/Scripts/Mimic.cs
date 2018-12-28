using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mimic : MonoBehaviour {
    public Sprite SuprisedSprite;
    public Sprite ConfusedSprite;
    public Sprite HappySprite;
    private SpriteRenderer MimicOutputSprite;
    // Use this for initialization
    void Start () {
        MimicOutputSprite = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        if (GameSetup.enemyQuantites < 20)
        {
            MimicOutputSprite.sprite = HappySprite;
        }
        else if ((GameSetup.enemyQuantites >= 20) && (GameSetup.enemyQuantites <= 30))
        {
            MimicOutputSprite.sprite = SuprisedSprite;
        }
        else
        {
            MimicOutputSprite.sprite = ConfusedSprite;
        }
    }
}
