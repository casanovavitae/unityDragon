using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflection : MonoBehaviour {

	public float smoothMaxAngle; //angle at the farthest point of contact
	float sizeRatio;

	[Tooltip("Different reflection sections. The value is the angle at which to reflect the projectiles")]
	public float[] sections; //first element is always 0

	public delegate Vector2 ReflectionFunction(Vector2 pointOfContact);
	public ReflectionFunction getReflectionVector;

	public bool smoothReflection;

	// Use this for initialization
	void Start () {
		sizeRatio = GetComponent<BoxCollider2D>().bounds.extents.y;

		//make sure the angle of the first section is always 0
		if(sections.Length > 0 && sections[0] != 0) {
			float[] temp = new float[sections.Length + 1];
			temp[0] = 0; //make first section 0
			for(int i = 1;i < temp.Length;i++) { //add the other sections/angles after the 0
				temp[i] = sections[i - 1];
			}
			sections = temp;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(smoothReflection) {
			getReflectionVector = SmoothReflection;
		} else {
			getReflectionVector = SectionedReflection;
		}
	}

	public Vector2 SmoothReflection(Vector2 pointOfContact) {
		float distanceToCenter = pointOfContact.y - transform.position.y; //will be negative for collisions below the center
		float angle = smoothMaxAngle * (distanceToCenter / sizeRatio);

		return new Vector2(Mathf.Cos(angle*Mathf.Deg2Rad),Mathf.Sin(angle * Mathf.Deg2Rad));
	}

	public Vector2 SectionedReflection(Vector2 pointOfContact) {
		float distanceToCenter = pointOfContact.y - transform.position.y;
		float distanceToSizeRatio = Mathf.Abs(distanceToCenter / sizeRatio);
		float sectionSize = sizeRatio / sections.Length;
		float angle=0;
		

		for(int i = 0;i < sections.Length;i++) {
			if(Mathf.Abs(distanceToCenter) >= sectionSize*i) {
				angle = sections[i] * Mathf.Sign(distanceToCenter);
				//Debug.Log("i " + i);
			}
		}

		//Debug.Log("distanceToCenter "+distanceToCenter);
		//Debug.Log("distanceToSizeRatio "+distanceToSizeRatio);
		//Debug.Log("sectionSize " + sectionSize);
		//Debug.Log("angle " + angle);
		//Debug.Log(new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad),Mathf.Sin(angle * Mathf.Deg2Rad)));

		return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad),Mathf.Sin(angle * Mathf.Deg2Rad));
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.blue;
		float xLeft = GetComponent<BoxCollider2D>().bounds.min.x;
		float xRight = GetComponent<BoxCollider2D>().bounds.max.x;
		float yBottom= transform.position.y - GetComponent<BoxCollider2D>().bounds.extents.y;
		for(int i = 0;i < sections.Length*2+1;i++) {
			float y= yBottom + (sizeRatio / sections.Length) * i;
			Gizmos.DrawLine(new Vector2(xLeft,y),new Vector2(xRight,y));
		}
	}
}