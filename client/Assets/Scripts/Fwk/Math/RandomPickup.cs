using System.Collections.Generic;
using UnityEngine;

namespace Fwk.Math
{
	public static class RandomPickup
	{
		// ----------------------------------------------------------------
		// Method
		// ----------------------------------------------------------------
		public static int GetRandomIndex(int[] rates)
		{
			var thresholds = new int[rates.Length];

			var maxRate = 0;
			for (int i = 0; i < thresholds.Length; ++i)
			{
				maxRate += rates[i];
				thresholds[i] = maxRate;
			}

			var rate = Random.Range(0, maxRate);

			var index = 0;
			for (; index < thresholds.Length; ++index)
			{
				if (rate < thresholds[index])
				{
					break;
				}
			}
			return index;
		}

		public static int GetRandomIndex(Item[] rates)
		{
			var thresholds = new int[rates.Length];

			var maxRate = 0;
			for (int i = 0; i < thresholds.Length; ++i)
			{
				maxRate += rates[i].Rate;
				thresholds[i] = maxRate;
			}

			var rate = Random.Range(0, maxRate);

			var index = 0;
			for (; index < thresholds.Length; ++index)
			{
				if (rate < thresholds[index])
				{
					break;
				}
			}
			return index;
		}

		public static int GetRandomIndex(List<int> rates)
		{
			var thresholds = new int[rates.Count];

			var maxRate = 0;
			for (int i = 0; i < thresholds.Length; ++i)
			{
				maxRate += rates[i];
				thresholds[i] = maxRate;
			}

			var rate = Random.Range(0, maxRate);

			var index = 0;
			for (; index < thresholds.Length; ++index)
			{
				if (rate < thresholds[index])
				{
					break;
				}
			}
			return index;
		}

		// -----------------------------------------
		// ※ GC Alloc 削減版
		// -----------------------------------------
		public static int GetRandomIndex<T>(ref List<int> thresholds, List<T> list, Dictionary<T, int> dictionary)
		{
			if (thresholds == null)
			{
				thresholds = new List<int>(list.Count);
			}
			else
			{
				thresholds.Clear();
			}

			var maxRate = 0;
			foreach (var key in list)
			{
				maxRate += dictionary[key];
				thresholds.Add(maxRate);
			}

			var rate = Random.Range(0, maxRate);

			var index = 0;
			for (; index < thresholds.Count; ++index)
			{
				if (rate < thresholds[index])
				{
					break;
				}
			}
			return index;
		}

		public static int GetRandomIndex<T>(ref List<int> thresholds, List<T> list, System.Func<T, int> callback)
		{
			if (thresholds == null)
			{
				thresholds = new List<int>(list.Count);
			}
			else
			{
				thresholds.Clear();
			}

			var maxRate = 0;
			foreach (var key in list)
			{
				maxRate += callback.Invoke(key);
				thresholds.Add(maxRate);
			}

			var rate = Random.Range(0, maxRate);

			var index = 0;
			for (; index < thresholds.Count; ++index)
			{
				if (rate < thresholds[index])
				{
					break;
				}
			}
			return index;
		}

		// ----------------------------------------------------------------
		// Interface
		// ----------------------------------------------------------------
		public interface Item
		{
			int Rate
			{
				get;
			}
		}
	}
} // Fwk.Math
