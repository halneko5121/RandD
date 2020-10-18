namespace Fwk.Math
{
	public static class Mathd
	{
		// ----------------------------------------------------------------
		// Method
		// ----------------------------------------------------------------
		public static double Sin(double d)
		{
			return System.Math.Sin(d);
		}

		public static double Cos(double d)
		{
			return System.Math.Cos(d);
		}

		public static double Tan(double d)
		{
			return System.Math.Tan(d);
		}

		public static double Asin(double d)
		{
			return System.Math.Asin(d);
		}

		public static double Acos(double d)
		{
			return System.Math.Acos(d);
		}

		public static double Atan(double d)
		{
			return System.Math.Atan(d);
		}

		public static double Atan2(double y, double x)
		{
			return System.Math.Atan2(y, x);
		}

		public static double Sqrt(double d)
		{
			return System.Math.Sqrt(d);
		}

		public static double Abs(double d)
		{
			return System.Math.Abs(d);
		}

		public static double Min(double a, double b)
		{
			return a < b ? a : b;
		}

		public static double Min(params double[] values)
		{
			if (values.Length == 0)
			{
				return 0.0;
			}
			var num = double.MaxValue;
			foreach (var value in values)
			{
				if (num > value)
				{
					num = value;
				}
			}
			return num;
		}

		public static double Max(double a, double b)
		{
			return a > b ? a : b;
		}

		public static double Max(params double[] values)
		{
			if (values.Length == 0)
			{
				return 0.0;
			}
			var num = double.MinValue;
			foreach (var value in values)
			{
				if (num < value)
				{
					num = value;
				}
			}
			return num;
		}

		public static double Pow(double d, double p)
		{
			return System.Math.Pow(d, p);
		}

		public static double Exp(double power)
		{
			return System.Math.Exp(power);
		}

		public static double Log(double d, double p)
		{
			return System.Math.Log(d, p);
		}

		public static double Log(double d)
		{
			return System.Math.Log(d);
		}

		public static double Log10(double d)
		{
			return System.Math.Log10(d);
		}

		public static double Ceil(double d)
		{
			return System.Math.Ceiling(d);
		}

		public static int CeilToInt(double d)
		{
			return (int)System.Math.Ceiling(d);
		}

		public static double Floor(double d)
		{
			return System.Math.Floor(d);
		}

		public static int FloorToInt(double d)
		{
			return (int)System.Math.Floor(d);
		}

		public static double Round(double d)
		{
			return System.Math.Round(d);
		}

		public static int RoundToInt(double d)
		{
			return (int)System.Math.Round(d);
		}

		public static double Sign(double d)
		{
			return d >= 0.0 ? 1.0 : -1.0;
		}

		public static double Clamp(double value, double min, double max)
		{
			if (value < min)
			{
				value = min;
			}
			else if (value > max)
			{
				value = max;
			}
			return value;
		}

		public static double Clamp01(double value)
		{
			if (value < 0.0)
			{
				return 0.0;
			}
			if (value > 1.0)
			{
				return 1.0;
			}
			return value;
		}

		public static double Lerp(double from, double to, double t)
		{
			t = Clamp01(t);
			return from + t * (to - from);
		}

		public static double LerpAngle(double a, double b, double t)
		{
			var num = Repeat(b - a, 360.0f);
			if (num > 180.0f)
			{
				num -= 360.0f;
			}
			t = Clamp01(t);
			return a + t * num;
		}

		public static double MoveTowards(double current, double target, double maxDelta)
		{
			if (Abs(target - current) <= maxDelta)
			{
				return target;
			}
			return current + Sign(target - current) * maxDelta;
		}

		public static double MoveTowardsAngle(double current, double target, double maxDelta)
		{
			target = current + DeltaAngle(current, target);
			return MoveTowards(current, target, maxDelta);
		}

		public static double SmoothStep(double from, double to, double t)
		{
			t = Clamp01(t);
			t = -2.0 * t * t * t + 3.0 * t * t;
			return to * t + from * (1.0 - t);
		}

		public static double Gamma(double value, double absmax, double gamma)
		{
			var num1 = Abs(value);
			if (num1 > absmax)
			{
				return value < 0.0 ? -num1 : num1;
			}
			else
			{
				var num2 = Pow(num1 / absmax, gamma) * absmax;
				return value < 0.0 ? -num2 : num2;
			}
		}

		public static bool Approximately(double a, double b)
		{
			return Abs(b - a) < Max(1E-06d * Max(Abs(a), Abs(b)), 1.121039E-44d);
		}

		public static double SmoothDamp(double current, double target, ref double currentVelocity, double smoothTime, double maxSpeed, double deltaTime)
		{
			smoothTime = Max(0.0001, smoothTime);
			var max = maxSpeed * smoothTime;
			var n1 = 2.0 / smoothTime;
			var n2 = n1 * deltaTime;
			var n3 = (1.0 / (1.0 + n2 + 0.479999989271164d * n2 * n2 + 0.234999999403954d * n2 * n2 * n2));
			var n4 = current - target;
			var n5 = target;
			var n6 = Clamp(n4, -max, max);
			var n7 = (currentVelocity + n1 * n6) * deltaTime;
			var n8 = (current - n6) + (n6 + n7) * n3;
			currentVelocity = (currentVelocity - n1 * n7) * n3;
			if (n5 - current > 0.0 == n8 > n5)
			{
				n8 = n5;
				currentVelocity = (n8 - n5) / deltaTime;
			}
			return n8;
		}

		public static double SmoothDampAngle(double current, double target, ref double currentVelocity, double smoothTime, double maxSpeed, double deltaTime)
		{
			target = current + DeltaAngle(current, target);
			return SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		public static double Repeat(double t, double length)
		{
			return t - Floor(t / length) * length;
		}

		public static double PingPong(double t, double length)
		{
			t = Repeat(t, length * 2.0);
			return length - Abs(t - length);
		}

		public static double InverseLerp(double from, double to, double value)
		{
			if (from < to)
			{
				if (value < from)
				{
					return 0.0;
				}
				if (value > to)
				{
					return 1.0;
				}
				value -= from;
				value /= to - from;
				return value;
			}
			else
			{
				if (from <= to)
				{
					return 0.0;
				}
				if (value < to)
				{
					return 1.0;
				}
				if (value > from)
				{
					return 0.0;
				}
				else
				{
					return (1.0 - (value - to) / (from - to));
				}
			}
		}

		public static double DeltaAngle(double current, double target)
		{
			var num = Repeat(target - current, 360.0);
			if (num > 180.0)
			{
				num -= 360.0;
			}
			return num;
		}

		internal static bool LineIntersection(Vector3d p1, Vector3d p2, Vector3d p3, Vector3d p4, ref Vector3d result)
		{
			var n1 = p2.x - p1.x;
			var n2 = p2.y - p1.y;
			var n3 = p4.x - p3.x;
			var n4 = p4.y - p3.y;
			var n5 = n1 * n4 - n2 * n3;
			if (n5 == 0.0)
			{
				return false;
			}
			var n6 = p3.x - p1.x;
			var n7 = p3.y - p1.y;
			var n8 = (n6 * n4 - n7 * n3) / n5;
			result = new Vector3d(p1.x + n8 * n1, p1.y + n8 * n2);
			return true;
		}

		internal static bool LineSegmentIntersection(Vector3d p1, Vector3d p2, Vector3d p3, Vector3d p4, ref Vector3d result)
		{
			var n1 = p2.x - p1.x;
			var n2 = p2.y - p1.y;
			var n3 = p4.x - p3.x;
			var n4 = p4.y - p3.y;
			var n5 = (n1 * n4 - n2 * n3);
			if (n5 == 0.0)
			{
				return false;
			}
			var n6 = p3.x - p1.x;
			var n7 = p3.y - p1.y;
			var n8 = (n6 * n4 - n7 * n3) / n5;
			if (n8 < 0.0 || n8 > 1.0)
			{
				return false;
			}
			var n9 = (n6 * n2 - n7 * n1) / n5;
			if (n9 < 0.0 || n9 > 1.0)
			{
				return false;
			}
			result = new Vector3d(p1.x + n8 * n1, p1.y + n8 * n2);
			return true;
		}

		// ----------------------------------------------------------------
		// Field
		// ----------------------------------------------------------------
		public const double PI = System.Math.PI;
		public const double Infinity = double.PositiveInfinity;
		public const double NegativeInfinity = double.NegativeInfinity;
		public const double Deg2Rad = 0.01745329251d;
		public const double Rad2Deg = 57.2957795131d;
		public const double Epsilon = 1.401298E-45d;
	}
} // Fwk.Math
