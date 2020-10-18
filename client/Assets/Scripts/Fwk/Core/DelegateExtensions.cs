namespace Fwk.Core
{
	// ----------------------------------------------------------------
	// C# 6 以上のバージョンで廃止予定
	//  if( self != null ) self.Invoke();
	//  ↓
	//  self?.Invoke();
	//
	//  return ( self != null ) ? self.Invoke( arg ) : result;
	//  ↓
	//  return self?.Invoke( value ) ?? result;
	// ----------------------------------------------------------------
	public static class DelegateExtensions
	{
		// ----------------------------------------------------------------
		// Method
		// ----------------------------------------------------------------
		public static void Call(this System.Action self)
		{
			if (self != null)
			{
				self.Invoke();
			}
		}

		public static void Call<T>(this System.Action<T> self, T arg)
		{
			if (self != null)
			{
				self.Invoke(arg);
			}
		}

		public static void Call<T1, T2>(this System.Action<T1, T2> self, T1 arg1, T2 arg2)
		{
			if (self != null)
			{
				self.Invoke(arg1, arg2);
			}
		}

		public static void Call<T1, T2, T3>(this System.Action<T1, T2, T3> self, T1 arg1, T2 arg2, T3 arg3)
		{
			if (self != null)
			{
				self.Invoke(arg1, arg2, arg3);
			}
		}

		public static void Call<T1, T2, T3, T4>(this System.Action<T1, T2, T3, T4> self, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
			if (self != null)
			{
				self.Invoke(arg1, arg2, arg3, arg4);
			}
		}

		public static TResult Call<TResult>(this System.Func<TResult> self, TResult result = default(TResult))
		{
			return (self != null) ? self.Invoke() : result;
		}

		public static TResult Call<T, TResult>(this System.Func<T, TResult> self, T arg, TResult result = default(TResult))
		{
			return (self != null) ? self.Invoke(arg) : result;
		}

		public static TResult Call<T1, T2, TResult>(this System.Func<T1, T2, TResult> self, T1 arg1, T2 arg2, TResult result = default(TResult))
		{
			return (self != null) ? self.Invoke(arg1, arg2) : result;
		}

		public static TResult Call<T1, T2, T3, TResult>(this System.Func<T1, T2, T3, TResult> self, T1 arg1, T2 arg2, T3 arg3, TResult result = default(TResult))
		{
			return (self != null) ? self.Invoke(arg1, arg2, arg3) : result;
		}

		public static TResult Call<T1, T2, T3, T4, TResult>(this System.Func<T1, T2, T3, T4, TResult> self, T1 arg1, T2 arg2, T3 arg3, T4 arg4, TResult result = default(TResult))
		{
			return (self != null) ? self.Invoke(arg1, arg2, arg3, arg4) : result;
		}
	}
} // Fwk.Core
