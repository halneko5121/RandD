using System.Collections;
using UnityEngine;

namespace Fwk.Core
{
	public static class MonoBehaviourExtensions
	{
		// ----------------------------------------------------------------
		// Method
		// ----------------------------------------------------------------
		public static IEnumerator CallAfterWaitForSeconds(this MonoBehaviour self, float seconds, System.Action action)
		{
			yield return new WaitForSeconds(seconds);
			action.Call();
		}

		public static IEnumerator CallAfterWaitForSeconds<T>(this MonoBehaviour self, float seconds, System.Action<T> action, T arg)
		{
			yield return new WaitForSeconds(seconds);
			action.Call(arg);
		}

		public static IEnumerator CallAfterWaitForSeconds<T1, T2>(this MonoBehaviour self, float seconds, System.Action<T1, T2> action, T1 arg1, T2 arg2)
		{
			yield return new WaitForSeconds(seconds);
			action.Call(arg1, arg2);
		}

		public static IEnumerator CallAfterWaitForSeconds<T1, T2, T3>(this MonoBehaviour self, float seconds, System.Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3)
		{
			yield return new WaitForSeconds(seconds);
			action.Call(arg1, arg2, arg3);
		}

		public static IEnumerator CallAfterWaitForSeconds<T1, T2, T3, T4>(this MonoBehaviour self, float seconds, System.Action<T1, T2, T3, T4> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
			yield return new WaitForSeconds(seconds);
			action.Call(arg1, arg2, arg3, arg4);
		}

		public static IEnumerator CallAfterWaitForSecondsRealtime( this MonoBehaviour self, float seconds, System.Action action )
		{
			yield return new WaitForSecondsRealtime( seconds );
			action.Call();
		}

		public static IEnumerator CallAfterWaitForSecondsRealtime<T>( this MonoBehaviour self, float seconds, System.Action<T> action, T arg )
		{
			yield return new WaitForSecondsRealtime( seconds );
			action.Call( arg );
		}

		public static IEnumerator CallAfterWaitForSecondsRealtime<T1, T2>( this MonoBehaviour self, float seconds, System.Action<T1, T2> action, T1 arg1, T2 arg2 )
		{
			yield return new WaitForSecondsRealtime( seconds );
			action.Call( arg1, arg2 );
		}

		public static IEnumerator CallAfterWaitForSecondsRealtime<T1, T2, T3>( this MonoBehaviour self, float seconds, System.Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3 )
		{
			yield return new WaitForSecondsRealtime( seconds );
			action.Call( arg1, arg2, arg3 );
		}

		public static IEnumerator CallAfterWaitForSecondsRealtime<T1, T2, T3, T4>( this MonoBehaviour self, float seconds, System.Action<T1, T2, T3, T4> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4 )
		{
			yield return new WaitForSecondsRealtime( seconds );
			action.Call( arg1, arg2, arg3, arg4 );
		}

		public static IEnumerator CallAfterWaitForFixedUpdate(this MonoBehaviour self, System.Action action)
		{
			yield return new WaitForFixedUpdate();
			action.Call();
		}

		public static IEnumerator CallAfterWaitForFixedUpdate<T>(this MonoBehaviour self, System.Action<T> action, T arg)
		{
			yield return new WaitForFixedUpdate();
			action.Call(arg);
		}

		public static IEnumerator CallAfterWaitForFixedUpdate<T1, T2>(this MonoBehaviour self, System.Action<T1, T2> action, T1 arg1, T2 arg2)
		{
			yield return new WaitForFixedUpdate();
			action.Call(arg1, arg2);
		}

		public static IEnumerator CallAfterWaitForFixedUpdate<T1, T2, T3>(this MonoBehaviour self, System.Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3)
		{
			yield return new WaitForFixedUpdate();
			action.Call(arg1, arg2, arg3);
		}

		public static IEnumerator CallAfterWaitForFixedUpdate<T1, T2, T3, T4>(this MonoBehaviour self, System.Action<T1, T2, T3, T4> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
			yield return new WaitForFixedUpdate();
			action.Call(arg1, arg2, arg3, arg4);
		}
	}
} // Fwk.Core
