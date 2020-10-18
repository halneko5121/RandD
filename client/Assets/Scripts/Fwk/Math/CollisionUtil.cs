using UnityEngine;

namespace Fwk.Math
{
	public static class CollisionUtil
	{
		// ----------------------------------------------------------------
		// Method
		// ----------------------------------------------------------------
		/// <summary>
		/// 点の多角形に対する内外判定
		/// http://www.sousakuba.com/Programming/gs_hittest_point_triangle.html
		/// </summary>
		/// <param name="p"></param>
		/// <param name="vertices"></param>
		/// <param name="indices"></param>
		public static bool IsInAreaXZ(Vector3 p, Vector3[] vertices, int[] indices)
		{
			int j = 0;
			for (var i = 0; i < indices.Length / 3; i++)
			{
				Vector3 A = vertices[indices[j++]];
				Vector3 B = vertices[indices[j++]];
				Vector3 C = vertices[indices[j++]];

				Vector3 AB = B - A;
				Vector3 BP = p - B;
				Vector3 BC = C - B;
				Vector3 CP = p - C;
				Vector3 CA = A - C;
				Vector3 AP = p - A;

				float c1 = AB.x * BP.z - AB.z * BP.x;
				float c2 = BC.x * CP.z - BC.z * CP.x;
				float c3 = CA.x * AP.z - CA.z * AP.x;

				//if( ( c1 > 0 && c2 > 0 && c3 > 0 ) || ( c1 < 0 && c2 < 0 && c3 < 0 ) )
				//if( c1 > 0 && c2 > 0 && c3 > 0 )
				if (c1 < 0 && c2 < 0 && c3 < 0) // 表ポリゴンだけでいい
				{
					// 三角形の内側に点がある
					return true;
				}
			}

			return false;
		}

		public static bool IsInAreaOrNear(Vector3 position, float radius, Vector3[] vertices, int[] indices)
		{
			if (IsInAreaXZ(position, vertices, indices))
			{
				return true;
			}

			// 線分と点の距離
			var p2 = new Vector2(position.x, position.z);
			for (int i = 0; i < vertices.Length; ++i)
			{
				var j = i + 1;
				if (j >= vertices.Length)
				{
					j = 0;
				}

				var vi = new Vector2(vertices[i].x, vertices[i].z);
				var vj = new Vector2(vertices[j].x, vertices[j].z);
				var projection = Vector2.zero;
				var difference = vj - vi;
				var direction = difference.normalized;
				var dot = Vector2.Dot(p2 - vi, direction);
				if (dot < 0.0f)
				{
					projection = vi;
				}
				else if (dot > difference.magnitude)
				{
					projection = vj;
				}
				else
				{
					projection = vi + direction * dot;
				}

				var distance = (projection - p2).magnitude;
				if (radius > distance)
				{
					return true;
				}
			}

			return false;
		}

		public static bool IsInMeshXZ(Vector3[] vertices, int[] indices, Vector3 position)
		{
			for (int i = 0; i < indices.Length; i += 3)
			{
				var i0 = indices[i + 0];
				var i1 = indices[i + 1];
				var i2 = indices[i + 2];
				if (IsInTriangleXZ(ref vertices[i0], ref vertices[i1], ref vertices[i2], ref position))
				{
					return true;
				}
			}
			return false;
		}

		public static bool IsInTriangleXZ(ref Vector3 v0, ref Vector3 v1, ref Vector3 v2, ref Vector3 p)
		{
			p.y = 0.0f;
			var cross0 = Vector3.Cross(p - v0, v1 - v0);
			var cross1 = Vector3.Cross(p - v1, v2 - v1);
			if (cross0.y * cross1.y <= 0.0f)
			{
				return false;
			}
			var cross2 = Vector3.Cross(p - v2, v0 - v2);
			if (cross0.y * cross2.y <= 0.0f)
			{
				return false;
			}
			return true;
		}

		public static bool IsInTriangle(ref Vector3 va, ref Vector3 vb, ref Vector3 vc, ref Vector3 vp)
		{
			var AB = vb - va;
			var AP = vp - va;
			var BC = vc - vb;
			var BP = vp - vb;
			var CA = va - vc;
			var CP = vp - vc;

			var c1 = AB.x * BP.z - AB.z * BP.x;
			var c2 = BC.x * CP.z - BC.z * CP.x;
			var c3 = CA.x * AP.z - CA.z * AP.x;

			if (false
				|| (c1 > 0 && c2 > 0 && c3 > 0)
				|| (c1 < 0 && c2 < 0 && c3 < 0)
				)
			{
				return true;
			}

			return false;
		}

		public static bool LineIentersected(Vector2 a, Vector2 b, Vector2 c, Vector2 d)
		{
			var ta = (c.x - d.x) * (a.y - c.y) + (c.y - d.y) * (c.x - a.x);
			var tb = (c.x - d.x) * (b.y - c.y) + (c.y - d.y) * (c.x - b.x);
			var tc = (a.x - b.x) * (c.y - a.y) + (a.y - b.y) * (a.x - c.x);
			var td = (a.x - b.x) * (d.y - a.y) + (a.y - b.y) * (a.x - d.x);

			//return tc * td < 0 && ta * tb < 0;
			return tc * td <= 0 && ta * tb <= 0; // 端点を含む場合
		}

		public static bool LineSegmentsIntersected(
			out Vector2 intersection,
			Vector2 a,
			Vector2 b,
			Vector2 c,
			Vector2 d)
		{
			intersection = Vector2.zero;

			var dd = (b.x - a.x) * (d.y - c.y) - (b.y - a.y) * (d.x - c.x);

			if (dd == 0.0f)
			{
				return false;
			}

			var u = ((c.x - a.x) * (d.y - c.y) - (c.y - a.y) * (d.x - c.x)) / dd;
			var v = ((c.x - a.x) * (b.y - a.y) - (c.y - a.y) * (b.x - a.x)) / dd;

			if (u < 0.0f || u > 1.0f || v < 0.0f || v > 1.0f)
			{
				return false;
			}

			intersection.x = a.x + u * (b.x - a.x);
			intersection.y = a.y + u * (b.y - a.y);

			return true;
		}

		public static float SqrDistancePointSegmentXZ(ref Vector3 p, ref Vector3 a, ref Vector3 b)
		{
			var ab = b - a;
			var ap = p - a;

			var e = DotXZ(ref ap, ref ab);
			if (e <= 0.0f)
			{
				return DotXZ(ref ap, ref ap);
			}

			var f = DotXZ(ref ab, ref ab);
			if (e >= f)
			{
				var bp = p - b;
				return DotXZ(ref bp, ref bp);
			}

			return DotXZ(ref ap, ref ap) - e * e / f;
		}

		private static float DotXZ(ref Vector3 lhs, ref Vector3 rhs)
		{
			return lhs.x * rhs.x + lhs.z * rhs.z;
		}
	}
} // Fwk.Math
