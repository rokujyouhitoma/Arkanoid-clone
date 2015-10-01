using UnityEngine;
using System.Collections;

namespace Arkanoid {
	public class Controller : MonoBehaviour {
		Movement movement;

		void Awake() {
			movement = GetComponent<Movement>();
		}

		void Update () {
			if (Input.GetKey("right")) {
				movement.Right();
			}
			if (Input.GetKey("left")) {
				movement.Left();
			}
			if (Input.GetKeyUp("right")) {
				movement.Stop();
			}
			if (Input.GetKeyUp("left")) {
				movement.Stop();
			}
		}
	}
}