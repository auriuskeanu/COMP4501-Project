using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RTS;

public abstract class Unit : Entity {

	protected float range, turnRate, moveSpeed, damage, atkSpd; //range : attack range, turnRate : degrees per seconds, moveSpeed : units per second, damage : dmg per hit, atkSpd : seconds per attack
	protected int state, aState;

	protected Vector3 destination;
	protected Entity target;
	protected Animator anim;
	protected float cd;

	protected override void  Awake() {
		base.Awake ();
	}
	protected override void Start () {
		base.Start ();
		anim = GetComponent<Animator> ();
		target = null;
		state = aState = 0;
		cd = 0.0f;
		destination = RTSConst.invalidPosition;
	}
	protected override void Update () {
		base.Update ();
	}
	protected override void OnGUI () {
		base.OnGUI ();
	}

	protected virtual bool pointTo(Vector3 v) { //need to fix rotating *** *FIXED ***
		Vector3 rel = transform.InverseTransformPoint (v);
		if (rel.x > 1.0f || (rel.x > 0 && rel.z < 0)) {
			transform.Rotate (Vector3.up, turnRate * Time.deltaTime);
			return false;
		} else if (rel.x < -1.0f || (rel.x < 0 && rel.z < 0)) {
			transform.Rotate (Vector3.up, -turnRate * Time.deltaTime);
			return false;
		} else {
			transform.LookAt (new Vector3(v.x, transform.position.y, v.z));
			return true;
		}
	}
	protected virtual void walk() {
		anim.SetInteger ("aState", 1);
		transform.Translate (Vector3.forward * moveSpeed * Time.deltaTime);
	}
	protected virtual void goTo(Vector3 v) {
		if (pointTo (destination) && Vector3.Distance (transform.position, destination) > 0)
			walk ();
	}
	protected virtual void attackEntity(Entity e) {
		destination = e.transform.position;
		if (objectDistance(e) > range) { //need to fix range calculation to center of this and edge of Entity e
			goTo (e.getPosition());
		} else {
			anim.SetInteger ("aState", 0);
			if (pointTo (e.getPosition())) {
				if (e.hitPoints <= 0) {
					state = 0;
					target = null;
					return;
				} else if (e.hitPoints > 0) {
					if (cd <= 0) {
						cd = atkSpd;
						anim.SetTrigger ("attack");
					} else {
						cd -= Time.deltaTime;
					}
				}
			}
		}
	}

	public override void mouseClick (GameObject hitObject, Vector3 point, Player controller) {
		base.mouseClick (hitObject, point, controller);
	}
	public override void rightClick(GameObject entity, Vector3 point, Player controller) {
		if (!entity.GetComponentInParent<Entity> ()) {
			this.setDestination (point);
		} 
		else if (entity.GetComponent<Entity> () != this) {
			this.target = entity.GetComponent<Entity>();
			state = 2;
		}
	}
	public void setDestination(Vector3 d) {
		destination = d;
		state = 1;
		target = null;
	}

	protected float objectDistance(Entity ent) {
		GameObject obj = ent.gameObject;
		Vector3 surfP1 = gameObject.GetComponent<Collider>().ClosestPointOnBounds (ent.transform.position);
		Vector3 surfP2 = obj.GetComponent<Collider>().ClosestPointOnBounds (transform.position);
		surfP1.y = surfP2.y = 0;
		Debug.DrawLine (surfP1, surfP2);
		return Vector3.Distance (surfP1, surfP2);//Vector2.Distance (new Vector2 (surfP1.x, surfP1.z), new Vector2 (surfP2.x, surfP2.z));
	}
	public void dealDamage() {
		if (target is Unit)
			pull ((Unit)target);
		target.takeDamage(damage);
	}
	public void pull(Unit u) {
		u.target = this;
		u.state = 2;
	}
}