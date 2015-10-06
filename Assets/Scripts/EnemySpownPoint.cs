using UnityEngine;
using System.Collections;

public class EnemySpownPoint : MonoBehaviour {

	public GameObject prefab;

	void Awake() {
		InvokeRepeating("Spown", 10f, 10f);
	}

	void Spown () {
		var p = transform.localPosition;
		var gameBoard = GameObject.Find ("/Canvas/Layer/GameBoard");
		var enemy = (GameObject)Instantiate(prefab, Vector3.zero, Quaternion.identity);
		enemy.tag = "Enemies";
		enemy.transform.parent = gameBoard.transform;
		enemy.transform.localPosition = new Vector3(p.x, p.y, p.z);
		enemy.transform.localScale = new Vector3(150f, 150f, 0f);
	}
}
