using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneric : MonoBehaviour {

	public uint health = 1;
	public bool canAttack = true;

	[SerializeField]
	private float movesAtSpeed = 0;				// Geschwindigkeit, mit der sich Enemy bewegt
	public float MovesAtSpeed					// Andere Scripts sollen die Geschwindigkeit nur lesen können
	{
		get {
			return movesAtSpeed;
		}
	}
	public float attacksWithIntensity; 			// Geschwindigkeit der Projektile, die Enemy feuert
	public float reloadsInSeconds;				// Geschwindigkeit des Nachladens

	[SerializeField]
	protected float reloadTimer;

	protected INavigation navigationBehavior;

	// Für Bewegungen
	protected Rigidbody2D body;
	protected Vector2 positionOfPlayer;

	// Angriffe sind in Attack.cs-Script ausgelagert (delegiert)
	protected Attack attack;
	// Damit Enemy Relation zu Protagonist erstellen kann
	public GameObject player;

	// Use this for initialization
	protected virtual void Start () 
	{
		// Geschwindigkeit korrigieren
		//movesAtSpeed *= 0.1f;

		// Attack-Interface verknüpfen
		attack = GetComponent<Attack>();
		// Body zum Bewegen verknüpfen
		body = GetComponent<Rigidbody2D>();

		navigationBehavior = GetComponent<INavigation>();

		//João: don't do this so that we can change it in the editor
		// Initialisiere ReloadTimer
		//reloadTimer = reloadsInSeconds;

	}

	// Fragt ab, ob Gegner überhaupt angreifen sollte. Standard ist nur bei Sichtkontakt!
	// Falls ein Gegner "immer" Sichtkontakt hat, wird das in der Unterklasse überschrieben
	private bool shouldAttack()
	{
		// Position des Spielers abfragen
		positionOfPlayer = player.transform.position;

		// Besteht Sichtkontakt zwischen Enemy und Spieler?
		Vector2 direction = ((Vector2)transform.position - positionOfPlayer).normalized;

		if (Physics2D.Raycast (transform.position, direction)) //@Willi please only comment on my stuff, but don't change it
			return true;
		else
			return false;

		// Was von Jo:
		//		if(Vector2.Distance(Camera.main.transform.position, transform.position) < 25 && transform.position.x>positionOfPlayer.x)

	}

	private void isAttacking()
	{
		if (canAttack && shouldAttack())
			attack.Shoot ((Vector2)(player.transform.position - transform.position).normalized, attacksWithIntensity);
	}
	
	// Update is called once per frame
	protected virtual void Update ()
	{
		manageReloadTimer();

		if (movesAtSpeed > 0)
			body.MovePosition(navigationBehavior.FindDestination());

		// Wenn Gegner aus Bildschirm geflogen ist, soll er sterben
		// Ne doch nich
//		if(transform.position.x < Camera.main.GetCameraBounds(true) - GetComponent<SpriteRenderer>().bounds.extents.x) 
//		{
//			Die();
//		}
	}

	//void OnDrawGizmos()

	private void manageReloadTimer()
	{
		// Reload-Timer zählt
		reloadTimer -= Time.deltaTime;

		if (reloadTimer <= 0)
		{
			isAttacking();
			reloadTimer = reloadsInSeconds;
		}
	}	
}
