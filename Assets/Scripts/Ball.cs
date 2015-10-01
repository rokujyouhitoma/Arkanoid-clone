using UnityEngine;
using System.Collections;

namespace Arkanoid {
	public class Ball : MonoBehaviour {
		public float speed = 0f;

		MyCollider mycollider;

		Movement movement;
		Vector2 dir;

		void Awake() {
			mycollider = GetComponent<MyCollider>();
			movement = GetComponent<Movement>();
		}

		void Start() {
			dir = (new Vector2(1, 2)).normalized;
		}

		void Update () {
//			VausCollisionResolver();
//			BordersCollisionResolver();
			BrickCollisionResolver();
			Move ();
		}

		void VausCollisionResolver() {
			var objs = GameObject.FindGameObjectsWithTag("Vaus");
			mycollider.CollisionResolver(objs);
		}

		void BordersCollisionResolver() {
			var objs = GameObject.FindGameObjectsWithTag("Borders");
			mycollider.CollisionResolver(objs);
		}

		void BrickCollisionResolver() {
			var objs = GameObject.FindGameObjectsWithTag("Bricks");
			mycollider.CollisionResolver(objs);
		}

		void Move() {
			var vec = dir * speed; 
			movement.vx = vec.x;
			movement.vy = vec.y;
		}

		void OnHorizontal(GameObject ga) {
			ResolvePosition(ga);
			dir = new Vector2(dir.x, dir.y * -1);
			Debug.Log (dir);
//			dir = new Vector2(0, 0);
		}

		void OnVertical(GameObject ga) {
			ResolvePosition(ga);
			dir = new Vector2(dir.x * -1, dir.y);
//			dir = new Vector2(0, 0);
		}

		void ResolvePosition(GameObject ga) {
			transform.localPosition = Libs.ResolvePosition(gameObject, ga);
		}
 	}
}