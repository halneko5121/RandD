/**
 * 概要：ストップウォッチログ出力
 */
using Fwk.Core;
using UnityEngine;

namespace Fwk.DebugSystem.Log
{
	public class DebugStopWatchLog : Singleton<DebugStopWatchLog>
	{
		// ----------------------------------------------------------------
		// Method
		// ----------------------------------------------------------------
		public void Initialize()
		{
#if FWK_DEBUG
			for (int i = 0; i < StopWatchMax; i++)
			{
				m_StopWatch[i] = new System.Diagnostics.Stopwatch();
			}
#endif
		}

		public void Start(int index)
		{
#if FWK_DEBUG
			Debug.AssertFormat(index >= 0 && index <= StopWatchMax, "indexが異常値です。 index = {0}", index);
			m_StopWatch[index].Start();
#endif
		}
		public void End(int index, string logName)
		{
#if FWK_DEBUG
			Debug.AssertFormat(index >= 0 && index <= StopWatchMax, "indexが異常値です。 index = {0}", index);

			m_StopWatch[index].Stop();
			var log = "[StopWatchTime]" + logName + ":" + m_StopWatch[index].ElapsedMilliseconds + "ms";
			UnityEngine.Debug.Log(log);
#endif
		}

		// ----------------------------------------------------------------
		// Enum/Const
		// ----------------------------------------------------------------
		private const int StopWatchMax = 40;

		// ----------------------------------------------------------------
		// Field
		// ----------------------------------------------------------------
		private static System.Diagnostics.Stopwatch[] m_StopWatch = new System.Diagnostics.Stopwatch[StopWatchMax];
	}
} // Fwk.DebugSystem.Log
