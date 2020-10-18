using UnityEngine;

namespace Fwk.Math
{
	public static class AngleUtil
	{
		// ----------------------------------------------------------------
		// Method
		// ----------------------------------------------------------------
		public static Vector2 CorrectAngle(Vector2 angle, Vector2 idealAngle)
		{
			return new Vector2(CorrectAngle(angle.x, idealAngle.x), CorrectAngle(angle.y, idealAngle.y));
		}

		private static float CorrectAngle(float from, float to)
		{
			if (Mathf.Abs(from - to) > 180.0f)
			{
				int fd;
				int fr;
				int td;
				int tr;
				CorrectAngle(out fd, out fr, Mathf.RoundToInt(from));
				CorrectAngle(out td, out tr, Mathf.RoundToInt(to));

				var r = 0;
				var a = td;
				var b = fr;
				while (true)
				{
					r = a * 360 + b;
					var d = r - to;
					if (d > 180.0f)
					{
						--a;
					}
					else if (d < -180.0f)
					{
						++a;
					}
					else
					{
						break;
					}
				}
				return r;
			}
			return from;
		}

		private static void CorrectAngle(out int d, out int r, int angle)
		{
			if (angle < 0)
			{
				angle = -angle - 1;
				d = -(angle / 360) - 1;
				r = 359 - angle % 360;
			}
			else
			{
				d = angle / 360;
				r = angle % 360;
			}
		}
	}
} // Fwk.Math
