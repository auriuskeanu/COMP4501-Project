using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public string Username;		//Identifier
	public bool human;			//is the player
	public HUD hud;
	public float cash;

	public Entity selectedObject { get; set; }

	// Use this for initialization
	void Start () {
		cash = 150f;
		hud = GetComponentInChildren<HUD> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void getCash(float f) {
		cash += f;
	}
}
