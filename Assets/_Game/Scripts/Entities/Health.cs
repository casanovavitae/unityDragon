using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	[SerializeField]
	float curHealth;
	[SerializeField]
	float maxHealth;

	[SerializeField]
	DeathEvent[] deathEventPrefabs;

	bool isQuitting; //set to true when the editor/game ends, to avoid trying to spawn death events when the scene closes (because OnDestroy is called)

	// Use this for initialization
	protected virtual void Start () {
		if(maxHealth == 0) {
			maxHealth = 1;
		}
		if(curHealth == 0 || curHealth>maxHealth) {
			curHealth = maxHealth;
		}
	}

	// Update is called once per frame
	protected virtual void Update () {
		
	}

	public virtual void TakeDamage(float damage) {
		curHealth -= damage;

		if(curHealth <= 0) {
			Die();
		}
	}

	/// <summary>
	/// Die happens BEFORE the object is destroyed. The last thing that is called in Die is always Destroy(gameObject). AFTER that, a DeathEvent (if any) will be triggered by Unity's OnDestroy event.
	/// When overriding, either call base.Die at the end or don't call it at all and implement Destroy(gameObject)
	/// </summary>
	protected virtual void Die() {
		Destroy(gameObject);
	}

	protected void OnApplicationQuit() {
		isQuitting = true;
	}

	protected virtual void OnDestroy() {
		if(isQuitting)
			return;
		for(int i = 0;i < deathEventPrefabs.Length;i++) {
			DeathEvent deathEventObj = Instantiate(deathEventPrefabs[i],transform.position,Quaternion.identity);
			//deathEventObj.name = deathEventObj.gameObject.name; //Mostly for debug purposes. Probably unecessary
		}
	}
}
