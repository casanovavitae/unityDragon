using UnityEngine;

public class Ball : MonoBehaviour
{
    //Make the min and max speed to be configurable in the editor.
    public float MinimumSpeed = 25;
    public float MaximumSpeed = 30;

   
    //Don't move the ball unless you tell it to
    private bool hasBeenLaunched = false;

   
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (hasBeenLaunched)
        {
            Rigidbody rigidBody = GetComponent<Rigidbody>();

            //Get current speed and direction
            Vector3 direction = rigidBody.velocity;
            float speed = direction.magnitude;
            direction.Normalize();

           
            {
             
                
                //Apply it back to the ball
                rigidBody.velocity = direction * speed;   
            }

            if (speed < MinimumSpeed || speed > MaximumSpeed)
            {
                //Limit the speed so it always above min en below max
                speed = Mathf.Clamp(speed, MinimumSpeed, MaximumSpeed);

           
                //Note that we don't use * Time.deltaTime here since we set the velocity once, not every frame.
                rigidBody.velocity = direction * speed;   
            }
        }
    }

    //When the bottom of the field it hit destroy the ball. 
    //Note: that the bottom collider is marked as a Trigger, else it would bounce back, now it goes just through the collider.
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Bottom")
        {
            Destroy(this.gameObject);
        }
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

        //Make sure we start at the minimum speed limit
        launchDirection = launchDirection.normalized * MinimumSpeed;
        launchDirection.z = Mathf.Abs(launchDirection.z);   //Prevent the user from shooting down

        //Apply it to the rigidbody so it keeps moving into that direction (untill it hits a block or wall ofcourse)
        GetComponent<Rigidbody>().velocity = launchDirection;

        hasBeenLaunched = true;
    }
}
