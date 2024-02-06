using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour { //mutable game world objects like buildings and units
	
	public string entityName;
	public float cost, sell, hitPoints, maxHitPoints;

	public Player player;
	protected bool selected = false;


	protected virtual void Awake() {

	}
	protected virtual void Start () {
		player = transform.root.GetComponentInParent<Player> ();
	}
	protected virtual void Update () {
		
	}
	protected virtual void OnGUI () {
	
	}


	public Vector3 getPosition() {	return transform.position;	}
	public void setSelect(bool s) { selected = s; }

	public virtual void mouseClick(GameObject hitObject, Vector3 point, Player controller) {
		if (selected && hitObject && hitObject.name != "Floor") {
			Entity worldObject = hitObject.transform.root.GetComponent<Entity> ();
			worldObject = (worldObject == null ? hitObject.transform.root.GetComponentInChildren<Entity> () : worldObject);
			if (worldObject)
				ChangeSelection (worldObject, controller);
		}
	}

	public virtual void rightClick(GameObject hitObject, Vector3 point, Player controller) {
	}
	public virtual void keyPress(Player controller) {
	}

	private void ChangeSelection(Entity worldObject, Player controller) {
		setSelect (false);
		if (controller.selectedObject)
			controller.selectedObject.setSelect (false);
		controller.selectedObject = worldObject;
		worldObject.setSelect (true);
	}

	public void takeDamage(float dmg) {
		hitPoints -= dmg;
		if (hitPoints <= 0) {
			Destroy (gameObject);
		}
	}
	public void healDamage(float dmg) {
		hitPoints += dmg;
		if (hitPoints > maxHitPoints)
			hitPoints = maxHitPoints;
	}
}