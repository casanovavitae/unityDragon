using UnityEngine;


public class ExtraBall : Powerup
{
    
    public GameObject PlayerballPrefab;

   
    protected override void OnPickup()
    {
       
        base.OnPickup();

        // Create a new ball and launch it
        GameObject Playerball = Instantiate(PlayerballPrefab, transform.position, Quaternion.identity) as GameObject;
        Playerball.GetComponent<Playerball>().Launch();
    }
}