using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseScoreEvent : DeathEvent {

	public int pointsAwarded;

	protected override void StartEvent() {
		Global.uiManager.IncreaseScore(pointsAwarded);
	}
}
