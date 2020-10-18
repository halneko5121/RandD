using System.Collections.Generic;
using UnityEngine;

namespace Fwk.Core
{
	public static class TransformExtensions
	{
		// ----------------------------------------------------------------
		// Method
		// ----------------------------------------------------------------
		public static Transform FindRecursively( this Transform self, string name )
		{
			if( self.name == name )
			{
				return self;
			}
			else
			{
				for( var i = 0; i < self.childCount; ++i )
				{
					var t = self.GetChild( i ).FindRecursively( name );
					if( t != null )
					{
						return t;
					}
				}
			}
			return null;
		}

		public static void ToListRecursively( this Transform self, ref List<Transform> list )
		{
			if( list == null )
			{
				list = new List<Transform>();
			}

			list.Add( self );

			for( var i = 0; i < self.childCount; ++i )
			{
				self.GetChild( i ).ToListRecursively( ref list );
			}
		}

		public static string GetPath( this Transform self )
		{
			string path = self.gameObject.name;

			Transform parent = self.parent;

			while (parent != null)
			{
				path = parent.name + "/" + path;
				parent = parent.parent;
			}

			return path;
		}
	}
} // Fwk.Core
