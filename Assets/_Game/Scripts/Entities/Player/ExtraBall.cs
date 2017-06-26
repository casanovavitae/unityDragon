using UnityEngine;


public class ExtraBall : Powerup
{
    
    public GameObject BallPrefab;

   
    protected override void OnPickup()
    {
       
        base.OnPickup();

        //Create a new ball and launch it
        GameObject ball = Instantiate(BallPrefab, transform.position, Quaternion.identity) as GameObject;
        ball.GetComponent<Ball>().Launch();
    }
}