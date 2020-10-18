/**
 * 概要：キャプチャー機能
 */
using System.IO;
using UnityEngine;

namespace Fwk.DebugSystem
{
	public static class CaptureScreenshot
	{
		// ----------------------------------------------------------------
		// Method
		// ----------------------------------------------------------------
		public static void Take(string dirName, string fileName)
		{
			string dirPath = ScreenshotDirName + "/" + dirName;
			if (!Directory.Exists(dirPath))
			{
				Directory.CreateDirectory(dirPath);
			}

			// note:拡張子を含めないとダメ
			string path = dirPath + "/" + fileName + ".png";
			ScreenCapture.CaptureScreenshot(path);
			Debug.Log($"スクリーンショットが保存されました => [{path}]");
		}

		public static void Take(string fileName)
		{
			Take("", fileName);
		}

		// ----------------------------------------------------------------
		// Enum/Const
		// ----------------------------------------------------------------
		private const string ScreenshotDirName = "Screenshot";
	}
} // Fwk.DebugSystem
