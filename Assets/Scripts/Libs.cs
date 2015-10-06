using UnityEngine;
using System.Collections;

namespace Arkanoid {
	public class Libs : MonoBehaviour {

		static float scaleX = 12.5f;
		static float scaleY = 12.5f;

		public static Rect GetRectByGameObject(GameObject ga) {
			var p = ga.transform.localPosition;
			var scale = ga.transform.localScale;
			var width = scale.x / scaleX;
			var height = scale.y / scaleY;
			return new Rect(p.x - width/2, p.y - height/2, width, height);
		}

		public static bool HitCheckRect(Rect r1, Rect r2) {
			if (r1.xMin < r2.xMax && r2.xMin < r1.xMax &&
			    r1.yMin < r2.yMax && r2.yMin < r1.yMax) {
				return true;
			}
			return false;
		}

		public static bool HitCheckGameObject(GameObject ga1, GameObject ga2) {
			var ga1Rect = GetRectByGameObject(ga1);
			var ga2Rect = GetRectByGameObject(ga2);
			return Libs.HitCheckRect(ga1Rect, ga2Rect);
		}

		public static Vector2 CollisionVector2(GameObject self, GameObject target) {
			var selfScale = self.transform.localScale;
			var selfWidth = selfScale.x / scaleX;
			var selfHeight = selfScale.y / scaleY;
			var targetScale = target.transform.localScale;
			var targetWidth = targetScale.x / scaleX;
			var targetHeight = targetScale.y / scaleY;
			var selfRect = GetRectByGameObject(self);
			var targetRect = GetRectByGameObject(target);
			if (Libs.HitCheckRect(selfRect, targetRect)) {
				var dirV = selfRect.center - targetRect.center;
				var isVertical = Mathf.Abs(dirV.x) <= Mathf.Abs(dirV.y);
				if (isVertical) {
					var diffY = dirV.y / Mathf.Abs(dirV.y) * selfHeight;
					if (diffY < 0) {
						return new Vector2(0, -1);
					} else {
						return new Vector2(0, 1);
					}
				} else {
					var diffX = dirV.x / Mathf.Abs(dirV.x) * selfWidth;
					if (diffX < 0) {
						return new Vector2(-1, 0);
					} else {
						return new Vector2(1, 0);
					}
				}
			}
			return Vector2.zero;
		}


		public static bool IsCross(Vector2 a, Vector2 b, Vector3 c, Vector3 d) {
			var ta = (c.x - d.x) * (a.y - c.y) + (c.y - d.y) * (c.x - a.x);
			var tb = (c.x - d.x) * (b.y - c.y) + (c.y - d.y) * (c.x - b.x);
			var tc = (a.x - b.x) * (c.y - a.y) + (a.y - b.y) * (a.x - c.x);
			var td = (a.x - b.x) * (d.y - a.y) + (a.y - b.y) * (a.x - d.x);
			return tc * td < 0 && ta * tb < 0;
		}
	}
}