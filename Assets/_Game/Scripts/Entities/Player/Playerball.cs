using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerball : MonoBehaviour {

    public float ballInitialVelocity = 600f;
    public float MinimumSpeed = 25;
    public float MaximumSpeed = 30;
    private Rigidbody2D rb;
    private bool hasBeenLaunched = false;
    private bool ballInplay;
    public float speed;
    public Vector2 direction;
    public bool simulated;
    void Awake () {

        rb = GetComponent<Rigidbody2D>();
	}
    public void Launch()
    {
       
    }
    // Ctrl
    void Update () {
		if (Input.GetButtonDown("Fire1")&& ballInplay == false)
        {
            transform.parent = null;
            ballInplay = true;
            rb.isKinematic = false;
            rb.simulated = true;
            rb.AddForce(new Vector3(ballInitialVelocity, 0, 0));

        }
    }
}
