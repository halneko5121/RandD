/**
 * 概要：時間管理
 */
using System;
using UnityEngine;
#if UNITY_EDITOR && FWK_DEBUG
using UnityEditor;
#endif

namespace Fwk.Core
{
	public class TimeManager : SingletonBehaviour<TimeManager>
	{
		public static void ChangeGameSpeedOfShader(float speed)
		{
			Shader.SetGlobalFloat("_Global_GameSpeed", speed);
		}

		// ----------------------------------------------------------------
		// MonoBehaviour
		// ----------------------------------------------------------------
		// 更新
		void Update()
		{
			Real = UnityEngine.Time.realtimeSinceStartup - m_LastRealTime;
			m_LastRealTime = UnityEngine.Time.realtimeSinceStartup;

			// サーバー時間更新
			m_ServerTime += (long)(Real*1000.0f);
		}

		//---------------------------------------------------------
		// Method
		//---------------------------------------------------------
		// ポーズ解除
		public void PauseBreak()
		{
			if (m_IsPause == true && m_DisablePause == false)
			{
				Scale = m_PausePushTimeScale;
			}
			m_IsPause = false;
		}

		// ポーズ開始
		public void Pause()
		{
			if (m_IsPause == false && m_DisablePause == false)
			{
				m_PausePushTimeScale = Scale;
				Scale = 0.0f;
				m_IsPause = true;
			}
			else
			{
				//Dbg.Warning("ポーズ中にポーズが再度呼ばれました!!!");
			}
		}

		// ポーズ中?
		public bool IsPause()
		{
			return m_IsPause;
		}

		// FrameRate変更
		public void ChangeTargetFrameRate(int _targetFrameRate = 60)
		{
			Application.targetFrameRate = _targetFrameRate;
			m_LastRealTime = UnityEngine.Time.realtimeSinceStartup;
		}

		// UnixTime用
		// TimeZone : UTC固定
		public static long ToUnixTime( DateTime dateTime )
		{
			double nowTicks = (dateTime.ToUniversalTime() - UnixEpoch).TotalSeconds;
			return (long)nowTicks;
		}

		public static long ToUnixTimeMillisecond( DateTime dateTime )
		{
			return ( long )dateTime.ToUniversalTime().Subtract( UnixEpoch ).TotalMilliseconds;
		}

		public static DateTime ToDateTimeUTC( long unixTime )
		{
			long set_time = unixTime/1000;
			DateTime utc = UnixEpoch.AddSeconds( set_time ).ToUniversalTime();
			return utc;
		}

		// 日本時間( UTC + 9時間)
		public static DateTime ToDateTimeJST( long unixTime )
		{
			long set_time = unixTime/1000;
			DateTime utc = UnixEpoch.AddSeconds( set_time ).ToUniversalTime();
			DateTime jst = utc.AddHours(9);
			return jst;
		}

		// ----------------------------------------------------------------
		// Property
		// ----------------------------------------------------------------
		// デルタ
		public float Delta
		{
			get
			{
				return UnityEngine.Time.deltaTime;
			}
		}

		// アンスケールドデルタ
		public float UnscaledDelta
		{
			get
			{
				return UnityEngine.Time.unscaledDeltaTime;
			}
		}

		// フィクスドデルタ
		public float FixedDelta
		{
			get
			{
				return UnityEngine.Time.fixedDeltaTime;
			}
		}

		// リアル
		public float Real
		{
			get; private set;
		}

		// タイムスケール
		public float Scale
		{
			get
			{
				return UnityEngine.Time.timeScale;
			}
			set
			{
				UnityEngine.Time.timeScale = value;
			}
		}

		// サーバー時間
		public long ServerTime
		{
			get
			{
				return m_ServerTime;
			}
			set
			{
				m_ServerTime = value;
			}
		}

		//---------------------------------------------------------
		// Field
		//---------------------------------------------------------
		private float m_PausePushTimeScale = 1.0f;
		private bool m_IsPause = false;
		private bool m_DisablePause = false;
		private float m_LastRealTime;
		private long m_ServerTime = 0;

		// UnixTime用
		public static readonly DateTime UnixEpoch = new DateTime( 1970 , 1 , 1 , 0 , 0 , 0 , DateTimeKind.Utc );
	}


#if UNITY_EDITOR && FWK_DEBUG
	// ----------------------------------------------------------------
	// Editorテスト表示部分
	// ----------------------------------------------------------------
	[CustomEditor(typeof(TimeManager))]
	public class TimeManagerDebugDisp : Editor
	{
		// ----------------------------------------------------------------
		// Method
		// ----------------------------------------------------------------
		public override void OnInspectorGUI()
		{
			if( Application.isPlaying )
			{
				EditorGUILayout.LongField("サーバー時間(生)",TimeManager.Instance.ServerTime);

				DateTime utc = TimeManager.ToDateTimeUTC(TimeManager.Instance.ServerTime);
				string utc_time = String.Format("{0}年{1}月{2}日{3}時{4}分{5}秒",utc.Year,utc.Month,utc.Day,utc.Hour,utc.Minute,utc.Second);
				EditorGUILayout.TextField("サーバー時間(UTC)",utc_time);

				DateTime jst = TimeManager.ToDateTimeJST(TimeManager.Instance.ServerTime);
				string jst_time = String.Format("{0}年{1}月{2}日{3}時{4}分{5}秒",jst.Year,jst.Month,jst.Day,jst.Hour,jst.Minute,jst.Second);
				EditorGUILayout.TextField("サーバー時間(JST)",jst_time);
			}
		}
	}
#endif
}
