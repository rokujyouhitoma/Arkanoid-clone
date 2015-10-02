using UnityEngine;
using System.Collections;


namespace Arkanoid {
	public class Movement : MonoBehaviour {

		public float vx = 0f;
		public float vy = 0f;

		public float vi = 0f;
		public float a = 0f;

		public float LimitXMin = -10000f;
		public float LimitXMax = 10000f;

		void Start() {
		}

		void Update () {
		}

		public void Move () {
			var dt = Time.deltaTime;
			var p = transform.localPosition; 
			var x = p.x + (vx * dt);
			var y = p.y + (vy * dt);
			if (x <= LimitXMin) {
				x = LimitXMin;
			} 
			if (LimitXMax <= x) {
				x = LimitXMax;
			}
			transform.localPosition = new Vector3(x, y, p.z);
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