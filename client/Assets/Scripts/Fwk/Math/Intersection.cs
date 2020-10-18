using UnityEngine;

namespace Fwk.Math
{
	public static class Intersection
	{
		// ----------------------------------------------------------------
		// Method
		// ----------------------------------------------------------------
		public static bool LineSegments(out Vector2 intersection, Vector2 p0, Vector2 p1, Vector2 q0, Vector3 q1)
		{
			intersection = Vector2.zero;

			var d = (p1.x - p0.x) * (q1.y - q0.y) - (p1.y - p0.y) * (q1.x - q0.x);
			if (d == 0.0f)
			{
				return false;
			}

			var u = ((q0.x - p0.x) * (q1.y - q0.y) - (q0.y - p0.y) * (q1.x - q0.x)) / d;
			var v = ((q0.x - p0.x) * (p1.y - p0.y) - (q0.y - p0.y) * (p1.x - p0.x)) / d;
			if (u < 0.0f || u > 1.0f || v < 0.0f || v > 1.0f)
			{
				return false;
			}

			intersection.x = p0.x + u * (p1.x - p0.x);
			intersection.y = p0.y + u * (p1.y - p0.y);
			return true;
		}

		public static float SqrDistancePointSegment(Vector3 p, Vector3 a, Vector3 b)
		{
			var ab = b - a;
			var ap = p - a;
			var e = Vector3.Dot(ap, ab);
			if (e <= 0.0f)
			{
				return Vector3.Dot(ap, ap);
			}
			var f = Vector3.Dot(ab, ab);
			if (e >= f)
			{
				var bp = p - b;
				return Vector3.Dot(bp, bp);
			}
			return Vector3.Dot(ap, ap) - e * e / f;
		}
	}
} // Fwk.Math
