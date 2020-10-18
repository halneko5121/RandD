using UnityEngine;

namespace Fwk.Math
{
	public interface ILine
	{
		// ----------------------------------------------------------------
		// Method
		// ----------------------------------------------------------------
		void RegisterNodes(Vector3[] nodes);
		Vector3 Evaluate(float rate);
		Vector3 EvaluateNonuniform(out bool isEnd, float speed, float elapsedTime);

		// ----------------------------------------------------------------
		// Property
		// ----------------------------------------------------------------
		float Length
		{
			get;
		}

		int NonuniformAccuracy
		{
			get;
			set;
		}
	}
} // Fwk.Math
