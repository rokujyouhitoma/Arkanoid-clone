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
			VausCollisionResolver();
			BordersCollisionResolver();
//			BrickCollisionResolver();
			Move ();
		}

		void VausCollisionResolver() {
			var p = transform.localPosition;
			var vaus = GameObject.Find("/Canvas/Layer/GameBoard/Vaus");
		}

		void BordersCollisionResolver() {
			var p = transform.localPosition;
			var background = GameObject.Find("/Canvas/Layer/GameBoard/Background");
			var backgroundRect = Libs.GetRectByGameObject(background);
			var diffX = 0f;
			var diffY = 0f;
			//right
			if (backgroundRect.xMax <= p.x) {
				diffX = -(p.x - backgroundRect.xMax);
				dir = new Vector2(dir.x * -1, dir.y);
			}
			//left
			if (p.x <= backgroundRect.xMin) {
				diffX = backgroundRect.xMin - p.x;
				dir = new Vector2(dir.x * -1, dir.y);
			}
			//top
			if (backgroundRect.yMax <= p.y) {
				diffY = -(p.y - backgroundRect.yMax);
				dir = new Vector2(dir.x, dir.y * -1);
			}
			//bottom
			if (p.y <= backgroundRect.yMin) {
				diffY = backgroundRect.yMin - p.y;
//				Destroy(gameObject); //TODO
				dir = new Vector2(dir.x, dir.y * -1);
			}
			transform.localPosition = new Vector3(p.x + diffX, p.y + diffY, p.z);
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

		void OnUpper(GameObject ga) {
			OnHorizontal(ga);
		}

		void OnLower(GameObject ga) {
			OnHorizontal(ga);
		}

		void OnHorizontal(GameObject ga) {
			Debug.Log("Hor");
//			ResolvePosition(ga);
			dir = new Vector2(dir.x, dir.y * -1);
//			dir = new Vector2(0, 0);
		}

		void OnVertical(GameObject ga) {
			Debug.Log("VER");
			ResolvePosition(ga);
			dir = new Vector2(dir.x * -1, dir.y);
//			dir = new Vector2(0, 0);
		}

		void ResolvePosition(GameObject ga) {
			var p = transform.localPosition;
			var diff = Libs.ResolvePosition(gameObject, ga);
			transform.localPosition = new Vector3(p.x + diff.x, p.y + diff.y, p.z * diff.z);
		}
 	}
}