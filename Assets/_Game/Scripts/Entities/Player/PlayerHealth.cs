using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health{

	// Use this for initialization
	protected override void Start () {
		base.Start();
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update();
	}

	public override void TakeDamage(float damage) {
		base.TakeDamage(damage);
	}

	protected override void Die() {


		base.Die();
	}

	protected override void OnDestroy() {
		base.OnDestroy();
	}
}
