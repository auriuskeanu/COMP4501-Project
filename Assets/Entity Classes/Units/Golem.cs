using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : Unit {


	protected override void  Awake() {
		base.Awake ();
	}
	protected override void Start () {
		base.Start ();

		entityName = "Golem";

		hitPoints = maxHitPoints = RTS.Golem.maxHitPoints;
		atkSpd = RTS.Golem.atkspd;
		damage = RTS.Golem.damage;
		range = RTS.Golem.range;
		moveSpeed = RTS.Golem.moveSpeed;
		turnRate = RTS.Golem.turnRate;
		state = (int)RTS.Golem.States.Idle;
	}
	protected override void Update () {
		base.Update ();

		switch (state) {
		case (int)RTS.Golem.States.Idle:
			anim.SetInteger ("aState", 0);
			break;
		case (int)RTS.Golem.States.Attack:
			if (target != null) {
				attackEntity (target);
			}
			break;
		case (int)RTS.Golem.States.Walk:
			goTo (destination);
			if (Vector2.Distance (new Vector2 (transform.position.x, transform.position.z), new Vector2 (destination.x, destination.z)) < 0.1) {
				destination = RTS.RTSConst.invalidPosition;
				state = (int)RTS.Golem.States.Idle;
			}
			break;
		}
	}
	protected override void OnGUI () {
		base.OnGUI ();
	}
}
