using UnityEngine;
using System.Collections;

namespace Arkanoid {
	public class Vaus: MonoBehaviour {
		public bool expand;

		Movement movement;
		MyCollider mycollider;
		GameObject background;

		void Awake() {
			mycollider = GetComponent<MyCollider>();
			movement = GetComponent<Movement>();
        }

		void Start() {
			background = GameObject.Find("/Canvas/Layer/GameBoard/Background");
			expand = false;
		}

		void Update () {
			if (Input.GetKey("right")) {
				Right ();
			}
			if (Input.GetKey("left")) {
				Left ();
			}
			if (Input.GetKeyUp("right")) {
				movement.Clear();
			}
			if (Input.GetKeyUp("left")) {
				movement.Clear();
			}
			var diff = movement.Move();
			if(IsExpand()) {
				Expand();
			} else {
				Constract();
			}
			UpdateLimitOfMove();
			UpdateBalls(diff);
			ItemCollisionResolver();
		}

		bool IsExpand() {
			return expand;
		}

		void Expand() {
			var scale = transform.localScale;
			transform.localScale = new Vector3(550f, scale.y, scale.z);
		}

		void Constract() {
			var scale = transform.localScale;
			transform.localScale = new Vector3(400f, scale.y, scale.z);
		}

		void ItemCollisionResolver() {
			var objs = GameObject.FindGameObjectsWithTag("Items");
			mycollider.CollisionObjectsResolver(objs);
		}

		void UpdateBalls(Vector3 diff) {
			var objs = GameObject.FindGameObjectsWithTag("Balls");
			foreach (var obj in objs) {
				var ball= obj.GetComponent<Ball>();
				ball.MoveByDiff(diff);
			}
		}

		void Right() {
			movement.Right();
		}

		void Left() {
			movement.Left();
		}

		void UpdateLimitOfMove () {
 			var rect = Libs.GetRectByGameObject(gameObject);
			var backgroundRect = Libs.GetRectByGameObject(background);
			movement.LimitXMin = backgroundRect.xMin + rect.width / 2;
			movement.LimitXMax = backgroundRect.xMax - rect.width / 2;
		}

		void OnBall(GameObject ga) {
        }

		void OnItem(GameObject ga) {
			ga.SendMessage("Take", gameObject);
		}

		void OnExpand(GameObject ga) {
			expand = true;
			//TODO
			Invoke("InvokeConstract", 10f);
		}

		void OnConstract(GameObject ga) {
			expand = false;
		}

 		void InvokeConstract() {
			expand = false;
		}
    }
}