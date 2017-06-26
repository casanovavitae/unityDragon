using UnityEngine;


public class ChangeSize : Powerup
{
    //How much units should the paddle change when this powerup is picked up?
    //Can also be negative to shrink the paddle!
    public Vector3 SizeIncrease = Vector3.zero;

    public Vector3 MinimalPaddleSize = new Vector3(0.4F, 1, 0.4F);
 
    protected override void OnPickup()
    {
   
        base.OnPickup();


        Paddle p = FindObjectOfType(typeof(Paddle)) as Paddle;
        p.transform.localScale += SizeIncrease;

      
        Vector3 size = p.transform.localScale;
        if (size.x < MinimalPaddleSize.x)
            size.x = MinimalPaddleSize.x;

        if (size.y < MinimalPaddleSize.y)
            size.y = MinimalPaddleSize.y;

        if (size.z < MinimalPaddleSize.z)
            size.z = MinimalPaddleSize.z;

        p.transform.localScale = size;
    }
}