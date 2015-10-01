using UnityEngine;
using System.Collections;


namespace Arkanoid {
	public class MyCollider : MonoBehaviour {
		public void OnEnter(GameObject ga, Vector2 dir) {
			if (ga == gameObject) {
				Debug.LogError ("bugs...");
				return;
			}
			if (0 < dir.y) {
				gameObject.SendMessage("OnHorizontal", ga);
			} else if (dir.y < 0) {
				gameObject.SendMessage("OnHorizontal", ga);
			}
//			if (0 < dir.x) {
//				gameObject.SendMessage("OnVertical", ga);
//			} else if (dir.x < 0) {
//				gameObject.SendMessage("OnVertical", ga);
//			}
		}

		public void CollisionResolver(GameObject[] objects) {
			foreach (GameObject obj in objects) {
				var vec = Libs.CollisionVector2(gameObject, obj);
				if (vec != Vector2.zero) { 
					OnEnter(obj, vec);
				}
			}
		}
	}
}