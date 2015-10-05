﻿using UnityEngine;
using System.Collections;

namespace Arkanoid {
	public class Ball : MonoBehaviour {
		public float speed = 0f;
		public float attack = 1f;
		public Vector3 p1;
		public bool stop = false;

//		MyCollider mycollider;

		Movement movement;
		Vector2 dir;

		void Awake() {
//			mycollider = GetComponent<MyCollider>();
			movement = GetComponent<Movement>();
			p1 = Vector3.zero;
		}

		void Start() {
			dir = (new Vector2(1, 2)).normalized;
		}

		void Update () {
			movement.Move ();
			VausCollisionResolver();
			BordersCollisionResolver();
			BrickCollisionResolver();
			p1 = transform.localPosition;
			UpdateVec ();
			if (IsStop ()) {
				Stop();
			}
		}
		 
		public bool IsStop() {
			return stop;
		}

		void Stop() {
			movement.Stop();
		}

		void VausCollisionResolver() {
			var vaus = GameObject.Find("/Canvas/Layer/GameBoard/Vaus");
			CollisionObjectResolver(vaus);
		}

		void BordersCollisionResolver() {
			var p = transform.localPosition;
			var background = GameObject.Find("/Canvas/Layer/GameBoard/Background");
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

		void BrickCollisionResolver() {
			var objs = GameObject.FindGameObjectsWithTag("Bricks");
			CollisionObjectsResolver(objs);
		}

		public void CollisionObjectsResolver(GameObject[] objects) {
			foreach (GameObject obj in objects) {
				CollisionObjectResolver(obj);
			}
		}

		public void CollisionObjectResolver(GameObject obj) {
			var diffX = 0f;
			var diffY = 0f;
			var rect = Libs.GetRectByGameObject(obj);
			var p = transform.localPosition;
			var diff = p - p1;
			if (diff.x == 0f && diff.y == 0f) {
				Debug.Log ("???");
				return;
			}
			if (0f <= diff.x && 0f <= diff.y) {
				//bottom
				if (Libs.IsCross(new Vector2(p.x, p.y),
				                 new Vector2(p1.x, p1.y),
				                 new Vector2(rect.xMin, rect.yMin),
				                 new Vector2(rect.xMax, rect.yMin))) {
					diffY = -(p.y - rect.yMin);
					dir = new Vector2(dir.x, dir.y * -1);
					obj.SendMessage("OnBall", gameObject);
				}
				//left
				if (Libs.IsCross(new Vector2(p.x, p.y),
				                 new Vector2(p1.x, p1.y),
				                 new Vector2(rect.xMin, rect.yMin),
					                 new Vector2(rect.xMin, rect.yMax))) {
					diffX = -(p.x - rect.xMin);
					dir = new Vector2(dir.x * -1, dir.y);
					obj.SendMessage("OnBall", gameObject);
                }
			}
			if (0f <= diff.x && diff.y <= 0f) {
				//top
				if (Libs.IsCross(new Vector2(p.x, p.y),
				                 new Vector2(p1.x, p1.y),
				                 new Vector2(rect.xMin, rect.yMax),
				                 new Vector2(rect.xMax, rect.yMax))) {
					diffY = rect.yMax - p.y;
					dir = new Vector2(dir.x, dir.y * -1);
					obj.SendMessage("OnBall", gameObject);
                }
				//left
				if (Libs.IsCross(new Vector2(p.x, p.y),
				                 new Vector2(p1.x, p1.y),
				                 new Vector2(rect.xMin, rect.yMin),
				                 new Vector2(rect.xMin, rect.yMax))) {
					diffX = -(p.x - rect.xMin);
					dir = new Vector2(dir.x * -1, dir.y);
					obj.SendMessage("OnBall", gameObject);
                }
			}
			if (diff.x <= 0f && 0f <= diff.y) {
				//bottom
				if (Libs.IsCross(new Vector2(p.x, p.y),
				                 new Vector2(p1.x, p1.y),
				                 new Vector2(rect.xMin, rect.yMin),
				                 new Vector2(rect.xMax, rect.yMin))) {
					diffY = -(p.y - rect.yMin);
					dir = new Vector2(dir.x, dir.y * -1);
					obj.SendMessage("OnBall", gameObject);
                }
				//right
				if (Libs.IsCross(new Vector2(p.x, p.y),
				                 new Vector2(p1.x, p1.y),
				                 new Vector2(rect.xMax, rect.yMin),
				                 new Vector2(rect.xMax, rect.yMax))) {
					diffX = rect.xMax - p.x;
					dir = new Vector2(dir.x * -1, dir.y);
					obj.SendMessage("OnBall", gameObject);
                }
			}
			if (diff.x <= 0f && diff.y <= 0f) {
				//top
				if (Libs.IsCross(new Vector2(p.x, p.y),
				                 new Vector2(p1.x, p1.y),
				                 new Vector2(rect.xMin, rect.yMax),
				                 new Vector2(rect.xMax, rect.yMax))) {
					diffY = rect.yMax - p.y;
					dir = new Vector2(dir.x, dir.y * -1);
					obj.SendMessage("OnBall", gameObject);
                }
				//right
				if (Libs.IsCross(new Vector2(p.x, p.y),
				                 new Vector2(p1.x, p1.y),
				                 new Vector2(rect.xMax, rect.yMin),
				                 new Vector2(rect.xMax, rect.yMax))) {
					diffX = rect.xMax - p.x;
					dir = new Vector2(dir.x * -1, dir.y);
					obj.SendMessage("OnBall", gameObject);
                }
			}
			transform.localPosition = new Vector3(p.x + diffX, p.y + diffY, p.z);
		}

		void UpdateVec() {
			var vec = dir * speed; 
			movement.vx = vec.x;
			movement.vy = vec.y;
		}

 	}
}