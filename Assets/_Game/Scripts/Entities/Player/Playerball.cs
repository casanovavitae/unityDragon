using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerball : MonoBehaviour {

    public float ballInitialVelocity = 600f;

    private Rigidbody2D rb;
    private bool ballInplay;

	void Awake () {

        rb = GetComponent<Rigidbody2D>();
		
	}
	
	// Ctrl
	void Update () {
		if (Input.GetButtonDown("Fire1")&& ballInplay == false)
        {
            transform.parent = null;
            ballInplay = true;
            rb.isKinematic = false;
            rb.AddForce(new Vector3(ballInitialVelocity, 0, 0));

        }
    }
}
