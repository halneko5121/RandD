namespace Fwk.Math
{
	public sealed class Rectd
	{
		// ----------------------------------------------------------------
		// Method
		// ----------------------------------------------------------------
		public Rectd(Vector3d min, Vector3d size)
		{
			this.Min = min;
			this.Size = size;
		}

		public bool Contains(Vector3d point)
		{
			return ((Width < 0.0) && (point.x <= Min.x) && (point.x > (Min.x + Size.x)) || (Width >= 0.0) && (point.x >= Min.x) && (point.x < (Min.x + Size.x)))
				&& ((Height < 0.0) && (point.y <= Min.y) && (point.y > (Min.y + Size.y)) || (Height >= 0.0) && (point.y >= Min.y) && (point.y < (Min.y + Size.y)));
		}

		// ----------------------------------------------------------------
		// Property
		// ----------------------------------------------------------------
		public Vector3d Min
		{
			get;
			private set;
		}

		public Vector3d Size
		{
			get;
			private set;
		}

		public Vector3d Center
		{
			get
			{
				return new Vector3d(Min.x + Size.x * 0.5, Min.y + Size.y * 0.5);
			}
			set
			{
				Min = new Vector3d(value.x - Size.x * 0.5, value.x - Size.y * 0.5);
			}
		}

		public double Height
		{
			get
			{
				return Size.y;
			}
		}

		public double Width
		{
			get
			{
				return Size.x;
			}
		}
	}
} // Fwk.Math
