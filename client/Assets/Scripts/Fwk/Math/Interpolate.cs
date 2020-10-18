using UnityEngine;

namespace Fwk.Math
{
	public static class Interpolate
	{
		// ----------------------------------------------------------------
		// Method
		// ----------------------------------------------------------------
		public static float SpringLerp(float strength, float deltaTime)
		{
			if (deltaTime > 1.0f)
			{
				deltaTime = 1.0f;
			}
			var ms = Mathf.RoundToInt(deltaTime * 1000.0f);
			deltaTime = 0.001f * strength;
			var cumulative = 0.0f;
			for (int i = 0; i < ms; ++i)
			{
				cumulative = Mathf.Lerp(cumulative, 1.0f, deltaTime);
			}
			return cumulative;
		}

		public static float SpringLerp(float from, float to, float strength, float deltaTime)
		{
			if (deltaTime > 1.0f)
			{
				deltaTime = 1.0f;
			}
			var ms = Mathf.RoundToInt(deltaTime * 1000.0f);
			deltaTime = 0.001f * strength;
			for (int i = 0; i < ms; ++i)
			{
				from = Mathf.Lerp(from, to, deltaTime);
			}
			return from;
		}

		public static Vector2 SpringLerp(Vector2 from, Vector2 to, float strength, float deltaTime)
		{
			return Vector2.Lerp(from, to, SpringLerp(strength, deltaTime));
		}

		public static Vector3 SpringLerp(Vector3 from, Vector3 to, float strength, float deltaTime)
		{
			return Vector3.Lerp(from, to, SpringLerp(strength, deltaTime));
		}

		public static Quaternion SpringLerp(Quaternion from, Quaternion to, float strength, float deltaTime)
		{
			return Quaternion.Slerp(from, to, SpringLerp(strength, deltaTime));
		}

		public static float Lerp(float a, float b, float t)
		{
			return Mathf.Lerp(a, b, t);
		}

		public static Vector2 Lerp(Vector2 a, Vector2 b, Vector2 t)
		{
			return new Vector2(Mathf.Lerp(a.x, b.x, t.x), Mathf.Lerp(a.y, b.y, t.y));
		}

		public static Vector3 Lerp(Vector3 a, Vector3 b, Vector3 t)
		{
			return new Vector3(Mathf.Lerp(a.x, b.x, t.x), Mathf.Lerp(a.y, b.y, t.y), Mathf.Lerp(a.z, b.z, t.z));
		}

		public static Quaternion Lerp(Quaternion from, Vector3 toEular, float t)
		{
			var endAngleX = toEular.x * t;
			var endAngleY = toEular.y * t;
			var endAngleZ = toEular.z * t;

			var endXAxis = Quaternion.AngleAxis(endAngleX, Vector3.right);
			var endYAxis = Quaternion.AngleAxis(endAngleY, Vector3.up);
			var endZAxis = Quaternion.AngleAxis(endAngleZ, Vector3.forward);

			return from * endXAxis * endYAxis * endZAxis;
		}

		public static Quaternion Lerp(Vector3 fromEular, Vector3 toEular, float t)
		{
			return Quaternion.Euler(Vector3.Lerp(fromEular, toEular, t));
		}

		public static Quaternion Slerp(Vector3 fromEular, Vector3 toEular, float t)
		{
			return Quaternion.Euler(Vector3.Slerp(fromEular, toEular, t));
		}

		// p1 : previous
		// p2 : start
		// p3 : end
		// p4 : next
		public static Vector2 CatmullRom(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, float u)
		{
			var u2 = u * u;
			var u3 = u * u2;
			var p =
				  p1 * (-0.5f * u3 + 1.0f * u2 - 0.5f * u)
				+ p2 * (+1.5f * u3 - 2.5f * u2 + 1.0f)
				+ p3 * (-1.5f * u3 + 2.0f * u2 + 0.5f * u)
				+ p4 * (+0.5f * u3 - 0.5f * u2)
				;
			return p;
		}

		public static Vector3 CatmullRom(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, float u)
		{
			var u2 = u * u;
			var u3 = u * u2;
			var p =
				  p1 * (-0.5f * u3 + 1.0f * u2 - 0.5f * u)
				+ p2 * (+1.5f * u3 - 2.5f * u2 + 1.0f)
				+ p3 * (-1.5f * u3 + 2.0f * u2 + 0.5f * u)
				+ p4 * (+0.5f * u3 - 0.5f * u2)
				;
			return p;
		}
	}
} // Fwk.Math
