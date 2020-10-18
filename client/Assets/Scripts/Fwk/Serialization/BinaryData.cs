using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Fwk.Serialization
{
	public static class BinaryData
	{
		// ----------------------------------------------------------------
		// Method
		// ----------------------------------------------------------------
		public static void Serialize(out byte[] data, object value)
		{
#if UNITY_IOS
			System.Environment.SetEnvironmentVariable( "MONO_REFLECTION_SERIALIZER", "yes" );
#endif
			using (var ms = new MemoryStream())
			{
				var bf = new BinaryFormatter();
				bf.Serialize(ms, value);
				data = ms.ToArray();
			}
		}

		public static void Deserialize(out object value, byte[] data)
		{
#if UNITY_IOS
			System.Environment.SetEnvironmentVariable( "MONO_REFLECTION_SERIALIZER", "yes" );
#endif
			using (var ms = new MemoryStream(data))
			{
				var bf = new BinaryFormatter();
				value = bf.Deserialize(ms);
			}
		}
	}
} // Fwk.Serialization
