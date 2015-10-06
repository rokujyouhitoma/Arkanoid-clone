using UnityEngine;
using System.Collections;

namespace Arkanoid {
	public class MyCollider : MonoBehaviour {
		public void OnEnter(GameObject ga, Vector2 dir) {
			var id = ga.GetComponent<Id>();
			if (ga == gameObject) {
				Debug.LogError ("bugs...");
				return;
			}
			switch (id.type) {
			case Id.SpeedDown:
			case Id.Catch:
			case Id.Disruption:
			case Id.Expand:
			case Id.Laser:
			case Id.Break:
			case Id.PlayerExtend:
				gameObject.SendMessage("OnItem", ga);
				break;
			case Id.Enemy:
				gameObject.SendMessage("OnEnemy", ga);
				break;
			}
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