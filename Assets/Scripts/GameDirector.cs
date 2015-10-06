using UnityEngine;
using System.Collections;

namespace Arkanoid {
	public class GameDirector : MonoBehaviour {
		public float highScore = 0;
		public float score = 0;
		public float maxVaus = 0;
		public float vaus = 0;

		public GameObject ballPrefab;

		Generator itemGenerator;

		void Awake() {
			itemGenerator = GetComponent<Generator>();
		}

		void Start () {
			Invoke("StartMoveBall", 5);
		}

		void Update () {
			if (!ExistsBricks()) {
				ClearRound();
			}
			if (!ExistsBalls()) {
				TerminateBall();
				if (0f < vaus) {
					CreateBall();
					SetUpVaus();
				} else {
					ResetGame();
				}
			}
		}

		void ResetGame() {
			Application.LoadLevel("GameMain");
		}

		public float GetScore() {
			return score;
		}

		public float GetVaus() {
			return vaus;
		}

		public void AddScoreByHealth(Health health) {
			AddScore (health.score);
		}

		void AddScore(float v) {
			score += v;
			if (score > highScore) {
				highScore = score;
			}
		}

		public void DamageVaus(float amount) {
			if (0f < vaus) {
				vaus = Mathf.Max(0f, vaus - amount);
				if (vaus <= 0f) {
					Debug.Log ("die..."); //TODO
				}
			}
		}

		bool ExistsBricks () {
			var objs = GameObject.FindGameObjectsWithTag("Bricks");
			 if (objs .Length > 0) {
				return true;
			}
			return false;
		}

		bool ExistsBalls () {
			var objs = GameObject.FindGameObjectsWithTag("Balls");
			if (objs .Length > 0) {
				return true;
			}
			return false;
		}

		void StartMoveBall () {
			var objs = GameObject.FindGameObjectsWithTag("Balls");
			foreach (var obj in objs) {
				var ball= obj.GetComponent<Ball>();
                ball.stop = false;
            }
        }

		void CreateBall() {
			//TODO
			var ball = (GameObject)Instantiate(ballPrefab, Vector3.zero, Quaternion.identity);
			var gameBoard = GameObject.Find ("/Canvas/Layer/GameBoard");
			ball.tag = "Balls";
			ball.transform.parent = gameBoard.transform;
			ball.transform.localPosition = new Vector3(2.5f, -86f, -1);
			ball.transform.localScale = new Vector3(62.5f, 50f, 0f);
			Invoke("StartMoveBall", 5);
        }

		void SetUpVaus() {
			var vaus = GameObject.Find("/Canvas/Layer/GameBoard/Vaus");
			vaus.transform.localPosition = new Vector3( 0, -92, -1);
			vaus.SendMessage("OnExpand", gameObject);
 		}

		void TerminateBall() {
			DamageVaus(1f);
		}

		void ClearRound() {
			Debug.Log ("Clear Round");
		}

		void OnPlayerExtend(GameObject ga) {
			DamageVaus(-1f) ;
        }

		public GameObject GetItemPrefabRandom() {
			return itemGenerator.GenerateRandom();
		}
    }
}