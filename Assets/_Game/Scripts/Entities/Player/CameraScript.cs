using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	public bool moveAuto; //should the camera move automatically? (This is the old behaviour)
	public float autoMoveSpeed;

	public GameObject player;

	public float offset; //amount on the X axis that the camera should be ahead/behind of the player
	public bool canMoveBack;
	Vector3 desiredPos; //position to move the camera to

	void Start() {
		desiredPos = transform.position;
	}


	//do this in FixedUpdate because that's when the player moves
	void FixedUpdate() {
		if(moveAuto)
			transform.Translate(autoMoveSpeed * Time.deltaTime,0,0); //moves the camera at this speed every FixedFrame. This speed can be controlled in realtime with KeyPad4, 5 and 6 (see GameManager script) - this is temporary
	}

	//do this in LateUpdate to make sure the player's position will not further change in this frame
	void LateUpdate() {
		if(moveAuto)
			return;
		
		if(player.transform.position.x + offset > transform.position.x) { //the player has moved forward
			desiredPos.x = player.transform.position.x + offset; //move with the player
		} else { //the player has moved backward or not moved at all
			if(canMoveBack) {
				desiredPos.x = player.transform.position.x + offset; //move back
			} else {
				desiredPos.x = transform.position.x; //the camera stays in the same place
			}
		}
		

		transform.position = desiredPos;
	}

	void UnbedingtBildschrimSchütteln() {

	}

	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawLine(new Vector3(transform.position.x - offset,transform.position.y - Camera.main.orthographicSize),new Vector3(transform.position.x - offset,transform.position.y + Camera.main.orthographicSize));
	}
}
