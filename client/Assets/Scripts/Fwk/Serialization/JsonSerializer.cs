using UnityEngine;
using Fwk.Core;

namespace Fwk.Serialization
{
	public static class JsonSerializer
	{
		// ----------------------------------------------------------------
		// Method
		// ----------------------------------------------------------------
		/// <summary>
		/// シリアライズ
		/// </summary>
		public static string Serialize<T>(T obj)
		{
			try
			{
				return JsonUtility.ToJson(obj);
			}
			catch (System.Exception ex)
			{
				Debug.LogException(ex);
				Dump.Log("JsonError", obj);
				throw;
			}
		}

		/// <summary>
		/// デシリアライズ
		/// </summary>
		public static T Deserialize<T>(string data)
		{
			try
			{
				return JsonUtility.FromJson<T>(data);
			}
			catch (System.Exception ex)
			{
				Debug.LogException(ex);
				Dump.Log("JsonError", data);
				throw;
			}
		}
	}
} // Fwk.Serialization
