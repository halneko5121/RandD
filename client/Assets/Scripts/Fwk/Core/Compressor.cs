/**
 * 概要：圧縮・解凍処理
 * 参考：https://qiita.com/tokoroten-lab/items/b865edaa0e3018cb5e55
 */
using System.IO;
using System.IO.Compression;

namespace Fwk.Core
{
	public static class Compressor
	{
		// ----------------------------------------------------------------
		// Method
		// ----------------------------------------------------------------
		/// <summary>
		/// 圧縮
		/// </summary>
		/// <param name="rawData">圧縮前データ</param>
		public static byte[] Compress(byte[] rawData)
		{
			byte[] result = null;

			using (MemoryStream compressedStream = new MemoryStream())
			{
				using (GZipStream gZipStream = new GZipStream(compressedStream, CompressionMode.Compress))
				{
					gZipStream.Write(rawData, 0, rawData.Length);
				}
				result = compressedStream.ToArray();
			}

			return result;
		}

		/// <summary>
		/// 解凍処理
		/// </summary>
		/// <param name="compressedData">圧縮データ</param>
		/// <returns></returns>
		static public byte[] Decompress(byte[] compressedData)
		{
			byte[] result = null;

			using (MemoryStream compressedStream = new MemoryStream(compressedData))
			{
				using (MemoryStream decompressedStream = new MemoryStream())
				{
					using (GZipStream gZipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
					{
						gZipStream.CopyTo(decompressedStream);
					}
					result = decompressedStream.ToArray();
				}
			}

			return result;
		}
	}
} // Fwk.Core
