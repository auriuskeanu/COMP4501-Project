using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : Entity {

	protected Vector3 rallyPoint;
	protected GameObject [] prefabs;

	protected override void  Awake() {
		base.Awake ();
	}
	protected override void Start () {
		base.Start ();
		rallyPoint = transform.position + transform.forward;
	}
	protected override void Update () {
		base.Update ();
	}
	protected override void OnGUI () {
		base.OnGUI ();
	}

	public override void rightClick(GameObject hitObject, Vector3 point, Player controller) {
		rallyPoint = point;
	}

	public virtual void spawn(int i) {
		Vector3 pos = gameObject.GetComponent<Collider> ().ClosestPointOnBounds (rallyPoint) - transform.position;
		pos = transform.position + 2*(Vector3.Normalize (pos));
		GameObject unit = Instantiate (prefabs [i], new Vector3 (pos.x, prefabs [i].transform.position.y, pos.z), Quaternion.identity);
		unit.GetComponent<Unit> ().setDestination (rallyPoint);
	}
}
