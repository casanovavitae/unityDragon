﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airship : EnemyGeneric {


	// Use this for initialization
	protected override void Start () 
	{
		base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		base.Update();
	}

	// shoudAttack überschreiben: Zeppeline können den Spieler immer sehen
	private bool shouldAttack()
	{
		return true;
	}
}