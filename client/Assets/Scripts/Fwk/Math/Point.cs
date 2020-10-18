namespace Fwk.Math
{
	public struct Point
	{
		// ----------------------------------------------------------------
		// Method
		// ----------------------------------------------------------------
		public Point(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is Point))
			{
				return false;
			}
			var value = (Point)obj;
			return this == value;
		}

		public override int GetHashCode()
		{
			return x ^ y;
		}

		public override string ToString()
		{
			return "(" + x + "," + y + ")";
		}

		// ----------------------------------------------------------------
		// Property
		// ----------------------------------------------------------------
		public static Point Zero
		{
			get
			{
				return m_Zero;
			}
		}

		// ----------------------------------------------------------------
		// Operator
		// ----------------------------------------------------------------
		public static Point operator +(Point p)
		{
			return new Point(p.x, p.y);
		}

		public static Point operator -(Point p)
		{
			return new Point(-p.x, -p.y);
		}

		public static Point operator +(Point l, Point r)
		{
			return new Point(l.x + r.x, l.y + r.y);
		}

		public static Point operator -(Point l, Point r)
		{
			return new Point(l.x - r.x, l.y - r.y);
		}

		public static Point operator *(Point l, int r)
		{
			return new Point(l.x * r, l.y * r);
		}

		public static Point operator *(int l, Point r)
		{
			return new Point(l * r.x, l * r.y);
		}

		public static Point operator /(Point l, int r)
		{
			return new Point(l.x / r, l.y / r);
		}

		public static bool operator ==(Point l, Point r)
		{
			return (l.x == r.x) && (l.y == r.y);
		}

		public static bool operator !=(Point l, Point r)
		{
			return (l.x != r.x) || (l.y != r.y);
		}

		// ----------------------------------------------------------------
		// Field
		// ----------------------------------------------------------------
		public int x;
		public int y;

		private static Point m_Zero = new Point(0, 0);
	}
} // Fwk.Math
