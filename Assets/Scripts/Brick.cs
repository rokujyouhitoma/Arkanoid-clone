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
			//TODO
			var rand = Random.Range(0, 3);
 			if (1 < rand) {
				CreateItem();
			}
			Destroy(gameObject); 
		}

		void CreateItem() {
			var p = transform.localPosition;
			var s = transform.localScale;
			var gameBoard = GameObject.Find ("/Canvas/Layer/GameBoard");
			var prefab = gameDirector.GetItemPrefabRandom();
			var item = (GameObject)Instantiate(prefab, Vector3.zero, Quaternion.identity);
			item.tag = "Items";
			item.transform.parent = gameBoard.transform;
			item.transform.localPosition = new Vector3(p.x, p.y, p.z);
			item.transform.localScale = new Vector3(s.x, s.y, s.z);
		}
	}
}