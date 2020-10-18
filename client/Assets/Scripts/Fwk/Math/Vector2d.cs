using UnityEngine;

namespace Fwk.Math
{
	[System.Serializable]
	public struct Vector2d
	{
		// ----------------------------------------------------------------
		// Method
		// ----------------------------------------------------------------
		public Vector2d(double x, double y)
		{
			this.x = x;
			this.y = y;
		}

		public Vector2d(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		public Vector2d(Vector2 v)
		{
			x = v.x;
			y = v.y;
		}

		public void Set(double x, double y)
		{
			this.x = x;
			this.y = y;
		}

		public void Scale(Vector2d v)
		{
			x *= v.x;
			y *= v.y;
		}

		public static Vector2d Lerp(Vector2d from, Vector2d to, double t)
		{
			t = Mathd.Clamp01(t);
			return new Vector2d(from.x + (to.x - from.x) * t, from.y + (to.y - from.y) * t);
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

		public static Vector2d Scale(Vector2d lhs, Vector2d rhs)
		{
			return new Vector2d(lhs.x * rhs.x, lhs.y * rhs.y);
		}

		public static double Dot(Vector2d lhs, Vector2d rhs)
		{
			return lhs.x * rhs.x + lhs.y * rhs.y;
		}

		public static Vector2d Reflect(Vector2d inDirection, Vector2d inNormal)
		{
			return -2.0 * Dot(inNormal, inDirection) * inNormal + inDirection;
		}

		public static Vector2d Normalize(Vector2d v)
		{
			var num = Magnitude(v);
			if (num > 9.99999974737875E-06)
			{
				return v / num;
			}
			return Zero;
		}

		public static Vector2d Project(Vector2d vector, Vector2d onNormal)
		{
			var num = Dot(onNormal, onNormal);
			if (num < 1.40129846432482E-45d)
			{
				return Zero;
			}
			return onNormal * Dot(vector, onNormal) / num;
		}

		public static Vector2d Exclude(Vector2d excludeThis, Vector2d fromThat)
		{
			return fromThat - Project(fromThat, excludeThis);
		}

		public static double Angle(Vector2d from, Vector2d to)
		{
			return Mathd.Acos(Mathd.Clamp(Dot(from.Normalized, to.Normalized), -1.0, 1.0)) * 57.29578d;
		}

		public static double Distance(Vector2d lhs, Vector2d rhs)
		{
			var v = new Vector2d(lhs.x - rhs.x, lhs.y - rhs.y);
			return Mathd.Sqrt(v.x * v.x + v.y * v.y);
		}

		public static Vector2d ClampMagnitude(Vector2d vector, double maxLength)
		{
			if (vector.SqrMagnitude() > maxLength * maxLength)
			{
				return vector.Normalized * maxLength;
			}
			return vector;
		}

		public static double Magnitude(Vector2d v)
		{
			return Mathd.Sqrt(v.x * v.x + v.y * v.y);
		}

		public double Magnitude()
		{
			return Mathd.Sqrt(x * x + y * y);
		}

		public static double SqrMagnitude(Vector2d v)
		{
			return v.x * v.x + v.y * v.y;
		}

		public double SqrMagnitude()
		{
			return x * x + y * y;
		}

		public static Vector2d Min(Vector2d lhs, Vector2d rhs)
		{
			return new Vector2d(Mathd.Min(lhs.x, rhs.x), Mathd.Min(lhs.y, rhs.y));
		}

		public static Vector2d Max(Vector2d lhs, Vector2d rhs)
		{
			return new Vector2d(Mathd.Max(lhs.x, rhs.x), Mathd.Max(lhs.y, rhs.y));
		}

		public static Vector2d MoveTowards(Vector2d current, Vector2d target, double maxDistanceDelta)
		{
			var v = target - current;
			var magnitude = v.Magnitude();
			if (magnitude <= maxDistanceDelta || magnitude == 0.0)
			{
				return target;
			}
			return current + v / magnitude * maxDistanceDelta;
		}

		public static Vector2d SmoothDamp(Vector2d current, Vector2d target, ref Vector2d currentVelocity, double smoothTime, double maxSpeed, double deltaTime)
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
			return x.GetHashCode() ^ y.GetHashCode() << 2;
		}

		public override bool Equals(object other)
		{
			if (!(other is Vector2d))
			{
				return false;
			}
			return this == (Vector2d)other;
		}

		public override string ToString()
		{
			return "( " + x.ToString("F16") + ", " + y.ToString("F16") + " )";
		}

		public string ToString(string format)
		{
			return "( " + x.ToString(format) + ", " + y.ToString(format) + " )";
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
					default:
						throw new System.IndexOutOfRangeException("Invalid Vector2d index!");
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
					default:
						throw new System.IndexOutOfRangeException("Invalid Vector2d index!");
				}
			}
		}

		public Vector2d Normalized
		{
			get
			{
				return Normalize(this);
			}
		}

		public static Vector2d Zero
		{
			get
			{
				return new Vector2d(0.0, 0.0);
			}
		}

		public static Vector2d One
		{
			get
			{
				return new Vector2d(1.0, 1.0);
			}
		}

		public static Vector2d Up
		{
			get
			{
				return new Vector2d(0.0, 1.0);
			}
		}

		public static Vector2d Down
		{
			get
			{
				return new Vector2d(0.0, -1.0);
			}
		}

		public static Vector2d Left
		{
			get
			{
				return new Vector2d(-1.0, 0.0);
			}
		}

		public static Vector2d Right
		{
			get
			{
				return new Vector2d(1.0, 0.0);
			}
		}

		// ----------------------------------------------------------------
		// Operator
		// ----------------------------------------------------------------
		public static Vector2d operator +(Vector2d lhs, Vector2d rhs)
		{
			return new Vector2d(lhs.x + rhs.x, lhs.y + rhs.y);
		}

		public static Vector2d operator -(Vector2d lhs, Vector2d rhs)
		{
			return new Vector2d(lhs.x - rhs.x, lhs.y - rhs.y);
		}

		public static Vector2d operator -(Vector2d v)
		{
			return new Vector2d(-v.x, -v.y);
		}

		public static Vector2d operator *(Vector2d v, double d)
		{
			return new Vector2d(v.x * d, v.y * d);
		}

		public static Vector2d operator *(double d, Vector2d v)
		{
			return new Vector2d(v.x * d, v.y * d);
		}

		public static Vector2d operator /(Vector2d v, double d)
		{
			return new Vector2d(v.x / d, v.y / d);
		}

		public static bool operator ==(Vector2d lhs, Vector2d rhs)
		{
			return SqrMagnitude(lhs - rhs) < 0.0 / 1.0;
		}

		public static bool operator !=(Vector2d lhs, Vector2d rhs)
		{
			return SqrMagnitude(lhs - rhs) >= 0.0 / 1.0;
		}

		// ----------------------------------------------------------------
		// Field
		// ----------------------------------------------------------------
		public double x;
		public double y;

		public const double kEpsilon = 1E-05d;
	}
} // Fwk.Math
