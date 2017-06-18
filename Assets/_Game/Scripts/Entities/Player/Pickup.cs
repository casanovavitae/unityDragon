using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

	public string pickupName;
	public bool wasPicked;
	

	// Use this for initialization
	void Start () {
		wasPicked = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public virtual Pickup PickThisUp() {
		wasPicked = true;
		return this;

	}


}
