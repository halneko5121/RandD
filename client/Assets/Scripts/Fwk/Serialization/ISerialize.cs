namespace Fwk.Serialization
{
	public interface ISerialize<ObjectType, SerializeType>
	{
		/// <summary>
		/// シリアライズ
		/// </summary>
		SerializeType Serialize(ObjectType obj);

		/// <summary>
		/// デシリアライズ
		/// </summary>
		ObjectType Deserialize(SerializeType data);
	}
} // Fwk.Serialization
