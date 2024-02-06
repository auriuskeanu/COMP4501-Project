using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RTS;

public class UserInput : MonoBehaviour {
	private Player player;
	// Use this for initialization
	void Start () {
		player = transform.root.GetComponent<Player> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (player.human) {
			moveCamera ();
			mouseActivity ();
			keyActivity ();
		}
	}

	private void moveCamera () {
		Vector2 mousepos;
		mousepos.x = Input.mousePosition.x;
		mousepos.y = Input.mousePosition.y;
		Vector3 camDirection = new Vector3 (0, 0, 0);

		//horizontal camera movement;
		if (mousepos.x > -(RTSConst.screenGive) && mousepos.x < (Screen.width+RTSConst.screenGive)) {
			camDirection.x = 0;
		} else {
			camDirection.x = RTSConst.scrollSpeed * (mousepos.x > 0 ? 1 : -1);
		}
		if (mousepos.y > -(RTSConst.screenGive) && mousepos.y < (Screen.height+RTSConst.screenGive)) {
			camDirection.y = 0;
		} else {
			camDirection.z = RTSConst.scrollSpeed * (mousepos.y > 0 ? 1 : -1);
		}

		camDirection = Quaternion.AngleAxis (-45, Vector3.up) * camDirection;
		Vector3 origin = Camera.main.transform.position;
		Vector3 destination = origin + camDirection;
		if (destination != origin) {
			Camera.main.transform.position = Vector3.MoveTowards (origin, destination, Time.deltaTime * RTSConst.scrollSpeed);
		}
	}

	private void mouseActivity () {
		if (Input.GetMouseButtonDown (0)) leftMouseClick ();
		else if (Input.GetMouseButtonDown (1)) rightMouseClick();
	}
	private void keyActivity () {
		if (Input.anyKeyDown) {
			if (player.selectedObject)
				player.selectedObject.keyPress (player);
		}
	}

	private void leftMouseClick () {
		if (player.hud.mouseOnWorld ()) {
			GameObject entity = raycastHitObject();
			Vector3 point = raycastHitPoint ();
			if (entity && point != RTS.RTSConst.invalidPosition) {
				if (player.selectedObject) {
					player.selectedObject.mouseClick (entity, point, player);

					if (!entity.GetComponentInParent<Entity>()) { //deselects object if left click on not entity
						player.selectedObject.setSelect (false);
						player.selectedObject = null;
					}
				} else if (entity.name != "Floor") {
					Entity worldObject = entity.transform.root.GetComponent<Entity> ();
					worldObject = (worldObject == null ? entity.transform.root.GetComponentInChildren<Entity> () : worldObject);
					if (worldObject) {
						player.selectedObject = worldObject;
						worldObject.setSelect (true);
					}
				}
			}
		}
	}
	private void rightMouseClick() {
		if (player.hud.mouseOnWorld()) {
			GameObject entity = raycastHitObject ();
			Vector3 point = raycastHitPoint ();
			if (player.selectedObject && player.selectedObject.player == player) {
				player.selectedObject.GetComponent<Entity> ().rightClick (entity, point, player);
/*				if (!entity.GetComponentInParent<Entity> ()) {
					player.selectedObject.GetComponent<Unit> ().setDestination (point);
				} 
				if (entity.GetComponent<Entity> ()) {
					
				} else {
					Debug.Log ("Clicked on an entity");
				}*/
			}
		}
	}

	private GameObject raycastHitObject() {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit))
			return hit.collider.gameObject;
		return null;
	}

	private Vector3 raycastHitPoint() {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			return hit.point;
		}
		return RTS.RTSConst.invalidPosition;
	}
}
