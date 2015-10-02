using UnityEngine;
using System.Collections;

namespace Arkanoid {
	public class Libs : MonoBehaviour {

		static float scaleX = 12.5f;
		static float scaleY = 12.5f;

		public static bool HitCheckRect(Rect r1, Rect r2) {
			if (r1.xMin < r2.xMax && r2.xMin < r1.xMax &&
			    r1.yMin < r2.yMax && r2.yMin < r1.yMax) {
				return true;
			}
			return false;
		}

		public static Vector3 ResolvePosition(GameObject self, GameObject target) {
			var selfScale = self.transform.localScale;
			var selfWidth = selfScale.x / scaleX;
			var selfHeight = selfScale.y / scaleY;
			var targetScale = target.transform.localScale;
			var targetWidth = targetScale.x / scaleX;
			var targetHeight = targetScale.y / scaleY;
			var p = self.transform.localPosition;
			var targetP = target.transform.localPosition;
			var selfRect = GetRectByGameObject(self);
			var targetRect = GetRectByGameObject(target);
			if (Libs.HitCheckRect(selfRect, targetRect)) {
				var dirV = selfRect.center - targetRect.center;
				var isVertical = Mathf.Abs(dirV.x) <= Mathf.Abs(dirV.y);
				if (isVertical) {
					var diffY = dirV.y / Mathf.Abs(dirV.y) * selfHeight;
					return new Vector3(0, diffY, 0);
				} else {
					var diffX = dirV.x / Mathf.Abs(dirV.x) * selfWidth;
					return new Vector3(diffX, 0, 0);
				}
			}
			return p;
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


		public static Rect GetRectByGameObject(GameObject ga) {
			var p = ga.transform.localPosition;
			var scale = ga.transform.localScale;
			var width = scale.x / scaleX;
			var height = scale.y / scaleY;
			var rect = new Rect(p.x - width/2, p.y - height/2, width, height);
			return rect;
		}
	}
}