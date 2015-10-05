using UnityEngine;
using System.Collections;

namespace Arkanoid {
	public class GameDirector : MonoBehaviour {
		public float highScore = 0;
		public float score = 0;
		public float numberOfVaus = 0;

		public void AddScore(float v) {
			score += v;
			if (score > highScore) {
				highScore = score;
			}
		}
	}

}