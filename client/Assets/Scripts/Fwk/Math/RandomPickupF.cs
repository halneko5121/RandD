using System.Collections.Generic;
using UnityEngine;

namespace Fwk.Math
{
	public static class RandomPickupF
	{
		// ----------------------------------------------------------------
		// Method
		// ----------------------------------------------------------------
		public static int GetRandomIndex(float[] rates)
		{
			var index = 0;

			var maxRate = 0.0f;
			var thresholds = new float[rates.Length];
			for (int i = 0; i < thresholds.Length; ++i)
			{
				maxRate += rates[i];
				thresholds[i] = maxRate;
			}
			var rate = Random.Range(0, maxRate);
			for (; index < thresholds.Length; ++index)
			{
				if (rate < thresholds[index])
				{
					break;
				}
			}
			thresholds = null;

			return index;
		}

		public static int GetRandomIndex(List<float> rates)
		{
			var index = 0;

			var maxRate = 0.0f;
			var thresholds = new float[rates.Count];
			for (int i = 0; i < thresholds.Length; ++i)
			{
				maxRate += rates[i];
				thresholds[i] = maxRate;
			}
			var rate = Random.Range(0, maxRate);
			for (; index < thresholds.Length; ++index)
			{
				if (rate < thresholds[index])
				{
					break;
				}
			}
			thresholds = null;

			return index;
		}

		public static int GetRandomIndex(Item[] rates)
		{
			var index = 0;

			var maxRate = 0.0f;
			var rateArray = new float[rates.Length];
			for (int i = 0; i < rateArray.Length; ++i)
			{
				maxRate += rates[i].Rate;
				rateArray[i] = maxRate;
			}
			var rate = Random.Range(0, maxRate);
			for (; index < rateArray.Length; ++index)
			{
				if (rate < rateArray[index])
				{
					break;
				}
			}
			rateArray = null;

			return index;
		}

		// ----------------------------------------------------------------
		// Interface
		// ----------------------------------------------------------------
		public interface Item
		{
			float Rate
			{
				get;
			}
		}
	}
} // Fwk.Math
