using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraverseSpline: MonoBehaviour, INavigation
{
	public BezierSpline spline;
	private float progress;
	private EnemyGeneric enemyObj;
	private Rigidbody2D body;
	private float speed;

	protected virtual void Start () 
	{
		enemyObj = GetComponent<EnemyGeneric>();
		body = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		FindDestination();

		
		// Distanz annähernd berechnen
		//		Vector2 p1 = spline.GetPoint(progress - 1f);
		//		Vector2 p2 = spline.GetPoint(progress);
		//
		//		print("Distanz: " + Vector2.Distance(p1, p2));
		//		print("GetPoint Progress: " + p2);
		//		print("GetPoint Progress-0.1f: " + p1);
		//
		//		float distance = Vector2.Distance(p1, p2);
		//		float angle = Vector2.Angle(p1, p2);
		//
		//		float radian = Mathf.Deg2Rad * angle;
		//
		//		float x = distance * Mathf.Cos(angle);
		//		float y = distance * Mathf.Sin(angle);

		//body.MovePosition(spline.GetPoint(progress));


	}

	public Vector2 FindDestination()
	{
		if(enemyObj.MovesAtSpeed > 0)
		{
			speed = 1f / enemyObj.MovesAtSpeed * 100f;
			progress += Time.deltaTime / speed;
		}

		if(progress > 1f)
			progress = 0f;

		print( spline.GetPoint(progress) );
		return spline.GetPoint(progress);
	}

	//void FixedUpdate() {
	//	GetComponent<Rigidbody2D>().MovePosition(spline.GetPoint(progress)); //@João: change this later to integrate it with the enemy's script
	//}
}
