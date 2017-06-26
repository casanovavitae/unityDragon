using UnityEngine;


public class ExtraBall : Powerup
{
    
    public GameObject PlayerballPrefab;

   
    protected override void OnPickup()
    {
       
        base.OnPickup();

        // Create a new ball and launch it
        GameObject ball = Instantiate(PlayerballPrefab, transform.position, Quaternion.identity) as GameObject;
        ball.GetComponent<Playerball>().Launch();
    }
}