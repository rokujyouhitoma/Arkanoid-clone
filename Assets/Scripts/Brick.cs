using UnityEngine;
using System.Collections;

namespace Arkanoid {
	public class Brick : MonoBehaviour {
		Health health;

		void Awake() {
			health = GetComponent<Health>();
		}

		void OnBall(GameObject ga) {
			health.Damage(1f);
		}

		void OnDie(GameObject ga) {
			Destroy(gameObject);
		}
	}
}