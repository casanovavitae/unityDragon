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
	void Awake () {

        rb = GetComponent<Rigidbody2D>();
	}
    public void Launch()
    {
        //Create a ray from camera to playfield
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane p = new Plane(Vector3.up, transform.position);

        //Create a random vector but make sure it always point "up" (z axis in this case) else it could be launched straight down
        //This is the fallback if the raycast misses for whatever reason. (Theoreticly it can't miss)
        Vector3 launchDirection = new Vector3(Random.Range(-1.0F, 1.0F), 0, Mathf.Abs(Random.value));

        //Shoot the ray to know where the click/tap found place in 3D
        float distance;
        if (p.Raycast(mouseRay, out distance))
        {
            Vector3 clicked3DPos = mouseRay.GetPoint(distance);
            clicked3DPos.y = transform.position.y;  //Don't change the y, keep the ball in the field!

            //Calculate the direction from ball to click
            launchDirection = clicked3DPos - transform.position;
        }
        else
        {
            //The ray missed
        }

        launchDirection = launchDirection.normalized * MinimumSpeed;
        launchDirection.z = Mathf.Abs(launchDirection.z);   
        GetComponent<Rigidbody>().velocity = launchDirection;

        hasBeenLaunched = true;
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
