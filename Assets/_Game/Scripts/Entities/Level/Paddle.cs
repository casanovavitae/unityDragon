using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour
{
    public float freedom = 19.25F;

    private bool isFirstTap = true;

    void Update()
    {
        //Check if left mousebutton is down or a finger is touching the screen
        if (Input.GetMouseButton(0) && !isFirstTap)
        {
            
            Plane p = new Plane(Vector3.up, transform.position);

         
            
                //Use current position as starting point
                Vector3 position = transform.position;
      
                //Apply the new position
                transform.position = position;
            }
            else
            {
                //The ray missed
            }

           
            Vector3 limitedPosition = transform.position;
            if (Mathf.Abs(limitedPosition.x) > freedom)
            {
            
                limitedPosition.x = Mathf.Clamp(transform.position.x, -freedom, freedom);
                transform.position = limitedPosition;
            }
        }

        
    }
