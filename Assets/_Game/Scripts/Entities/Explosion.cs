using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	public LayerMask layersToDetect;
	public float radius;
	public int damage;

	// Use this for initialization
	void Start () {
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position,radius,layersToDetect);

		for(int i = 0;i < colliders.Length;i++) {
			if(colliders[i].GetComponent<Health>()!=null)
				colliders[i].GetComponent<Health>().TakeDamage(damage);
		}

		Destroy(gameObject,1);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
