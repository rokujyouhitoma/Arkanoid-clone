using UnityEngine;
using System.Collections;


namespace Arkanoid {
	public class Enemy : MonoBehaviour {

		Vector2 dir;
		Movement movement;
		GameObject background;


		void Awake() {
			movement = GetComponent<Movement>();
		}

		void Start () {
			background = GameObject.Find("/Canvas/Layer/GameBoard/Background");
			dir = new Vector2(1, 0);
			InvokeRepeating("ChoiceDir", 5f, 5f);
		}
		
		void Update () {
			BordersCollisionResolver();
			UpdateMovement();
			movement.Move();
		}

		void UpdateMovement() {
			movement.Clear();
			if (dir.x == 1) {
				movement.Right();
			}
			if (dir.x == -1) {
				movement.Left();
				
			} 
			if (dir.y == 1) {
				movement.Top();
				
			}
			if (dir.y == -1) {
				movement.Bottom();
			}
		}

		void ChoiceDir() {
			var rand = Random.Range(0, 4);
			switch (rand) {
			case 0:
				dir = new Vector2(1, 0); //Right
				break;
			case 1:
				dir = new Vector2(-1, 0); //Left
				break;
			case 2:
				dir = new Vector2(0, 1); //Top
				break;
			case 3:
				dir = new Vector2(0, -1); //Bottom
				break;
			}
		}

		void BordersCollisionResolver() {
			var p = transform.localPosition;
			var rect = Libs.GetRectByGameObject(background);
			var diffX = 0f;
			var diffY = 0f;
			//right-border
			if (rect.xMax <= p.x) {
				diffX = -(p.x - rect.xMax);
				dir = new Vector2(dir.x * -1, dir.y);
			}
			//left-border
			if (p.x <= rect.xMin) {
				diffX = rect.xMin - p.x;
				dir = new Vector2(dir.x * -1, dir.y);
			}
			//top-border
			if (rect.yMax <= p.y) {
				diffY = -(p.y - rect.yMax);
				dir = new Vector2(dir.x, dir.y * -1); 
			}
			//bottom-border
			//TODO: xxx
			if (p.y <= rect.yMin) {
				diffY = rect.yMin - p.y;
				dir = new Vector2(dir.x, dir.y * -1);
			}
			transform.localPosition = new Vector3(p.x + diffX, p.y + diffY, p.z);
		}

		void OnBall(GameObject ga) {
			Die (gameObject);
		}

		void Die(GameObject ga) {
			Destroy (ga);
		}
	}
}