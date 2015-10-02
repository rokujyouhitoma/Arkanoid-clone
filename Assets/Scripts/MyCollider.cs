using UnityEngine;
using System.Collections;


namespace Arkanoid {
	public class MyCollider : MonoBehaviour {
		public void OnEnter(GameObject ga, Vector2 dir) {
			if (ga == gameObject) {
				Debug.LogError ("bugs...");
				return;
			}
//			if (0 < dir.y) {
//				gameObject.SendMessage("OnUpper", ga);
//			} else if (dir.y < 0) {
//				gameObject.SendMessage("OnLower", ga);
//			} else  if (0 < dir.x) {
//				gameObject.SendMessage("OnRight", ga);
//			} else if (dir.x < 0) {
//				gameObject.SendMessage("OnLeft", ga);
//			}
		}

		public void CollisionObjectsResolver(GameObject[] objects) {
			foreach (GameObject obj in objects) {
				CollisionObjectResolver(obj);
			}
		}

		public void CollisionObjectResolver(GameObject obj) {
			var vec = Libs.CollisionVector2(gameObject, obj);
			if (vec != Vector2.zero) { 
				OnEnter(obj, vec);
			}
		}
	}
}