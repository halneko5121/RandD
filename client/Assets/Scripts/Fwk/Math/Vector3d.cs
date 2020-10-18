using UnityEngine;

namespace Fwk.Math
{
	[System.Serializable]
	public struct Vector3d
	{
		// ----------------------------------------------------------------
		// Method
		// ----------------------------------------------------------------
		public Vector3d(double x, double y, double z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public Vector3d(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public Vector3d(Vector3 v)
		{
			x = v.x;
			y = v.y;
			z = v.z;
		}

		public Vector3d(double x, double y)
		{
			this.x = x;
			this.y = y;
			z = 0.0;
		}

		public void Set(double x, double y, double z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public void Scale(Vector3d v)
		{
			x *= v.x;
			y *= v.y;
			z *= v.z;
		}

		public static Vector3d Lerp(Vector3d from, Vector3d to, double t)
		{
			t = Mathd.Clamp01(t);
			return new Vector3d(from.x + (to.x - from.x) * t, from.y + (to.y - from.y) * t, from.z + (to.z - from.z) * t);
		}

		public static Vector3d Slerp(Vector3d from, Vector3d to, double t)
		{
			var v = Vector3.Slerp((Vector3)from, (Vector3)to, (float)t);
			return new Vector3d(v);
		}

		public void Normalize()
		{
			var num = Magnitude(this);
			if (num > 9.99999974737875E-06)
			{
				this = this / num;
			}
			this = Zero;
		}

		public static Vector3d Scale(Vector3d lhs, Vector3d rhs)
		{
			return new Vector3d(lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z);
		}

		public static Vector3d Cross(Vector3d lhs, Vector3d rhs)
		{
			return new Vector3d(lhs.y * rhs.z - lhs.z * rhs.y, lhs.z * rhs.x - lhs.x * rhs.z, lhs.x * rhs.y - lhs.y * rhs.x);
		}

		public static double Dot(Vector3d lhs, Vector3d rhs)
		{
			return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
		}

		public static Vector3d Reflect(Vector3d inDirection, Vector3d inNormal)
		{
			return -2.0 * Dot(inNormal, inDirection) * inNormal + inDirection;
		}

		public static Vector3d Normalize(Vector3d v)
		{
			var num = Magnitude(v);
			if (num > 9.99999974737875E-06)
			{
				return v / num;
			}
			return Zero;
		}

		public static Vector3d Project(Vector3d vector, Vector3d onNormal)
		{
			var num = Dot(onNormal, onNormal);
			if (num < 1.40129846432482E-45d)
			{
				return Zero;
			}
			return onNormal * Dot(vector, onNormal) / num;
		}

		public static Vector3d Exclude(Vector3d excludeThis, Vector3d fromThat)
		{
			return fromThat - Project(fromThat, excludeThis);
		}

		public static double Angle(Vector3d from, Vector3d to)
		{
			return Mathd.Acos(Mathd.Clamp(Dot(from.Normalized, to.Normalized), -1.0, 1.0)) * 57.29578d;
		}

		public static double Distance(Vector3d lhs, Vector3d rhs)
		{
			var v = new Vector3d(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
			return Mathd.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);
		}

		public static Vector3d ClampMagnitude(Vector3d vector, double maxLength)
		{
			if (vector.SqrMagnitude() > maxLength * maxLength)
			{
				return vector.Normalized * maxLength;
			}
			return vector;
		}

		public static double Magnitude(Vector3d v)
		{
			return Mathd.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);
		}

		public double Magnitude()
		{
			return Mathd.Sqrt(x * x + y * y + z * z);
		}

		public static double SqrMagnitude(Vector3d v)
		{
			return v.x * v.x + v.y * v.y + v.z * v.z;
		}

		public double SqrMagnitude()
		{
			return x * x + y * y + z * z;
		}

		public static Vector3d Min(Vector3d lhs, Vector3d rhs)
		{
			return new Vector3d(Mathd.Min(lhs.x, rhs.x), Mathd.Min(lhs.y, rhs.y), Mathd.Min(lhs.z, rhs.z));
		}

		public static Vector3d Max(Vector3d lhs, Vector3d rhs)
		{
			return new Vector3d(Mathd.Max(lhs.x, rhs.x), Mathd.Max(lhs.y, rhs.y), Mathd.Max(lhs.z, rhs.z));
		}

		public static void OrthoNormalize(ref Vector3d normal, ref Vector3d tangent)
		{
			var n = (Vector3)normal;
			var t = (Vector3)tangent;
			Vector3.OrthoNormalize(ref n, ref t);
			normal = new Vector3d(n);
			tangent = new Vector3d(t);
		}

		public static void OrthoNormalize(ref Vector3d normal, ref Vector3d tangent, ref Vector3d binormal)
		{
			var n = (Vector3)normal;
			var t = (Vector3)tangent;
			var b = (Vector3)binormal;
			Vector3.OrthoNormalize(ref n, ref t, ref b);
			normal = new Vector3d(n);
			tangent = new Vector3d(t);
			binormal = new Vector3d(b);
		}

		public static Vector3d MoveTowards(Vector3d current, Vector3d target, double maxDistanceDelta)
		{
			var v = target - current;
			var magnitude = v.Magnitude();
			if (magnitude <= maxDistanceDelta || magnitude == 0.0)
			{
				return target;
			}
			return current + v / magnitude * maxDistanceDelta;
		}

		public static Vector3d RotateTowards(Vector3d current, Vector3d target, double maxRadiansDelta, double maxMagnitudeDelta)
		{
			var v = Vector3.RotateTowards((Vector3)current, (Vector3)target, (float)maxRadiansDelta, (float)maxMagnitudeDelta);
			return new Vector3d(v);
		}

		public static Vector3d SmoothDamp(Vector3d current, Vector3d target, ref Vector3d currentVelocity, double smoothTime, double maxSpeed, double deltaTime)
		{
			smoothTime = Mathd.Max(0.0001d, smoothTime);
			var maxLength = maxSpeed * smoothTime;
			var n1 = 2.0 / smoothTime;
			var n2 = n1 * deltaTime;
			var n3 = (1.0 / (1.0 + n2 + 0.479999989271164d * n2 * n2 + 0.234999999403954d * n2 * n2 * n2));
			var v1 = current - target;
			var v2 = target;
			var v3 = ClampMagnitude(v1, maxLength);
			var v4 = (currentVelocity + n1 * v3) * deltaTime;
			var v5 = (current - v3) + (v3 + v4) * n3;
			if (Dot(v2 - current, v5 - v2) > 0.0)
			{
				v5 = v2;
				currentVelocity = (currentVelocity - n1 * v4) * n3;
				currentVelocity = (v5 - v2) / deltaTime;
			}
			return v5;
		}

		public override int GetHashCode()
		{
			return x.GetHashCode() ^ y.GetHashCode() << 2 ^ z.GetHashCode() >> 2;
		}

		public override bool Equals(object other)
		{
			if (!(other is Vector3d))
			{
				return false;
			}
			return this == (Vector3d)other;
		}

		public override string ToString()
		{
			return "( " + x.ToString("F16") + ", " + y.ToString("F16") + ", " + z.ToString("F16") + " )";
		}

		public string ToString(string format)
		{
			return "( " + x.ToString(format) + ", " + y.ToString(format) + ", " + z.ToString(format) + " )";
		}

		// ----------------------------------------------------------------
		// Property
		// ----------------------------------------------------------------
		public double this[int index]
		{
			get
			{
				switch (index)
				{
					case 0:
						return x;
					case 1:
						return y;
					case 2:
						return z;
					default:
						throw new System.IndexOutOfRangeException("Invalid Vector3d index!");
				}
			}
			set
			{
				switch (index)
				{
					case 0:
						x = value;
						break;
					case 1:
						y = value;
						break;
					case 2:
						z = value;
						break;
					default:
						throw new System.IndexOutOfRangeException("Invalid Vector3d index!");
				}
			}
		}

		public Vector3d Normalized
		{
			get
			{
				return Normalize(this);
			}
		}

		public static Vector3d Zero
		{
			get
			{
				return new Vector3d(0.0, 0.0, 0.0);
			}
		}

		public static Vector3d One
		{
			get
			{
				return new Vector3d(1.0, 1.0, 1.0);
			}
		}

		public static Vector3d Forward
		{
			get
			{
				return new Vector3d(0.0, 0.0, 1.0);
			}
		}

		public static Vector3d Back
		{
			get
			{
				return new Vector3d(0.0, 0.0, -1.0);
			}
		}

		public static Vector3d Up
		{
			get
			{
				return new Vector3d(0.0, 1.0, 0.0);
			}
		}

		public static Vector3d Down
		{
			get
			{
				return new Vector3d(0.0, -1.0, 0.0);
			}
		}

		public static Vector3d Left
		{
			get
			{
				return new Vector3d(-1.0, 0.0, 0.0);
			}
		}

		public static Vector3d Right
		{
			get
			{
				return new Vector3d(1.0, 0.0, 0.0);
			}
		}

		// ----------------------------------------------------------------
		// Operator
		// ----------------------------------------------------------------
		public static Vector3d operator +(Vector3d lhs, Vector3d rhs)
		{
			return new Vector3d(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
		}

		public static Vector3d operator -(Vector3d lhs, Vector3d rhs)
		{
			return new Vector3d(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
		}

		public static Vector3d operator -(Vector3d v)
		{
			return new Vector3d(-v.x, -v.y, -v.z);
		}

		public static Vector3d operator *(Vector3d v, double d)
		{
			return new Vector3d(v.x * d, v.y * d, v.z * d);
		}

		public static Vector3d operator *(double d, Vector3d v)
		{
			return new Vector3d(v.x * d, v.y * d, v.z * d);
		}

		public static Vector3d operator /(Vector3d v, double d)
		{
			return new Vector3d(v.x / d, v.y / d, v.z / d);
		}

		public static bool operator ==(Vector3d lhs, Vector3d rhs)
		{
			return SqrMagnitude(lhs - rhs) < 0.0 / 1.0;
		}

		public static bool operator !=(Vector3d lhs, Vector3d rhs)
		{
			return SqrMagnitude(lhs - rhs) >= 0.0 / 1.0;
		}

		// Vector3
		public static explicit operator Vector3(Vector3d v)
		{
			return new Vector3((float)v.x, (float)v.y, (float)v.z);
		}

		// ----------------------------------------------------------------
		// Field
		// ----------------------------------------------------------------
		public double x;
		public double y;
		public double z;

		public const double kEpsilon = 1E-05d;
	}
} // Fwk.Math
