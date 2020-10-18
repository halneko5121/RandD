/**
 * 概要：外部ファイルを使用したセーブデータクラス
 *		 このクラスの派生クラスは、メンバ変数がJsonに変換され
 *		 クラス名と同じファイル名で保存される。
 */
using Fwk.Core;
using System.IO;
using System.Text;
using UnityEngine;

namespace Fwk.SaveData
{
	public interface ISaveData
	{
		void Save();
		void Load();
		void Delete();
		bool IsLoadSucess();
	}

    public class SaveData : ISaveData
	{
		// ----------------------------------------------------------------
		// Method
		// ----------------------------------------------------------------
		public SaveData(string dirName)
		{
			var fileName = this.GetType().Name;
			var backupFileName = this.GetType().Name + "_Backup";
			var hashString = fileName.GetHashCode().ToString();
			var backupHashString = backupFileName.GetHashCode().ToString();
#if UNITY_EDITOR
			m_FilePath = Path.Combine(SaveDataDirName, dirName, fileName);
			m_BackupFilePath = Path.Combine(SaveDataDirName, dirName, backupFileName);
#else
			m_FilePath = Path.Combine(dirName, hashString);
			m_BackupFilePath = Path.Combine(dirName, backupHashString);
#endif
			Debug.LogFormat("[{0}] filename [{1}] buckup filename [{2}] ", fileName, hashString, backupHashString);
			// Load()
		}

		public void Save()
		{
			string[] paths = { FilePath, BackupFilePath };
			var json = Serialization.JsonSerializer.Serialize<dynamic>(this);

			foreach (var path in paths)
			{
				// ディレクトリがない場合は生成する
				var dirPath = Path.GetDirectoryName(path);
				if (!Directory.Exists(dirPath))
				{
					Directory.CreateDirectory(dirPath);
				}

				using (FileStream fileStream = File.Create(path))
				{
					SaveFile(fileStream, json);
				}
			}
		}

		public void Load()
		{
			// ディレクトリがない場合は生成する
			var dirPath = Path.GetDirectoryName(FilePath);
			if (!Directory.Exists(dirPath))
			{
				Directory.CreateDirectory(dirPath);
			}

			try
			{
				LoadFile(FilePath);
			}
			catch
			{
				// 失敗した場合は予備のファイルをロードする
				try
				{
					LoadFile(BackupFilePath);
					m_IsLoadSucess = false;
					Debug.LogFormat("セーブデータ：バックアップファイルからロード {0}", BackupFilePath);
				}
				catch
				{
					Save();
					Debug.LogFormat("セーブデータ：新規作成 {0}", FilePath);
				}
			}
		}

		public void Delete()
		{
			var dirPath = Path.GetDirectoryName(FilePath);
			RecursionDelete(dirPath);
		}

		public bool IsLoadSucess()
		{
			return m_IsLoadSucess;
		}

		private void SaveFile(FileStream fileStream, string json)
		{
			try
			{
				byte[] data = Encoding.UTF8.GetBytes(json);
#if !UNITY_EDITOR
				// 圧縮 => 暗号化
				data = Compressor.Compress(data);
				data = Encryptor.Encrypt(data);
#endif
				fileStream.Write(data, 0, data.Length);
			}
			catch
			{
			}

			fileStream.Flush();
			fileStream.Close();
		}

		private void LoadFile(string filePath)
		{
			// 読み込み
			FileInfo fileInfo = new FileInfo(filePath);
			byte[] data = null;
			using (FileStream fileStream = fileInfo.OpenRead())
			{
				data = new byte[fileStream.Length];
				fileStream.Read(data, 0, data.Length);
			}

#if !UNITY_EDITOR
			// 復号化 => 解凍
			data = Encryptor.Decrypt(data);
			data = Compressor.Decompress(data);
#endif
			// JSON 化して上書き
			string json = Encoding.UTF8.GetString(data);
			JsonUtility.FromJsonOverwrite(json, this);
		}

		/// <summary>
		/// 指定したディレクトリとその中身を全て削除する
		/// </summary>
		private void RecursionDelete(string targetDirPath)
		{
			if (!Directory.Exists(targetDirPath))
			{
				return;
			}

			//ディレクトリ以外の全ファイルを削除
			string[] filePaths = Directory.GetFiles(targetDirPath);
			foreach (string filePath in filePaths)
			{
				File.SetAttributes(filePath, FileAttributes.Normal);
				File.Delete(filePath);
			}

			//ディレクトリの中のディレクトリも再帰的に削除
			string[] dirPaths = Directory.GetDirectories(targetDirPath);
			foreach (string dirPath in dirPaths)
			{
				RecursionDelete(dirPath);
			}

			// 中が空になったらディレクトリ自身も削除
			Directory.Delete(targetDirPath, false);
		}

		// ----------------------------------------------------------------
		// Enum/Const
		// ----------------------------------------------------------------
		private const string SaveDataDirName = "SaveData";

		// ----------------------------------------------------------------
		// Property
		// ----------------------------------------------------------------
		private string FilePath
		{
			get
			{
#if UNITY_EDITOR
				return m_FilePath;
#else
				// 実機なら [Application.persistentDataPath] に
				return Path.Combine(Application.persistentDataPath, m_FilePath);
#endif
			}
		}

		private string BackupFilePath
		{
			get
			{
#if UNITY_EDITOR
				return m_BackupFilePath;
#else
				// 実機なら [Application.persistentDataPath] に
				return Path.Combine(Application.persistentDataPath, m_BackupFilePath);
#endif
			}
		}

		// ----------------------------------------------------------------
		// Field
		// ----------------------------------------------------------------
		private string m_FilePath = default;
		private string m_BackupFilePath = default;
		private bool m_IsLoadSucess = true;
	}
} // Fwk.SaveData
