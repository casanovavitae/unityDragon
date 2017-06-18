using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Tile : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Vector2 pos=transform.localPosition;
		pos.x = Mathf.Floor(pos.x);
		pos.y = Mathf.Floor(pos.y);
		transform.localPosition = pos;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
