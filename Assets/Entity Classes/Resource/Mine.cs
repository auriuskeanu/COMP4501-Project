using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Resource {

	protected override void  Awake() {
		base.Awake ();
	}
	protected override void Start () {
		base.Start ();

		moneyPer = 25f;
		entityName = "Mine";
	}
	protected override void Update () {
		base.Update ();
	}
	protected override void OnGUI () {
		base.OnGUI ();
	}
}
