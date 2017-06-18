using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager: MonoBehaviour
{
    public Text scoreText;
	public Text livesText;

    public int score = 0;


	void Awake() {
		Global.uiManager = this;
	}
	    
	// Use this for initialization 
	void Start (){
		
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

	public void UpdateLives(int lives) {
		livesText.text = "Lives: " + lives.ToString();
	}

    public void IncreaseScore(int points)
    {
        score = score + points;
		scoreText.text = "Score : " + score.ToString();
    }
}
