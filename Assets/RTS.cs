using UnityEngine;
using System.Collections;

namespace RTS {
	public static class RTSConst{
		public static float scrollSpeed = 25;
		public static float screenGive = 50;
		private static Vector3 InvalidPosition = new Vector3(-9999,-9999,-9999);

		public static Vector3 invalidPosition { get { return InvalidPosition; } }
	}

	//player units' consts and such
	public static class Peasant {
		public static float cost = 50, maxHitPoints = 50, damage = 15, range = 0.4f,
							turnRate = 540, moveSpeed = 2.8f, atkspd = 0.8f;
		public enum States { Idle, Walk, Build, Repair, Attack, Harvest }
	}
	public static class Footman {
		public static float cost = 75, maxHitPoints = 100, damage = 25, range = 0.5f,
							turnRate = 720, moveSpeed = 3.2f, atkspd = 1.5f;
		public enum States { Idle, Walk, Attack }
	}
	public static class Swordsman {
		public static float cost = 100, maxHitPoints = 150, damage = 35, range = 0.5f,
							turnRate = 720, moveSpeed = 3f, atkspd = 1f;
		public enum States { Idle, Walk, Attack }
	}
	public static class Archer {
		public static float cost = 100, maxHitPoints = 75, damage = 40, range = 5f,
							turnRate = 720, moveSpeed = 2.8f, atkspd = 2f;
		public enum States { Idle, Walk, Attack }
	}

	//building consts and such
	public static class Hall {
		public static float maxHitPoints = 512;
	}
	public static class Barracks {
		public static float maxHitPoints = 256;
	}


	//enemy units' consts and such
	public static class Skeleton {
		public static float maxHitPoints = 100, damage = 30, range = 0.5f,
							turnRate = 720, moveSpeed = 3f, atkspd = 1f;
		public enum States { Idle, Walk, Attack }
	}
	public static class Golem {
		public static float maxHitPoints = 1000, damage = 50, range = 0.5f,
							turnRate= 240, moveSpeed = 1f, atkspd = 2f;
		public enum States { Idle, Walk, Attack }
	}
}