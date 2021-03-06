﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Arkanoid {
	public class ScoreText : MonoBehaviour {

		Text textUI;
		GameDirector gameDirector;
	
		void Start () {
			var gd = GameObject.Find("/GameDirector");
			gameDirector = gd.GetComponent<GameDirector>();
			textUI = GetComponent<Text>();
		}

		void Update () {
			textUI.text = gameDirector.GetScore().ToString() ;
		}
	}
}