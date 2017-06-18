using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider:MonoBehaviour {

	public Vector3 initialPos;
	public float positionOffset;

	float timer;
	public float cooldown;
	public float hitDistance;
	public float speed;

	void Awake() {
		initialPos = transform.localPosition;
	}

	// Use this for initialization
	void Start() {		
		cooldown = Global.playerControl.reflectionDuration;
		speed = hitDistance / (cooldown/3);
	}

	// Update is called once per frame
	void Update() {		

		if(Global.playerControl.reflection) {
			transform.localPosition = Vector2.Lerp(transform.localPosition,Vector2.right * hitDistance,Time.deltaTime * speed*5);
		}

		if(transform.localPosition.x == 0 || Global.playerControl.reflection == false) {
			if(Global.playerControl.reflection == false) { //the reflection is handled by the player's timer
				gameObject.SetActive(false);
			}
			
		}
	}	
	void OnEnable() {
		timer = 0;
		//transform.localPosition = initialPos;
	}

	void OnDisable() {
		transform.localPosition = initialPos;
	}
	
	void Hit() { //@João: What the hell is this for??? Delete?? The implementation above works great so far. This is way too farfetched
		timer += Time.deltaTime;
		if(Global.playerControl.reflectionTimer >= Global.playerControl.reflectionDuration / 2) {
			if(Global.playerControl.canReflect) {
				Global.playerControl.reflection = true;
				transform.Translate(Vector2.right * Time.deltaTime);
				Global.playerControl.canReflect = false;
				Global.playerControl.reflectionTimer = 0;
			} else if(Global.playerControl.reflectionTimer >= Global.playerControl.reflectionDuration) {
				transform.Translate(Vector2.left * Time.deltaTime);
				if(Global.playerControl.canReflect == false) {
					Global.playerControl.reflection = false;

					Global.playerControl.canReflect = true;
					Global.playerControl.reflectionTimer = 0;
				}
			}
		}
	}
}