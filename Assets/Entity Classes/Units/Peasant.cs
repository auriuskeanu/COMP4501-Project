using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peasant : Unit {

	protected override void  Awake() {
		base.Awake ();
	}
	protected override void Start () {
		base.Start ();

		entityName = "Peasant";

		cost = RTS.Peasant.cost;
		player = GameObject.Find ("Player").GetComponent<Player>();
		hitPoints = maxHitPoints = RTS.Peasant.maxHitPoints;
		atkSpd = RTS.Peasant.atkspd;
		damage = RTS.Peasant.damage;
		range = RTS.Peasant.range;
		moveSpeed = RTS.Peasant.moveSpeed;
		turnRate = RTS.Peasant.turnRate;
		state = (int)RTS.Peasant.States.Idle;
	}
	protected override void Update () {
		base.Update ();

		switch (state) {
		case (int)RTS.Peasant.States.Idle:
			anim.SetInteger ("aState", 0);
			break;
		case (int)RTS.Peasant.States.Walk:
			goTo (destination);
			if (Vector2.Distance (new Vector2 (transform.position.x, transform.position.z), new Vector2 (destination.x, destination.z)) < 0.1) {
				destination = RTS.RTSConst.invalidPosition;
				state = (int)RTS.Peasant.States.Idle;
			}
			break;
		case (int)RTS.Peasant.States.Attack:
			if (target != null)
				attackEntity (target);
			break;
		case (int)RTS.Peasant.States.Repair:
			if (target != null) {
				if (target.hitPoints >= target.maxHitPoints) {
					target = null;
					state = (int)RTS.Peasant.States.Idle;
				} else
				repairBuilding (target);
			}
			break;
		case (int)RTS.Peasant.States.Harvest:
			harvestResource ();
			break;
		}
	}
	protected override void OnGUI () {
		base.OnGUI ();
	}

	public override void rightClick(GameObject entity, Vector3 point, Player player) {
		if (!entity.GetComponentInParent<Entity> ()) {
			this.setDestination (point);
		} 
		else if (entity.GetComponent<Entity> () != this) {
			this.target = entity.GetComponent<Entity>();
			if (target is Building && target.player == this.player) {
				state = (int)RTS.Peasant.States.Repair;
			} 
			else if (target is Resource) {
				state = (int)RTS.Peasant.States.Harvest;
			}
			else if (target is Unit) {
				state = (int)RTS.Peasant.States.Attack;
			}
		}

	}

	private void repairBuilding(Entity e) {
		Building b = (Building)e;
		destination = b.getPosition ();
		if (objectDistance(e) > range) {
			goTo (b.getPosition());
		} else {
			//repair code here. basically like attack code except the building's hp goes UP!
			anim.SetInteger ("aState", 0);
			if (pointTo(e.getPosition())) {
				if (e.hitPoints < e.maxHitPoints) {
					if (cd <= 0) {
						cd = atkSpd;
						anim.SetTrigger ("repair");
					} else
						cd -= Time.deltaTime;
				} else if (e.hitPoints == e.maxHitPoints) {
					target = null;
					state = 0;
					return;
				}
			}
		}
	}

	public void repair() {
		target.healDamage (damage);
	}

	public void harvestResource() {
		Resource r = (Resource)target;
		destination = r.getPosition ();
		if (objectDistance (r) > range) {
			goTo (destination);
		} else {
			anim.SetInteger ("aState", 0);
			if (pointTo (destination)) {
				if (cd <= 0) {
					cd = atkSpd;
					anim.SetTrigger ("harvest");
				} else
					cd -= Time.deltaTime;
			}
		}
	}
	public void harvest() {
		Resource r = (Resource)target;
		this.player.getCash (r.moneyPer);
	}
}
