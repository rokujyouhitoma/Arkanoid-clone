using UnityEngine;
using System.Collections;

namespace Arkanoid {
	public class Brick : MonoBehaviour {
		Health health;
		GameDirector gameDirector;

		void Awake() {
			var gd = GameObject.Find("/GameDirector");
			gameDirector = gd.GetComponent<GameDirector>();
			health = GetComponent<Health>();
		}
		 
		void OnBall(GameObject ga) {
			health.Damage(1f);
		}

		void OnDie(GameObject ga) {
			var brickHealth = ga.GetComponent<Health>();
			gameDirector.AddScoreByHealth(brickHealth);
			Destroy(gameObject); 
			CreateItem();
		}

		void CreateItem() {

		}
	}
}