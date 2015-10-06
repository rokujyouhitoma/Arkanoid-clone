using UnityEngine;
using System.Collections;

namespace Arkanoid {
	public class Item : MonoBehaviour {
		Movement movement;
		Id id;

		GameObject background; 
		GameDirector gameDirector;

		void Awake() {
			var gd = GameObject.Find("/GameDirector");
			gameDirector = gd.GetComponent<GameDirector>();
			id = GetComponent<Id>();
			movement = GetComponent<Movement>();
		}
 
		void Start() {
			background = GameObject.Find("/Canvas/Layer/GameBoard/Background");
		}

		void Update () {
			movement.Bottom();
			movement.Move();
			LowerBorderCollisionResolver();
		}

		void LowerBorderCollisionResolver() {
			var p = transform.localPosition;
			var rect = Libs.GetRectByGameObject(background);
			if (p.y <= rect.yMin) {
				Die();
 			}
		}

		public void Take(GameObject ga) {
			switch (id.type) {
			case Id.SpeedDown:
				SpeedDown(ga);
				break;
			case Id.Catch:
				Catch (ga);
				break;
			case Id.Disruption:
				Disruption(ga);
				break;
			case Id.Expand:
				Expand(ga);
				break;
			case Id.Laser:
				Laser(ga);
				break;
			case Id.Break:
				Break (ga);
				break;
			case Id.PlayerExtend:
				PlayerExtend(ga);
				break;
			}
			Die ();
		}

		void Die() {
			Destroy(gameObject);
		}

		void SpeedDown(GameObject ga) {
			var objs = GameObject.FindGameObjectsWithTag("Balls");
			foreach (var obj in objs) {
				var ball = obj.GetComponent<Ball>();
				ball.SendMessage("OnSpeedDown", gameObject);
			}
		}

		void Catch(GameObject ga) {
			//TODO
		}

		void Disruption(GameObject ga) {
			//TODO
		}

		void Expand(GameObject ga) {
			var vaus = GameObject.Find("/Canvas/Layer/GameBoard/Vaus");
			vaus.SendMessage("OnExpand", gameObject);
		}

		void Laser(GameObject ga) {
			//TODO
		}

		void Break(GameObject ga) {
			//TODO
		}

		void PlayerExtend(GameObject ga) {
			gameDirector.SendMessage("OnPlayerExtend", gameObject);
		}
	}
}