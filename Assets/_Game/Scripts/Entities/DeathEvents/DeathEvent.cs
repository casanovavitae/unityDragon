using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Death events happen AFTER the caller dies. Eg. spawn particle effects, sounds, explosions, a story bit, etc.
/// </summary>
public class DeathEvent : MonoBehaviour {

	[SerializeField]
	protected GameObject[] objectsToSpawn;

	[SerializeField]
	[Tooltip("Time to wait before the death event starts (things get spawned, etc)")]
	protected float timeBeforeStart;

	// Use this for initialization
	protected virtual void Start () {
		StartCoroutine(WaitAndStart(timeBeforeStart));
	}

	// Update is called once per frame
	protected virtual void Update () {
		
	}

	protected virtual IEnumerator WaitAndStart(float time) { //time must be positive or zero
		if(time == 0) {
			yield return null;
		} else {
			yield return new WaitForSeconds(Mathf.Abs(time)); //double check it's not negative
		}

		StartEvent();
	}

	protected virtual void StartEvent() {
		if(objectsToSpawn.Length > 0) {
			for(int i = 0;i < objectsToSpawn.Length;i++) {
				Instantiate(objectsToSpawn[i], transform.position, Quaternion.identity,transform );
			}
		}
	}
}
