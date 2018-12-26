using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonAction : MonoBehaviour {

	// Use this for initialization
    public void StartGame()
    {
        SceneManager.LoadScene("FirstScene");
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
