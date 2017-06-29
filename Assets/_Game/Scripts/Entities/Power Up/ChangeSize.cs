using UnityEngine;

//Notice that we inherit from PowerUpBase, it contains the general behaviour of a powerup
//Here we just implement the specific power up logic
public class ChangeSize : PowerUpBase
{
    //How much units should the paddle change when this powerup is picked up?
    //Can also be negative to shrink the paddle!
    public Vector3 SizeIncrease = Vector3.zero;

    public Vector3 MinimalPaddleSize = new Vector3(0.4F, 1, 0.4F);

    //Notice how we override we the OnPickup method of the base class  
    protected override void OnPickup()
    {
        //Call the default behaviour of the base class frist
        base.OnPickup();

        //Then do the powerup specific behaviour, changing the size in this case
        Paddle p = FindObjectOfType(typeof(Paddle)) as Paddle;
        p.transform.localScale += SizeIncrease;

        //Limit the minimal size of the paddle
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