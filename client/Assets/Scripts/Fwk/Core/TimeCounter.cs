/**
 * 概要：タイマー管理
 */
namespace Fwk.Core
{
	public sealed class TimeCounter
	{
		// ----------------------------------------------------------------
		// Method
		// ----------------------------------------------------------------
		// リセットする
		public void Reset()
		{
			IsUpdate = false;
			m_CurrentTime = 0.0f;
			m_IsMoment = false;
			m_StartTime = 0.0f;
			m_IsUnscaledDeltaTime = false;
			m_IsStart = false;
		}

		// 時間を設定する
		public void SetStartTime(float time, bool isPause = false)
		{
			m_StartTime = time;
			m_IsMoment = false;
			IsUpdate = false;

			if (!isPause)
			{
				Start();
			}
		}

		// UnscaledDeltaTimeに切り替える
		public void SetUnscaledDeltaTime(bool enable)
		{
			m_IsUnscaledDeltaTime = enable;
		}

		// 開始する
		public void Start()
		{
			if (m_StartTime > 0.0f)
			{
				m_CurrentTime = m_StartTime;
				m_IsMoment = false;
				IsUpdate = true;
				m_IsStart = true;
			}
			else
			{
				Stop();
			}
		}

		// 停止する
		public void Stop()
		{
			m_CurrentTime = 0.0f;
			m_IsMoment = false;
			IsUpdate = false;
			m_IsStart = false;
		}

		// ポーズ
		public void Pause( bool isPause )
		{
			IsUpdate = !isPause;
		}

		// 設定した時間がタイムアウトしているか？
		// ※Startしていなくてもtrueが返る
		public bool IsTimeOut()
		{
			// 1回でもスタートしている
			return m_CurrentTime <= 0.0f;
		}

		// 設定した時間がタイムアウトしているか？
		// ※一度でもStartしていないとtrueにならない
		public bool IsTimeOutByStarted()
		{
			// 1回でもスタートしている
			return (m_IsStart == true) &&  m_CurrentTime <= 0.0f;
		}

		// 設定した時間がタイムアウトした瞬間か？
		public bool IsTimeOutMoment()
		{
			return IsTimeOut() && m_IsMoment;
		}

		// 残り時間を取得する
		public float GetDecTime()
		{
			return m_CurrentTime;
		}

		// 経過時間を取得する
		public float GetAddTime()
		{
			return m_StartTime - m_CurrentTime;
		}

		// 経過時間割合を取得する(0.0f → 1.0f)
		public float GetAddTimeRate()
		{
			if (m_StartTime == 0.0f) return 0.0f;

			return (m_StartTime - m_CurrentTime) / m_StartTime;
		}

		// 経過時間割合を取得する(1.0f → 0.0f)
		public float GetDecTimeRate()
		{
			return 1.0f - GetAddTimeRate();
		}

		// 更新する
		public void Update()
		{
			m_IsMoment = false;

			if (!IsUpdate)
			{
				return;
			}

			if (m_CurrentTime > 0.0f)
			{
				if (m_IsUnscaledDeltaTime)
				{
					m_CurrentTime -= TimeManager.Instance.UnscaledDelta;
				}
				else
				{
					m_CurrentTime -= TimeManager.Instance.Delta;
				}

				if (m_CurrentTime <= 0.0f)
				{
					m_CurrentTime = 0.0f;
					m_IsMoment = true;
					IsUpdate = false;
				}
			}
			else
			{
				m_CurrentTime = 0.0f;
			}
		}

		// ----------------------------------------------------------------
		// Property
		// ----------------------------------------------------------------
		public bool IsUpdate
		{
			get; private set;
		}

		public bool IsStart
		{
			get{ return m_IsStart; }
		}

		// ----------------------------------------------------------------
		// Field
		// ----------------------------------------------------------------
		bool m_IsMoment = false;
		bool m_IsUnscaledDeltaTime = false;
		float m_CurrentTime = 0.0f;
		float m_StartTime = 0.0f;
		bool m_IsStart = false;
	}
} // Fwk.Core
