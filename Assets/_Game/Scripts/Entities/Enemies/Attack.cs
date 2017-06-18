using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

	public GameObject bulletPrefab;	

	public void Shoot(Vector2 direction, float speed) {
		direction.Normalize();
		GameObject bullet = Instantiate(bulletPrefab,new Vector2(transform.position.x - GetComponent<SpriteRenderer>().bounds.extents.x, transform.position.y) + direction * 2,Quaternion.identity);
		bullet.GetComponent<Bullet>().SetVelocity(direction,speed);
	}
}