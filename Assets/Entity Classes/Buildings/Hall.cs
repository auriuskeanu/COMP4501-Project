using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hall : Building {

	protected override void  Awake() {
		base.Awake ();
	}
	protected override void Start () {
		base.Start ();

		prefabs = new GameObject[1];

		player = GameObject.Find ("Player").GetComponent<Player>();
		prefabs[0] = (GameObject)Resources.Load ("Peasant");

		hitPoints = maxHitPoints = RTS.Hall.maxHitPoints;
		entityName = "Hall";
	}
	protected override void Update () {
		base.Update ();
	}
	protected override void OnGUI () {
		base.OnGUI ();
	}
		
	public override void keyPress(Player controller) {
		if (Input.GetKeyDown ("a")) {
			if (player.cash >= RTS.Peasant.cost) {
				spawn (0);
				player.cash -= RTS.Peasant.cost;
			}
		}
	}
}
