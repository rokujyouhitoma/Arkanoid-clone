using UnityEngine;
using System.Collections;

namespace Arkanoid {
	public class Ball : MonoBehaviour {
		public float speed = 0f;

		Movement movement;
		Vector2 dir;

		void Awake() {
			movement = GetComponent<Movement>();
		}

		void Start() {
			dir = (new Vector2(1, 2)).normalized;
		}

		void Update () {
			var vec = dir * speed; 
			movement.vx = vec.x;
			movement.vy = vec.y;
		}
	}
}