using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager:MonoBehaviour {


	public Material spriteMat;
	public float matIntensity;
	//public UnityEngine.UI.Text intensityText;

	public int currentPhase;
		
	public GameObject floorPrefab;
	int floorCount;

	public GameObject pitchLivesPanel;
	public GameObject pitchEnemy1;
	public GameObject pitchScorePanel;


	void Awake() {
		Global.gameManager = this;
		//DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization 
	void Start() {
		spriteMat.SetFloat("_Intensity",matIntensity);
		floorCount = 0;
	}

	// Update is called once per frame
	void Update() {
		///////////////////////////
		//@João: just for the pitch @@maybe not...????
		if(Camera.main.transform.position.x > floorCount * 51) {
			SpawnFloor();			
		}

		
		if(Input.GetKeyDown(KeyCode.F2)) {
			SceneManager.LoadScene(Application.loadedLevel); //to hell with SceneManager------Application rocks!
		}
		if(Input.GetKey(KeyCode.KeypadPlus)) {
			matIntensity = spriteMat.GetFloat("_Intensity") + Time.deltaTime;
			spriteMat.SetFloat("_Intensity",matIntensity);
		}
		if(Input.GetKey(KeyCode.KeypadMinus)) {
			matIntensity = spriteMat.GetFloat("_Intensity") - Time.deltaTime;
			spriteMat.SetFloat("_Intensity",matIntensity);
		}
		//intensityText.text = matIntensity.ToString();


		if(Input.GetKeyDown(KeyCode.Return)) {
			GoToNextPhase();
		}
		if(Input.GetKeyDown(KeyCode.Keypad4)) {
			Camera.main.GetComponent<CameraScript>().autoMoveSpeed--;
		}
		if(Input.GetKeyDown(KeyCode.Keypad6)) {
			Camera.main.GetComponent<CameraScript>().autoMoveSpeed++;
		}
		if(Input.GetKeyDown(KeyCode.Keypad5)) {
			Camera.main.GetComponent<CameraScript>().autoMoveSpeed=0;
		}


		//////////////////////
	}

	void GoToNextPhase() {
		currentPhase++;
		switch(currentPhase) {
			case 1:
				Global.playerControl.livesLeft = 9001;
				pitchLivesPanel.SetActive(true);
				break;
			case 2:
				Global.uiManager.score = 0;
				Global.uiManager.IncreaseScore(0);
				pitchScorePanel.SetActive(true);
				break;
			case 3:
				pitchEnemy1.GetComponent<Airship>().health = 1;
				break;
		}
	}

	void SpawnFloor() {
		floorCount++;
		Instantiate(floorPrefab,new Vector2(51*floorCount,2.3f),Quaternion.identity);		
	}
}
