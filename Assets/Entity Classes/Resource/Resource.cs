using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Resource : Entity {

	public float moneyPer;

	protected override void  Awake() {
		base.Awake ();
	}
	protected override void Start () {
		base.Start ();
	}
	protected override void Update () {
		base.Update ();
	}
	protected override void OnGUI () {
		base.OnGUI ();
	}
}
