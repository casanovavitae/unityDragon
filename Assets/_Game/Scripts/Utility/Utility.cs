using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility {
	public static float[] GetCameraBounds(this Camera cam) { //returns the x coordinate in world space of the camera's view frustrum (first element is the leftmost bound and the second element is the rightmost bound)
		float[] bounds = new float[2];

		bounds[0] = GetCameraBounds(cam, true);
		bounds[1] = GetCameraBounds(cam,false);

		return bounds;
	}

	public static float GetCameraBounds(this Camera cam, bool leftMost=true) { //returns the x coordinate in world space of the camera's view bound (either the leftmost or the rightmost bound)
		if(leftMost) {			
			return cam.ScreenToWorldPoint(Vector3.zero).x;
		} else {
			return cam.ScreenToWorldPoint(new Vector3(Screen.width,0,0)).x;
		}
	}
}
