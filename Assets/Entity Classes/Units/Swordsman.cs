using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordsman : Unit {

	protected override void  Awake() {
		base.Awake ();
	}
	protected override void Start () {
		base.Start ();

		entityName = "Swordsman";

		cost = RTS.Swordsman.cost;
		player = GameObject.Find ("Player").GetComponent<Player>();
		hitPoints = maxHitPoints = RTS.Swordsman.maxHitPoints;
		atkSpd = RTS.Swordsman.atkspd;
		damage = RTS.Swordsman.damage;
		range = RTS.Swordsman.range;
		moveSpeed = RTS.Swordsman.moveSpeed;
		turnRate = RTS.Swordsman.turnRate;
		state = (int)RTS.Swordsman.States.Idle;
	}
	protected override void Update () {
		base.Update ();

		switch (state) {
		case (int)RTS.Swordsman.States.Idle:
			anim.SetInteger("aState", 0);
			break;
		case (int)RTS.Swordsman.States.Walk:
			goTo (destination);
			if (Vector2.Distance (new Vector2 (transform.position.x, transform.position.z), new Vector2 (destination.x, destination.z)) < 0.1) {
				destination = RTS.RTSConst.invalidPosition;
				state = (int)RTS.Swordsman.States.Idle;
			}
			break;
		case (int)RTS.Swordsman.States.Attack:
			if (target != null)
				attackEntity (target);
			break;
		}
	}
	protected override void OnGUI () {
		base.OnGUI ();
	}

	public override void rightClick (GameObject entity, Vector3 point, Player player) {
		base.rightClick (entity, point, player);
	}
}
