using System.Diagnostics;

namespace Fwk.Core
{
	public static class Dump
	{
		// ----------------------------------------------------------------
		// Method
		// ----------------------------------------------------------------
		[Conditional("FWK_DEBUG")]
		public static void Log(object obj)
		{
			UnityEngine.Debug.Log(Serialization.JsonSerializer.Serialize(obj));
		}

		[Conditional("FWK_DEBUG")]
		public static void Log(string text, object obj)
		{
			UnityEngine.Debug.Log(text + "=" + Serialization.JsonSerializer.Serialize(obj));
		}

		[Conditional("FWK_DEBUG")]
		public static void LogWarning(object obj)
		{
			UnityEngine.Debug.LogWarning(Serialization.JsonSerializer.Serialize(obj));
		}

		[Conditional("FWK_DEBUG")]
		public static void LogWarning(string text, object obj)
		{
			UnityEngine.Debug.LogWarning(text + "=" + Serialization.JsonSerializer.Serialize(obj));
		}
	}
} // Fwk.Core
