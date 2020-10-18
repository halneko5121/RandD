using UnityEngine;

namespace Fwk.Math
{
	public abstract class BaseLine : ILine
	{
		// ----------------------------------------------------------------
		// Method
		// ----------------------------------------------------------------
		public BaseLine()
		{
			m_NodeDistances = new float[m_NonuniformAccuracy + 1];
		}

		public void RegisterNodes(Vector3[] nodes)
		{
			OnRegisterNodes(nodes);
			CalculateDistances();
		}

		public abstract Vector3 Evaluate(float rate);

		public virtual Vector3 EvaluateNonuniform(out bool isEnd, float speed, float elapsedTime)
		{
			isEnd = false;

			var t = 0.0f;
			var elapsedLength = elapsedTime * speed;
			if (elapsedLength <= 0)
			{
				t = 0.0f;
			}
			else if (elapsedLength >= m_Length)
			{
				t = 1.0f;
				isEnd = true;
			}
			else
			{
				t = CalculateTFromLength(elapsedLength);
			}
			return Evaluate(t);
		}

		protected abstract void OnRegisterNodes(Vector3[] nodes);

		private void CalculateDistances()
		{
			m_Length = 0.0f;

			var divT = 1.0f / m_NonuniformAccuracy;
			var prevPoint = Evaluate(0);

			for (int i = 1; i <= m_NonuniformAccuracy; ++i)
			{
				var nextPoint = Evaluate(divT * i);
				var distance = (nextPoint - prevPoint).magnitude;
				m_Length += distance;
				m_NodeDistances[i] = m_Length;

				prevPoint = nextPoint;
			}
		}

		private float CalculateTFromLength(float elapsedLength)
		{
			if (elapsedLength <= 0.0f)
			{
				return 0.0f;
			}
			else if (elapsedLength >= m_Length)
			{
				return 1.0f;
			}

			var targetLength = elapsedLength;
			var index = BinarySearch(m_NodeDistances, targetLength);
			var diff = targetLength - m_NodeDistances[index];
			var localLength = m_NodeDistances[index + 1] - m_NodeDistances[index];
			if ((diff < 0.5f) && (index > 0))
			{
				localLength = Mathf.Lerp(m_NodeDistances[index] - m_NodeDistances[index - 1], localLength, diff * 2.0f);
			}
			else if ((diff > 0.5f) && (index < m_NonuniformAccuracy - 1))
			{
				localLength = Mathf.Lerp(localLength, m_NodeDistances[index + 2] - m_NodeDistances[index + 1], diff * 2.0f);
			}

			var localRate = diff / localLength;
			var t = (index + localRate) / m_NonuniformAccuracy;

			return t;
		}

		private static int BinarySearch(float[] list, float target)
		{
			var low = 0;
			var high = list.Length - 1;
			var index = 0;

			while (low < high)
			{
				index = low + (int)((high - low) * 0.5f);
				if (target > list[index])
				{
					low = index + 1;
				}
				else
				{
					high = index;
				}
			}

			return index;
		}

		// ----------------------------------------------------------------
		// Property
		// ----------------------------------------------------------------
		public float Length
		{
			get
			{
				return m_Length;
			}
		}

		public int NonuniformAccuracy
		{
			get
			{
				return m_NonuniformAccuracy;
			}
			set
			{
				value = Mathf.Max(0, value);
				if (m_NonuniformAccuracy != value)
				{
					m_NonuniformAccuracy = value;
					m_NodeDistances = new float[m_NonuniformAccuracy + 1];
					CalculateDistances();
				}
			}
		}

		// ----------------------------------------------------------------
		// Field
		// ----------------------------------------------------------------
		private float m_Length = 0.0f;
		private float[] m_NodeDistances = null;
		private int m_NonuniformAccuracy = 20;
	}
} // Fwk.Math
