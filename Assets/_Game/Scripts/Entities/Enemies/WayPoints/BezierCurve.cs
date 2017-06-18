using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour {

	public Vector2[] points;

	public Vector2 GetPoint(float t)
	{
		return transform.TransformPoint (Bezier.GetPoint (points [0], points [1], points [2], points[3], t));
	}

	public Vector2 GetVelocity(float t)
	{
		return transform.TransformPoint (Bezier.GetFirstDerivative (points[0], points[1], points[2], points[3], t));
		//return new Vector2(0, 0);
	}

	public Vector2 GetDirection(float t)
	{
		//return GetVelocity(t).normalized;

		return new Vector2(0, 0);
	}

	public void Reset()
	{
		points = new Vector2[] {
			new Vector2 (1f, 0f),
			new Vector2 (2f, 0f),
			new Vector2 (3f, 0f),
			new Vector2 (4f, 0f)
		};
	}
}
