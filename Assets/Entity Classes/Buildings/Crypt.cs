using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crypt : Building {

	protected override void  Awake() {
		base.Awake ();
	}
	protected override void Start () {
		base.Start ();

		prefabs = new GameObject[1];

		prefabs[0] = (GameObject)Resources.Load ("Skeleton");

		hitPoints = maxHitPoints = RTS.Hall.maxHitPoints;
		entityName = "Crypt";
	}
	protected override void Update () {
		base.Update ();
	}
	protected override void OnGUI () {
		base.OnGUI ();
	}
		
}
