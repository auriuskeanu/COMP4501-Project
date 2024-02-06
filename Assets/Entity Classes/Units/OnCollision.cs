using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollision : MonoBehaviour {
	void OnCollisionEnter (Collision collision) {
	}
	void OnCollisionStay (Collision collision) {
	}
	void OnCollisionExit (Collision collision) {
		Rigidbody tmp = this.GetComponent < Rigidbody> ();
		if (tmp)
			tmp.velocity = Vector3.zero;
	}
}
