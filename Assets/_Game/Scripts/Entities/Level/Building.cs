using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

	public AudioSource aSource; //sound to play when the brick/building is destroyed
	public int pointsAwarded;

	public int lives;
    
	// Use this for initialization
	void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}
	

	//void Die() {
	//	GetComponent<Collider2D>().enabled = false;
	//	GetComponent<Renderer>().enabled = false;
	//  Global.uiManager.IncreaseScore(pointsAwarded);
	//	StartCoroutine(Global.audioManager.DestroyWithSound(gameObject,aSource));
	//}
}
