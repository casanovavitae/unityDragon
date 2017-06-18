using System;
using UnityEngine;

public static class Bezier
{
	public static Vector2 GetPoint (Vector2 point0, Vector2 point1, Vector2 point2, Vector2 point3, float t)
	{
		// Statt Lerp wird eine quadratische Formel genutzt
		// return Vector2.Lerp (Vector2.Lerp(point0, point1, t), Vector2.Lerp(point1, point2, t), t);

		t = Mathf.Clamp01 (t);
		float oneMinusT = 1f - t;

		return 	(oneMinusT * oneMinusT * oneMinusT * point0) + 
				(3f * oneMinusT * oneMinusT * t * point1) + 
				(3f * oneMinusT * t * t * point2) +
				(t * t * t * point3);
	}

	// Ableitungen berechnen
	public static Vector2 GetFirstDerivative (Vector2 point0, Vector2 point1, Vector2 point2, Vector2 point3, float t)
	{
		t = Mathf.Clamp01 (t);
		float oneMinusT = 1f - t;

		return 	(3f * oneMinusT * oneMinusT * (point1 - point0)) +
				(6f * oneMinusT * t * (point2 - point1)) +
				(3f * t * t * (point3 - point2));
	}
}
