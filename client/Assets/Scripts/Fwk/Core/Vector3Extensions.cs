using UnityEngine;

namespace Fwk.Core
{
	public static class Vector3Extensions
	{
		// ----------------------------------------------------------------
		// Method
		// ----------------------------------------------------------------
		public static bool IsZero( this Vector3 self )
		{
			return Equals( self, Vector3.zero );
		}

		public static bool Equals( this Vector3 self, Vector3 other )
		{
			return true
				&& ( self.x == other.x )
				&& ( self.y == other.y )
				&& ( self.z == other.z )
				;
		}
	}
} // Fwk.Core
