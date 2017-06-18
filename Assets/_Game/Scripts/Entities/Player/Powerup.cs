using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : Pickup {

	public float duration;
    public Rigidbody2D rb;

    // P.U Use destroy all enemys
	public void Use() {
		Debug.Log("THE POWERUP HAS BEEN USED: PEW PEW PEW!!!");
        	for(int i = 0;i < Global.enemyManager.enemyList.Length;i++) {
                Destroy(Global.enemyManager.enemyList[i]);
            }
            Destroy(gameObject, duration);
    }

	
    // P.U Air return the bullet Act Q
    public void Air() //@João: change this to a delegate. Create an enum that can be set in the inspector. The enum determines which powerup this is. The only method that should be called from the outside is Use()
    {
        // @cambiar el FindGameObjectsWithTag despues del pitch 
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        int numberOfBullets = bullets.Length;
        Debug.Log("Number of Bullets" + numberOfBullets);
        foreach (GameObject item in bullets)
        {
            //rb = item.GetComponent<Rigidbody2D>();
            //rb.velocity = new Vector3(0, 0, 0);
            //rb.velocity = new Vector3(1, 0, 0);
			item.GetComponent<Bullet>().Reflect(Vector2.right);
        }
		Destroy(gameObject,duration);
		// Typo item in bullets
	}
    // P.U Bomb Act B  (Auto act) hacer un rango para destroy todo dentro del rango 

     /*public void Bomb()
    {
        Debug.Log("The power up has been used BOMB");
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");


        Destroy(gameObject, duration);

     }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            //Action
        }
    }
}
