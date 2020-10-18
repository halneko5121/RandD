/**
 * 概要：使用したリソースのログ出力
 */
#if UNITY_EDITOR
using System.IO;
using System.Text;
using Fwk.Core;
using UnityEngine;

namespace Fwk.DebugSystem.Log
{
	public sealed class DebugUseResourceLog : Singleton<DebugUseResourceLog>
	{
		// ----------------------------------------------------------------
		// Method
		// ----------------------------------------------------------------
		public void Initialize()
		{
			if( m_SB == null )
			{
				m_SB = new StringBuilder();
			}
			m_SB.Clear();
		}

		public void Append( string line )
		{
			if( m_SB != null )
			{
				m_SB.AppendLine( line );
			}
		}

		public void Save()
		{
			if( m_SB != null )
			{
				if( m_SB.Length > 0 )
				{
					string filepath = Path.Combine( Application.dataPath, OutputPath );
					var dirPath = Path.GetDirectoryName(filepath);

					if (!Directory.Exists(dirPath))
					{
						Directory.CreateDirectory(dirPath);
					}

					File.WriteAllText( filepath, m_SB.ToString(), Encoding.UTF8 );
					Debug.Log($"UseResourceList => {filepath}");
				}
			}
		}

		// ----------------------------------------------------------------
		// Enum/Const
		// ----------------------------------------------------------------
		private const string OutputPath = "UseResourceList/UseResourceList.txt";

		// ----------------------------------------------------------------
		// Field
		// ----------------------------------------------------------------
		private StringBuilder m_SB = null;
	}
} // Fwk.DebugSystem.Log
#endif
