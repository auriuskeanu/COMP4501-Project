using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Unit {


	protected override void  Awake() {
		base.Awake ();
	}
	protected override void Start () {
		base.Start ();

		entityName = "Skeleton";

		hitPoints = maxHitPoints = RTS.Skeleton.maxHitPoints;
		atkSpd = RTS.Skeleton.atkspd;
		damage = RTS.Skeleton.damage;
		range = RTS.Skeleton.range;
		moveSpeed = RTS.Skeleton.moveSpeed;
		turnRate = RTS.Skeleton.turnRate;
		state = (int)RTS.Skeleton.States.Idle;
	}
	protected override void Update () {
		base.Update ();

		switch (state) {
		case (int)RTS.Skeleton.States.Idle:
			anim.SetInteger ("aState", 0);
			break;
		case (int)RTS.Skeleton.States.Attack:
			if (target != null) {
				attackEntity (target);
			}
			break;
		case (int)RTS.Skeleton.States.Walk:
			goTo (destination);
			if (Vector2.Distance (new Vector2 (transform.position.x, transform.position.z), new Vector2 (destination.x, destination.z)) < 0.1) {
				destination = RTS.RTSConst.invalidPosition;
				state = (int)RTS.Skeleton.States.Idle;
			}
			break;
		}
	}
	protected override void OnGUI () {
		base.OnGUI ();
	}
}
