/**
 * 概要：フェード
 */
using Fwk.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Fwk.Graphics
{
	public sealed class Fade : SingletonBehaviour<Fade>
	{
		// ----------------------------------------------------------------
		// MonoBehaviour
		// ----------------------------------------------------------------
		void Update()
		{
			if (m_IsPlaying)
			{
				m_CurrentTime = Mathf.Clamp(m_CurrentTime + TimeManager.Instance.Real * m_Sign, 0.0f, m_FadeTime);

				SetAlpha(m_CurrentTime / m_FadeTime);

				if (false
					|| (m_Sign < 0.0f && m_CurrentTime <= 0.0f)
					|| (m_Sign > 0.0f && m_FadeTime <= m_CurrentTime)
					)
				{
					m_IsPlaying = false;
					m_FinishAction.Call();
				}
			}
		}

		// ----------------------------------------------------------------
		// Method
		// ----------------------------------------------------------------
		// フェードインする
		public void In(float fadeTime, System.Action onFinish = null)
		{
			if (fadeTime <= 0.0f)
			{
				// 0秒以下は即フェード
				fadeTime = 0.0f;
				m_IsPlaying = false;
				SetAlpha(0.0f);
			} else {
				m_IsPlaying = true;
			}

			m_FadeTime = fadeTime;
			m_Sign = -1.0f;
			m_IsPlaying = true;
			m_FinishAction = onFinish;
			m_CurrentTime = m_FadeTime;
		}

		// フェードアウトする
		public void Out(float fadeTime, System.Action onFinish = null)
		{
			if (fadeTime <= 0.0f)
			{
				// 0秒以下は即フェード
				fadeTime = 0.0f;
				m_IsPlaying = false;
				SetAlpha(1.0f);
			} else {
				m_IsPlaying = true;
			}

			m_FadeTime = fadeTime;
			m_Sign = 1.0f;
			m_FinishAction = onFinish;
			m_CurrentTime = 0.0f;
		}

		// フェードインしきっているか？
		public bool IsIn()
		{
			return (m_Image.color.a <= 0.0f);
		}

		// フェードアウトしきっているか？
		public bool IsOut()
		{
			return (m_Image.color.a >= 1.0f);
		}

		// アルファを設定する
		private void SetAlpha(float alpha)
		{
			var color = m_FadeColor;
			color.a = Mathf.Clamp01(alpha);
			m_Image.color = color;
			m_Image.enabled = (color.a > 0.0f);
		}

		protected override void OnCreateSingleton()
		{
			m_Image.color = m_FadeColor;
			if (m_FadeInStart)
			{
				m_CurrentTime = 0.0f;
				SetAlpha(0.0f);
			}
			else
			{
				m_CurrentTime = m_FadeTime;
				SetAlpha(1.0f);
			}
		}

		protected override void OnDestroySingleton()
		{
		}

		// ----------------------------------------------------------------
		// Property
		// ----------------------------------------------------------------
		public bool IsPlaying
		{
			get
			{
				return m_IsPlaying;
			}
		}

		public Color FadeColor
		{
			get
			{
				return m_FadeColor;
			}
			set
			{
				m_FadeColor = value;
			}
		}

		public float FadeTime
		{
			get
			{
				return m_FadeTime;
			}
		}

		public bool IsRaycastTarget
		{
			get
			{
				return m_Image.raycastTarget;
			}
			set
			{
				m_Image.raycastTarget = value;
			}
		}

		// ----------------------------------------------------------------
		// Field
		// ----------------------------------------------------------------
		[SerializeField]
		private Image m_Image = null;
		[SerializeField]
		private Color m_FadeColor = Color.white;
		[SerializeField]
		private bool m_FadeInStart = false;		// 起動時にフェードアウト状態ではじめるか

		private float m_FadeTime = 0.0f;
		private System.Action m_FinishAction = null;
		private float m_CurrentTime = 0.0f;
		private float m_Sign = 0.0f;
		private bool m_IsPlaying = false;
	}
} // Fwk.Graphics
