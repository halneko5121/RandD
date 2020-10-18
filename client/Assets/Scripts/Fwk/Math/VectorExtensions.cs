using UnityEngine;

namespace Fwk.Math
{
	public static class VectorExtensions
	{
		// ----------------------------------------------------------------
		// Method
		// ----------------------------------------------------------------
		public static bool EqualsField(this Vector2 self, Vector2 other)
		{
			return (true
				&& (self.x == other.x)
				&& (self.y == other.y)
				);
		}

		public static bool EqualsField(this Vector3 self, Vector3 other)
		{
			return (true
				&& (self.x == other.x)
				&& (self.y == other.y)
				&& (self.z == other.z)
				);
		}

		public static bool EqualsField(this Vector4 self, Vector4 other)
		{
			return (true
				&& (self.x == other.x)
				&& (self.y == other.y)
				&& (self.z == other.z)
				&& (self.w == other.w)
				);
		}

		public static bool IsZero(this Vector2 self)
		{
			return (true
				&& (self.x == 0.0f)
				&& (self.y == 0.0f)
				);
		}

		public static bool IsZero(this Vector3 self)
		{
			return (true
				&& (self.x == 0.0f)
				&& (self.y == 0.0f)
				&& (self.z == 0.0f)
				);
		}

		public static bool IsZero(this Vector4 self)
		{
			return (true
				&& (self.x == 0.0f)
				&& (self.y == 0.0f)
				&& (self.z == 0.0f)
				&& (self.w == 0.0f)
				);
		}
	}
} // Fwk.Math
