using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : Building {

	protected override void  Awake() {
		base.Awake ();
	}
	protected override void Start () {
		base.Start ();
		prefabs = new GameObject[1];

		player = GameObject.Find ("Player").GetComponent<Player>();
		prefabs[0] = (GameObject)Resources.Load ("Swordsman");
		/*
		prefabs[1] = (GameObject)Resources.Load("Spearman");
		prefabs[2] = (GameObject)Resources.Load("Archer");
		 */
		hitPoints = maxHitPoints = RTS.Barracks.maxHitPoints;
		entityName = "Barracks";
	}
	protected override void Update () {
		base.Update ();
	}
	protected override void OnGUI () {
		base.OnGUI ();
	}

	public override void keyPress(Player controller) {
		if (Input.GetKeyDown("a")) {
			if (player.cash >= RTS.Swordsman.cost) {
				spawn (0);
				player.cash -= RTS.Swordsman.cost;
			}
		}
	}
}
