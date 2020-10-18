/**
 * 概要：シングルトン
 */
namespace Fwk.Core
{
	public class Singleton<T> where T : class, new()
	{
		// ----------------------------------------------------------------
		// Property
		// ----------------------------------------------------------------
		public static T Instance
		{
			get
			{
				// ダブルチェック ロッキング アプローチ
				if (s_Instance == null)
				{
					lock (m_SyncRoot)
					{
						if (s_Instance == null)
						{
							s_Instance = new T();
						}
					}
				}
				return s_Instance;
			}
		}

		// ----------------------------------------------------------------
		// Field
		// ----------------------------------------------------------------
		private static volatile T s_Instance = default(T);
		private static object m_SyncRoot = new object();
	}
} // Fwk.Core
