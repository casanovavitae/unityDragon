using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl: MonoBehaviour{

	[SerializeField]
	float moveSpeed;
	Vector2 input;

	Rigidbody2D rigid;

	public bool reflection;
	public bool canReflect;
	public float reflectionTimer;
	public float reflectionDuration;
	public int reflectionToCooldownFactor; //how much longer the player can reflect as the cooldown
	public GameObject hitCollider;
	
	public Powerup currentPowerup;

	public AudioSource deathSoundSource;

	public uint startWithLives;
	private uint _livesLeft;
	public uint livesLeft
	{		
		get { 
			return _livesLeft;
		}
		set
		{
			_livesLeft = value;
			Global.uiManager.UpdateLives((int)value);
		}
	}

	void Awake() {
		Global.playerControl = this;
	}

	// Use this for initialization
	void Start () {		
		rigid = GetComponent<Rigidbody2D>();
		canReflect = true;

		livesLeft = startWithLives;
	}
	
	// Update is called once per frame
	void Update () {
		reflectionTimer+=Time.deltaTime;

		/*the active reflection time is twice the time between reflects (we can reflect if the reflectionTimer is halfway through the cooldown)
		*start reflecting*
		can't reflect again
		reflectionCooldown passes
		*stop reflecting*	
		half the reflectionCooldown passes
		can reflect again
		*/
        
		/*if(reflectionTimer >= reflectionDuration / reflectionToCooldownFactor) { //player can reflect
			if(canReflect && Input.GetKeyDown(KeyCode.Space))
            { //player starts reflecting
                reflection = true;
				hitCollider.SetActive(true);
				//GetComponent<SpriteRenderer>().color = Color.blue;
				canReflect = false;
				reflectionTimer = 0;
			} else if(reflectionTimer >= reflectionDuration) { //player is currently reflecting

				if(canReflect == false) {
					reflection = false;
					GetComponent<SpriteRenderer>().color = Color.white;
					canReflect = true;
					reflectionTimer = 0;
				}
			}
		}*/


		/*
		@João: change this so that only use is called
		if(currentPowerUp!=null && (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))) {

            currentPowerUp.Air();
            //;currentPowerup.Use()
		}

        if (currentPowerUp != null && (Input.GetKeyDown(KeyCode.Q)))
        {
            currentPowerUp.Air();
        }		*/    

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
				input += Vector2.left;
		}
		if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
			input += Vector2.right;
		}
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
			input += Vector2.up;
		}
		if(Input.GetKey(KeyCode.S)  || Input.GetKey(KeyCode.DownArrow)) {
			input += Vector2.down;
		}
	}

	void FixedUpdate() {
		rigid.MovePosition((Vector2)transform.position + (input.normalized * moveSpeed) * Time.deltaTime);
		
		input = Vector2.zero;
	}

	//public void TakeDamage(Bullet caller) {
	//	//caller.Hit();
	//	if(reflection == false) {
	//		if(livesLeft > 0) {
	//			livesLeft--;			}
			
	//		print("Lives left: " + livesLeft);

	//		if (livesLeft == 0)
	//		{
	//			//Die(); @João: just for the pitch
	//		}
	//		Destroy(caller.gameObject);
	//	}
	//}	

	void Die() {
		//GetComponent<SpriteRenderer>().color = Color.red;
		StartCoroutine(Global.audioManager.DestroyWithSound(gameObject,deathSoundSource));
		GetComponent<Collider2D>().enabled = false;
		GetComponent<Renderer>().enabled = false;
		enabled = false;
		//Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.GetComponent<Pickup>() != null) {

			//currentPowerup=(Powerup)other.GetComponent<Pickup>().PickThisUp();

			currentPowerup.gameObject.SetActive(false);
		}
	}
}