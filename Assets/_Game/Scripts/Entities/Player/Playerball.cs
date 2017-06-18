using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerball : MonoBehaviour {

    public float ballInitialVelocity = 600f;

    private Rigibody rb;
    private bool ballInplay;

	// Use this for initialization
	void Awake () {

        rb = GetComponet<Rigidbody2D>();
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1")&& ballInplay == false)
        {
            transform.parent = null;
            ballInplay = true;
            rb.isKinematic = false;
            rb.Addforce(new Vector 3(ballInitialVelocity, 0, 0));
        }
    }
}
