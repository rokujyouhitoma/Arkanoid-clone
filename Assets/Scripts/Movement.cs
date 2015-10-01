using UnityEngine;
using System.Collections;


namespace Arkanoid {
	public class Movement : MonoBehaviour {

		public float vx = 0f;
		public float vy = 0f;

		public float vi = 0f;
		public float a = 0f;

		void Start() {
		}

		void Update () {
			Move();
		}

		void Move () {
			var dt = Time.deltaTime;
			var p = transform.position;
			transform.position = new Vector3(p.x + (vx * dt), p.y + (vy * dt), p.z);
		}

		public void Right() {
			var dt = Time.deltaTime;
			var v =  vi + a * dt;
			vx = v;
		}

		public void Left() {
			var dt = Time.deltaTime;
			var v =  vi + a * dt;
			vx = -v;
		}

		public void Top() {
			var dt = Time.deltaTime;
			var v =  vi + a * dt;
			vy = v;
		}

		public void Bottom() {
			var dt = Time.deltaTime;
			var v =  vi + a * dt;
			vy = -v;
		}

		public void Stop() {
			vx = 0f;
			vy = 0f;
		}

	}
}