/**
 * 概要：スクリーン管理
 */
using System;
using UnityEngine;

namespace Fwk.Core
{
	public class ScreenManager : SingletonBehaviour<ScreenManager>
	{
		// ----------------------------------------------------------------
		// Method
		// ----------------------------------------------------------------
		public void InitResolution(int baseHeight)
		{
			OriginalWidth = Screen.width;
			OriginalHeight = Screen.height;

			SetResolutionWithBaseHeight(baseHeight);
		}

		// 解像度を変更する
		public void SetResolutionWithBaseHeight(int baseHeight, float scale = 1.0f)
		{
			if (RefHeight != baseHeight)
			{
				RefHeight = baseHeight;
				SetResolution(scale);
			}
		}

		public void SetResolution(float scale = 1.0f)
		{
			ConvertRate = Mathf.Min((float)RefHeight / (float)OriginalHeight, 1.0f);
			Width = (int)(OriginalWidth * ConvertRate);
			Height = (int)(OriginalHeight * ConvertRate);
			if (Mathf.Approximately(scale, 1.0f))
			{
				Screen.SetResolution(Width, Height, true);
			}
			else
			{
				Width = (int)(Width * scale);
				Height = (int)(Height * scale);
				Screen.SetResolution(Width, Height, true);
			}
#if DIAGNOSTICS
			DebugSystem.DebugToast.SharedInstance().PopupMessageWithLog("Set Resolution w : " + width + " h : " + height);
#endif
			if (m_ResolutionChangedCallback != null)
			{
				m_ResolutionChangedCallback(Width, Height);
			}
		}

		public void RegisterResolutionChangedCallback(Action<int, int> func)
		{
			m_ResolutionChangedCallback += func;
		}

		// ----------------------------------------------------------------
		// property
		// ----------------------------------------------------------------
		public int OriginalWidth
		{
			get; private set;
		}
		public int OriginalHeight
		{
			get; private set;
		}

		public int Width			// 幅
		{
			get; private set;
		}
		public int Height			// 高さ
		{
			get; private set;
		}
		public int RefHeight		// ベースとなる高さ
		{
			get; private set;
		}
		public float ConvertRate	// 変換割合
		{
			get; private set;
		}

		//---------------------------------------------------------
		// Field
		//---------------------------------------------------------
		private Action<int, int> m_ResolutionChangedCallback;

	}
}
