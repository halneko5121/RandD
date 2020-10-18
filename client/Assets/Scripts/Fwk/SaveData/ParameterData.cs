/**
 * 概要：外部ファイルを使用したパラメータ保存クラス(Editor起動中しか保存できない)
 *		 このクラスの派生クラスは、メンバ変数がJsonに変換され
 *		 クラス名と同じファイル名で保存される。
 */
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Fwk.SaveData
{
	public class ParameterData : MonoBehaviour
	{
		// ----------------------------------------------------------------
		// MonoBehaviour
		// ----------------------------------------------------------------
		void OnDestroy()
		{
			Resources.UnloadAsset(m_JsonAsset);
		}

		// ----------------------------------------------------------------
		// Method
		// ----------------------------------------------------------------
		public bool Setup(string filePath)
		{
			m_FilePath = filePath;
			var fullPath = GetFullPath(filePath);
			Debug.LogFormat("filePath [{0}] fullPath [{1}]", filePath, fullPath);

			return Load();
		}

		public void Save()
		{
			var fullPath = GetFullPath(m_FilePath);
			SaveFile(fullPath);
		}

		public void Save(string filePath)
		{
			var fullPath = GetFullPath(filePath);
			SaveFile(fullPath);
		}

		public bool Load()
		{
			// 読み込み
			var paramPath = ParamerterDataDirName + "/" + m_FilePath;
			m_JsonAsset = Resources.Load(paramPath) as TextAsset;
			if (m_JsonAsset != null)
			{
				// JSON で上書き
				JsonUtility.FromJsonOverwrite(m_JsonAsset.text, this);
				return true;
			}

			return false;
		}

		private void SaveFile(string fullPath)
		{
			// ディレクトリがない場合は生成する
			var dirPath = Path.GetDirectoryName(fullPath);
			if (!Directory.Exists(dirPath))
			{
				Directory.CreateDirectory(dirPath);
			}

			// クラスを json に変換
			var json = JsonUtility.ToJson(this, true);
			using (StreamWriter streamWriter = new StreamWriter(fullPath, false))
			{
				try
				{
					streamWriter.Write(json);
				}
				catch
				{
				}

				streamWriter.Flush();
				streamWriter.Close();
			}

#if UNITY_EDITOR
			AssetDatabase.Refresh();
#endif
		}

		private string GetFullPath(string filePath)
		{
			return ParamerterDataDirPath + filePath + ".json";
		}

		// ----------------------------------------------------------------
		// Enum/Const
		// ----------------------------------------------------------------
		private const string ParamerterDataDirPath = "Assets/Resources/Parameter/";
		private const string ParamerterDataDirName = "Parameter";

		// ----------------------------------------------------------------
		// Field
		// ----------------------------------------------------------------
		string m_FilePath = default;
		TextAsset m_JsonAsset = default;
	}
} // Fwk.SaveData
