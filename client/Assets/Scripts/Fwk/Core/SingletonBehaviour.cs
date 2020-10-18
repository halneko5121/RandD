/**
 * 概要：シングルトンMonoBehaviour
 */
using System.Diagnostics;
using UnityEngine;

namespace Fwk.Core
{
	public abstract class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
	{
		public static bool IsExistInstance
		{
			get { return (!Utilities.IsNull(Instance)); }
		}


		// ----------------------------------------------------------------
		// MonoBehaviour
		// ----------------------------------------------------------------
		protected virtual void Awake()
		{
			Log(name + ".Awake()");
			Create();
		}

		protected virtual void OnDestroy()
		{
			Log(name + ".OnDestroy()");
			Destroy();
		}

		// ----------------------------------------------------------------
		// Method
		// ----------------------------------------------------------------
		protected virtual void OnCreateSingleton()
		{
		}

		protected virtual void OnDestroySingleton()
		{
		}

		private void Create()
		{
			if (s_Instance == null)
			{
				if (this is T)
				{
					s_Instance = this as T;
					OnCreateSingleton();
				}
				else
				{
					UnityEngine.Debug.LogWarning(typeof(T) + ":シングルトンの型がおかしい");
					Destroy(gameObject);
				}
			}
			else
			{
				UnityEngine.Debug.LogWarning(typeof(T) + ":既にインスタンスが存在している");
				Destroy(gameObject);
			}
		}

		private void Destroy()
		{
			if (s_Instance == this)
			{
				OnDestroySingleton();
				s_Instance = null;
			}
		}

		[Conditional("SINGLETON_BEHAVIOUR_DEBUG")]
		private static void Log(object message)
		{
			UnityEngine.Debug.Log(message);
		}

		[Conditional("SINGLETON_BEHAVIOUR_DEBUG")]
		private static void LogFormat(string format, params object[] args)
		{
			UnityEngine.Debug.LogFormat(format, args);
		}

		// ----------------------------------------------------------------
		// Property
		// ----------------------------------------------------------------
		public static T Instance
		{
			get
			{
				return s_Instance;
			}
		}

		// ----------------------------------------------------------------
		// Field
		// ----------------------------------------------------------------
		private static T s_Instance = default(T);
	}
} // Fwk.Core
