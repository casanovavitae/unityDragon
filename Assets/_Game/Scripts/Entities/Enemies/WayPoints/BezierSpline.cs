using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierSpline : MonoBehaviour {

	[SerializeField]
	private bool _loop;
	public bool loop {
		get 
		{
			return _loop;
		}
		set 
		{
			_loop = value;
			if(value == true)
			{
				modes[modes.Length - 1] = modes[0];
				SetControlPoint(0, points[0]);
			}
		}
	}

	[SerializeField]
	private Vector2[] points;

	[SerializeField]
	private BezierControlPointMode[] modes;

	public int ControlPointCount {
		get {
			return points.Length;
		}
	}

	public Vector2 GetControlPoint (int index)
	{
		return points[index];
	}

	public void SetControlPoint(int index, Vector2 point)
	{
		if(index % 3 == 0)
		{
			Vector2 delta = point - points[index];

			if(loop)
			{
				if(index == 0)
				{
					points[1] += delta;
					points[points.Length - 2] += delta;
					points[points.Length - 1] = point;
				} 
				else if(index == points.Length - 1)
				{
					points[0] = point;
					points[1] += delta;
					points[index - 1] += delta;
				} 
				else
				{
					points[index - 1] += delta;
					points[index + 1] += delta;
				}
			} 
			else
			{
				if(index > 0)
					points[index - 1] += delta;
				if(index + 1 < points.Length)
					points[index + 1] += delta;
			}
		}


		points[index] = point;
		EnforceMode(index);
	}

	public void SetControlPointMode (int index, BezierControlPointMode mode)
	{
		int modeIndex = (index + 1) / 3;
		modes[modeIndex] = mode;

		if(loop)
		{
			if(modeIndex == 0)
				modes[modes.Length - 1] = mode;
			else if(modeIndex == modes.Length - 1)
					modes[0] = mode;
		}
				
		EnforceMode(index);
	}

	public BezierControlPointMode GetControlPointMode (int index)
	{
		return modes[(index + 1) / 3];
	}

	private void EnforceMode (int index)
	{
		int modeIndex = (index + 1) / 3;

		BezierControlPointMode mode = modes[modeIndex];
		if(mode == BezierControlPointMode.Free || !loop && modeIndex == 0 || modeIndex == modes.Length - 1)
		{
			return;
		}

		int middleIndex = modeIndex * 3;
		int fixedIndex;
		int enforcedIndex;

		if(index <= middleIndex)
		{
			fixedIndex = middleIndex - 1;
			if(fixedIndex < 0)
				fixedIndex = points.Length - 2;
			
			enforcedIndex = middleIndex + 1;
			if(enforcedIndex >= points.Length)
				enforcedIndex = 1;
		} 
		else
		{
			fixedIndex = middleIndex + 1;
			if(fixedIndex >= points.Length)
				fixedIndex = 1;
			
			enforcedIndex = middleIndex - 1;
			if(enforcedIndex < 0)
				enforcedIndex = points.Length - 2;
		}

		Vector2 middle = points[middleIndex];
		Vector2 enforcedTangent = middle - points[fixedIndex];

		if(mode == BezierControlPointMode.Aligned)
			enforcedTangent = enforcedTangent.normalized * Vector2.Distance(middle, points[enforcedIndex]);

		points[enforcedIndex] = middle + enforcedTangent;
	}

	public Vector2 GetPoint(float t)
	{
		int i;

		if(t >= 1f)
		{
			t = 0f;
			i = points.Length - 4;

		}
		else
		{
			t = Mathf.Clamp01(t) * CurveCount;
			i = (int)t;
			t -= i;
			i *= 3;
		}

		return transform.TransformPoint (Bezier.GetPoint (points[i], points[i + 1], points[i + 2], points[i + 3], t));
	}

//	public Vector2 GetInterpolatedPoint(float t)
//	{
//		int lastPoint = (int)t - 1;
//
//		int i;
//
//		if(t >= 1f)
//		{
//			t = 1f;
//			i = points.Length - 4;
//
//		}
//		else
//		{
//			t = Mathf.Clamp01(t) * CurveCount; // T ist Anzahl der einzelnen Kurven, normalisiert auf 0-1
//			i = (int)t;
//			t -= i;
//			i *= 3;
//		}
//
//		Vector2 nextPoint = (Bezier.GetPoint (points[i], points[i + 1], points[i + 2], points[i + 3], t));
//
//
//		return transform.TransformPoint (Bezier.GetPoint (points[t], points[lastPoint], t));
//	}

	public Vector2 GetVelocity(float t)
	{
		int i;

		if(t >= 1f)
		{
			t = 1f;
			i = points.Length - 4;
		} 
		else
		{
			t = Mathf.Clamp01(t) * CurveCount;
			i = (int)t;
			t -= i;
			i *= 3;
		}

		return transform.TransformPoint (Bezier.GetFirstDerivative (points[i], points[i + 1], points[i + 2], points[i + 3], t)) - transform.position;
	}

	public Vector2 GetDirection(float t)
	{
		return GetVelocity(t).normalized;
	}

	public void AddCurve()
	{
		// Jede einzelne Kurve besteht aus vier Punkten, von denen der erste mit dem letzten der vorherigen überein stimmt.
		// Jede weitere Kurve fügt deshalb drei neue Punkte hinzu.

		// Letzter Punkt des Spline entspricht ersten Punkt der neuen Kurve, die später dem Spline hinzugefügt wird
		Vector2 point = points[points.Length - 1];
		// Das Spline besteht aus einem Array, das in der Länge um die drei neuen Punkte der weiteren Kurve erweitert werden soll
		Array.Resize (ref points, points.Length + 3);

		point.x += 1f;
		points [points.Length - 3] = point;
		point.x += 1f;
		points [points.Length - 2] = point;
		point.x += 1f;
		points [points.Length - 1] = point;

		Array.Resize(ref modes, modes.Length + 1);
		modes[modes.Length - 1] = modes[modes.Length - 2];
		EnforceMode(points.Length - 4);

		if(loop)
		{
			points[points.Length - 1] = points[0];
			modes[modes.Length - 1] = modes[0];
			EnforceMode(0);
		}
	}

	public void RemoveCurve()
	{
		// Die letzte Kurve soll wieder entfernt werden
		Array.Resize(ref points, points.Length - 3);
	}

	public int CurveCount
	{
		get {
			return (points.Length - 1) / 3;
		}
	}

	public void Reset()
	{
		points = new Vector2[] {
			new Vector2 (1f, 0f),
			new Vector2 (2f, 0f),
			new Vector2 (3f, 0f),
			new Vector2 (4f, 0f)
		};

		modes = new BezierControlPointMode[] {
			BezierControlPointMode.Free,
			BezierControlPointMode.Free
		};
	}
}