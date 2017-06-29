using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed;
    public Vector2 direction;
    
    Rigidbody2D rigid;

	[SerializeField]
	Vector3 debugVelocity;

    // Use this for initialization
    void Start(){
        rigid = GetComponent<Rigidbody2D>();
        //Destroy(gameObject,10);

        //
    }

    // Update is called once per frame
    void Update(){
		//check if the bullet is out of the camera view frustrum. If yes, destroy it. @João: change to pooling
		float[] cameraBounds = Camera.main.GetCameraBounds();
		if(transform.position.x < cameraBounds[0] - GetComponent<SpriteRenderer>().bounds.extents.x || transform.position.x > cameraBounds[1] + GetComponent<SpriteRenderer>().bounds.extents.x) {
			Destroy(gameObject);
		}

		debugVelocity = rigid.velocity;
	}
	
    public void SetVelocity(Vector2 direction, float speed){
        this.direction = direction;
        this.speed = speed;
        if (rigid == null)
        {
            rigid = GetComponent<Rigidbody2D>();
        }
        rigid.velocity = direction.normalized * speed;

		transform.rotation=Quaternion.LookRotation(Vector3.forward, Vector3.Cross(Vector3.forward, direction));
    }

}




