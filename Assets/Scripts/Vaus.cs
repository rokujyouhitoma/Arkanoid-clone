using UnityEngine;
using System.Collections;

namespace Arkanoid {
	public class Vaus: MonoBehaviour {
		Movement movement;

		void Awake() {
			movement = GetComponent<Movement>();
		}

		void Start() {
			SetupLimitOfMove();
		}

		void Update () {
			if (Input.GetKey("right")) {
				Right ();
			}
			if (Input.GetKey("left")) {
				Left ();
			}
			if (Input.GetKeyUp("right")) {
				movement.Stop();
			}
			if (Input.GetKeyUp("left")) {
				movement.Stop();
			}
			movement.Move();
		}

		void Right() {
			movement.Right();
		}

		void Left() {
			movement.Left();
		}

		void SetupLimitOfMove () {
 			var rect = Libs.GetRectByGameObject(gameObject);
			var background = GameObject.Find("/Canvas/Layer/GameBoard/Background");
			var backgroundRect = Libs.GetRectByGameObject(background);
			movement.LimitXMin = backgroundRect.xMin + rect.width / 2;
			movement.LimitXMax = backgroundRect.xMax - rect.width / 2;
		}

		void OnBall(GameObject ga) {
        }
    }
}