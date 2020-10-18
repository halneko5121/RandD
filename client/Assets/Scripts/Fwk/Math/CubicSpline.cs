using UnityEngine;

namespace Fwk.Math
{
	public sealed class CubicSpline : BaseLine
	{
		// ----------------------------------------------------------------
		// Method
		// ----------------------------------------------------------------
		public override Vector3 Evaluate(float rate)
		{
			if (m_NodeNum == 0)
			{
				return Vector3.zero;
			}

			rate = Mathf.Clamp01(rate);

			rate *= m_NodeNum - 1;

			int j;
			float dt;

			j = Mathf.FloorToInt(rate);
			if (j >= m_NodeNum)
			{
				j = m_NodeNum - 1;
			}
			dt = rate - j;

			return m_As[j] + (m_Bs[j] + (m_Cs[j] + m_Ds[j] * dt) * dt) * dt;
		}

		protected override void OnRegisterNodes(Vector3[] nodes)
		{
			m_NodeNum = nodes.Length;
			if (m_NodeNum == 0)
			{
				return;
			}

			var maxIndex = nodes.Length - 1;

			m_As = new Vector3[nodes.Length];
			m_Bs = new Vector3[nodes.Length];
			m_Cs = new Vector3[nodes.Length];
			m_Ds = new Vector3[nodes.Length];
			var w = new Vector3[nodes.Length];

			// 3次多項式の0次係数(a)を設定
			for (var i = 0; i < nodes.Length; ++i)
			{
				m_As[i] = nodes[i];
			}

			// 3次多項式の2次係数(c)を計算
			m_Cs[0] = m_Cs[maxIndex] = Vector3.zero;
			// 2次係数(c)の中間データを計算
			for (int i = 1; i < maxIndex; ++i)
			{
				m_Cs[i] = 3.0f * (m_As[i - 1] - 2.0f * m_As[i] + m_As[i + 1]);
			}

			w[0] = Vector3.zero;
			for (int i = 1; i < nodes.Length; ++i)
			{
				var temp = new Vector3(4, 4, 4) - w[i - 1];
				m_Cs[i] = (m_Cs[i] - m_Cs[i - 1]);
				m_Cs[i] = new Vector3(m_Cs[i].x / temp.x, m_Cs[i].y / temp.y, m_Cs[i].z / temp.z);
				w[i] = new Vector3(1 / temp.x, 1 / temp.y, 1 / temp.z);
			}

			for (int i = maxIndex - 1; i > 0; --i)
			{
				var currentC = m_Cs[i];
				var nextC = m_Cs[i + 1];
				m_Cs[i] = currentC - (new Vector3(nextC.x * w[i].x, nextC.y * w[i].y, nextC.z * w[i].z));
			}

			// 3次多項式の1次係数(b)と3次係数(b)を計算
			m_Bs[maxIndex] = m_Ds[maxIndex] = Vector3.zero;
			for (int i = 0; i < maxIndex; i++)
			{
				m_Ds[i] = (m_Cs[i + 1] - m_Cs[i]) / 3;
				m_Bs[i] = m_As[i + 1] - m_As[i] - m_Cs[i] - m_Ds[i];
			}
		}

		// ----------------------------------------------------------------
		// Field
		// ----------------------------------------------------------------
		private Vector3[] m_As = null;
		private Vector3[] m_Bs = null;
		private Vector3[] m_Cs = null;
		private Vector3[] m_Ds = null;

		private int m_NodeNum = 0;
	}
} // Fwk.Math
