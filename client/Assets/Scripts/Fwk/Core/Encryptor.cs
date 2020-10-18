/**
 * 概要：暗号化・復号化処理
 * 参考：https://qiita.com/kz-rv04/items/62a56bd4cd149e36ca70
 */
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Fwk.Core
{
    public static class Encryptor
    {
		// ----------------------------------------------------------------
		// Method
		// ----------------------------------------------------------------
		/// <summary>
		/// 対象鍵暗号を使って文字列を暗号化する
		/// </summary>
		/// <param name="src">暗号化する文字列</param>
		/// <returns></returns>
		public static byte[] Encrypt(byte[] src)
		{
			using (RijndaelManaged rijndael = new RijndaelManaged())
			{
				rijndael.BlockSize = 128;
				rijndael.KeySize = 128;
				rijndael.Mode = CipherMode.CBC;
				rijndael.Padding = PaddingMode.PKCS7;

				rijndael.IV = Encoding.UTF8.GetBytes(AesIV);
				rijndael.Key = Encoding.UTF8.GetBytes(AesKey);

				ICryptoTransform encryptor = rijndael.CreateEncryptor(rijndael.Key, rijndael.IV);
				byte[] dest;
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
					{
						cryptoStream.Write(src, 0, src.Length);
					}
					dest = memoryStream.ToArray();
				}

				return dest;
			}
		}

		/// <summary>
		/// 対象鍵暗号を使って暗号文を復号化する
		/// </summary>
		/// <param name="encryptedData">暗号データ</param>
		/// <returns></returns>
		static public byte[] Decrypt(byte[] encryptedData)
		{
			using (RijndaelManaged rijndael = new RijndaelManaged())
			{
				rijndael.BlockSize = 128;
				rijndael.KeySize = 128;
				rijndael.Mode = CipherMode.CBC;
				rijndael.Padding = PaddingMode.PKCS7;

				rijndael.IV = Encoding.UTF8.GetBytes(AesIV);
				rijndael.Key = Encoding.UTF8.GetBytes(AesKey);

				ICryptoTransform decryptor = rijndael.CreateDecryptor(rijndael.Key, rijndael.IV);
				byte[] dest = null;

				using (MemoryStream encryptedStream = new MemoryStream(encryptedData))
				{
					using (MemoryStream dencryptedStream = new MemoryStream())
					{
						using (CryptoStream cryptoStream = new CryptoStream(encryptedStream, decryptor, CryptoStreamMode.Read))
						{
							cryptoStream.CopyTo(dencryptedStream);
						}
						dest = dencryptedStream.ToArray();
					}
				}

				return dest;
			}
		}

		// ----------------------------------------------------------------
		// Enum/Const
		// ----------------------------------------------------------------
		private const string AesIV = @"pf69DL6GrWFyZcMK";
		private const string AesKey = @"9Fix4L4HB4PKeKWY";
	}
} // Fwk.Core
