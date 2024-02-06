using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour {

	public GUISkin resourceSkin, commandSkin;
	private Player player;
	private const int resourceWidth = 80, resourceHeight = 30, commandWidth = 120, commandHeight = 180, selectionHeight = 25;


	void Start () {
		player = transform.root.GetComponent<Player> ();
	}

	void OnGUI () {
		if (player && player.human) {
			drawResourceBar ();
			drawCommandBar ();
		}
	}

	private void drawResourceBar() {
		GUI.skin = resourceSkin;
		GUI.BeginGroup (new Rect (0, 0, Screen.width, resourceHeight));
		GUI.Box (new Rect (0, 0, resourceWidth, resourceHeight), "");

		GUI.contentColor = Color.black;
		string money = "$" + (int)player.cash;
		GUI.Label (new Rect (5, 0, resourceWidth, resourceHeight), money);

		GUI.EndGroup ();
	}
	private void drawCommandBar() {
		GUI.skin = commandSkin;
		GUI.BeginGroup (new Rect (0, Screen.height-commandHeight, commandWidth, commandHeight));
		GUI.Box (new Rect (0, 0, commandWidth, commandHeight), "");

		string selectionName = "";
		string health = "";
		if (player.selectedObject) {
			selectionName = player.selectedObject.entityName;
			health = (int)player.selectedObject.hitPoints + "/" + (int)player.selectedObject.maxHitPoints;
		}
		else if (player.selectedObject == null)
			selectionName = "";
		GUI.Label (new Rect (0, 0, commandWidth, selectionHeight), selectionName);
		GUI.Label (new Rect (0, 12, commandWidth, selectionHeight), health);

		GUI.EndGroup ();
	}

	public bool mouseOnWorld() {
		Vector3 mousePos = Input.mousePosition;
		bool onScreen = (mousePos.x > 0 && mousePos.x < Screen.width) && (mousePos.y > 0 && mousePos.y < Screen.height);
		bool onResourceBox = (mousePos.x > 0 && mousePos.x < resourceWidth) && (mousePos.y > Screen.height - resourceHeight && mousePos.y < Screen.height);
		bool onCommandBox = (mousePos.x > 0 && mousePos.x < commandWidth) && (mousePos.y > 0 && mousePos.y < commandHeight);
		return onScreen && !onResourceBox && !onCommandBox;
	}
}
