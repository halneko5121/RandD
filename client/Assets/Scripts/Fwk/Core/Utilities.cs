using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Fwk.Core
{
	public static class Utilities
	{
		// ----------------------------------------------------------------
		// Method
		// ----------------------------------------------------------------
		public static void CreateArray<T>(out T[] t, int n) where T : class, new()
		{
			t = new T[n];
			for (int i = 0; i < t.Length; ++i)
			{
				t[i] = new T();
			}
		}

		public static void CreateRectangularArray<T>(out T[,] t, int n1, int n2) where T : class, new()
		{
			t = new T[n1, n2];
			for (int i = 0; i < t.GetLength(0); ++i)
			{
				for (int j = 0; j < t.GetLength(1); ++j)
				{
					t[i, j] = new T();
				}
			}
		}

		public static void CreateJaggedArray<T>(out T[][] t, int n1, int n2) where T : class, new()
		{
			t = new T[n1][];
			for (int i = 0; i < t.Length; ++i)
			{
				t[i] = new T[n2];
				for (int j = 0; j < t[i].Length; ++j)
				{
					t[i][j] = new T();
				}
			}
		}

		public static void Destroy(Object obj)
		{
			if (IsNull(obj))
			{
				return;
			}

			if (Application.isPlaying)
			{
				if (obj is GameObject)
				{
					var go = obj as GameObject;
					go.transform.SetParent(null, false);
				}
				Object.Destroy(obj);
			}
			else
			{
				Object.DestroyImmediate(obj);
			}
		}

		public static void DestroyMaterials(ref MeshRenderer meshRenderer)
		{
			foreach (var material in meshRenderer.materials)
			{
				Object.DestroyImmediate(material);
			}
		}

		public static void DestroyMesh(ref MeshCollider meshCollider)
		{
			if (meshCollider.sharedMesh != null)
			{
				meshCollider.sharedMesh.Clear(false);
				Object.DestroyImmediate(meshCollider.sharedMesh);
				meshCollider.sharedMesh = null;
			}
		}

		public static void DestroyMesh(ref MeshFilter meshFilter)
		{
			if (meshFilter.sharedMesh != null)
			{
				meshFilter.sharedMesh.Clear(false);
				Object.DestroyImmediate(meshFilter.sharedMesh);
				meshFilter.sharedMesh = null;
			}
		}

		public static void SafeClear<T1, T2>(ref Dictionary<T1, T2> dictionary)
		{
			if (dictionary != null)
			{
				dictionary.Clear();
				dictionary = null;
			}
		}

		public static void SafeClear<T>(ref HashSet<T> hashSet)
		{
			if (hashSet != null)
			{
				hashSet.Clear();
				hashSet = null;
			}
		}

		public static void SafeClear<T>(ref List<T> list)
		{
			if (list != null)
			{
				list.Clear();
				list = null;
			}
		}

		public static void SafeClear<T>(ref Queue<T> queue)
		{
			if (queue != null)
			{
				queue.Clear();
				queue = null;
			}
		}

		public static void SafeClear<T>(ref Stack<T> stack)
		{
			if (stack != null)
			{
				stack.Clear();
				stack = null;
			}
		}

		public static void SafeDestroy(ref GameObject gameObject)
		{
			if (IsNull(gameObject))
			{
				return;
			}

			Object.Destroy(gameObject);
			gameObject = null;
		}

		public static void SafeDestroy<T>(ref T component) where T : Component
		{
			if (IsNull(component))
			{
				return;
			}

			Object.Destroy(component.gameObject);
			component = null;
		}

		public static bool IsNull(object obj)
		{
			if (obj is Object)
			{
				if ((Object)obj != null)
				{
					Log("UnityEngine.Object [" + obj + "] is not null.");
					return false;
				}
				else
				{
					return true;
				}
			}
			else
			{
				if (obj != null)
				{
					Log("System.Object [" + obj + "] is not null.");
					return false;
				}
				else
				{
					return true;
				}
			}
		}

		public static bool IsNotNull(object obj)
		{
			return !IsNull(obj);
		}

		/*
		public static T SetUI<T>(GameObject prefab, string rootName)
		{
			T t = default(T);

			//var root = UIRoot.instance.FindRoot( rootName );
			var root = UISystem.UIOrganizer.instance.GetLayer();
			prefab.transform.SetParent(root.transform);
			t = prefab.GetComponent<T>();

			return t;
		}
		*/

		public static bool IsPrefab(GameObject go)
		{
			return go.transform.parent == null; // todo:暫定。正しい方法ではない
		}

		public static void SetLayerAll(Transform transform, int layer)
		{
			transform.gameObject.layer = layer;

			for (int i = 0, l = transform.childCount; i < l; ++i)
			{
				SetLayerAll(transform.GetChild(i), layer);
			}
		}

		public static void SetLayerAll<T>(Transform transform, int layer) where T : Component
		{
			var t = transform.GetComponent<T>();
			if (t != null)
			{
				t.gameObject.layer = layer;
			}

			for (int i = 0, l = transform.childCount; i < l; ++i)
			{
				SetLayerAll<T>(transform.GetChild(i), layer);
			}
		}

		public static void ChangeHideInHierarchy(GameObject go, bool hide)
		{
			if (go == null)
			{
				return;
			}

			if (hide)
			{
				go.hideFlags |= HideFlags.HideInHierarchy;
			}
			else
			{
				go.hideFlags &= ~HideFlags.HideInHierarchy;
			}
		}

		public static bool IsPointerOverEventSystem(int fingerId)
		{
			var eventSystem = EventSystem.current;
			if (eventSystem != null)
			{
#if UNITY_IOS || UNITY_ANDROID
				if( eventSystem.IsPointerOverGameObject( fingerId ) )
				{
					return true;
				}
#else
				if (eventSystem.IsPointerOverGameObject())
				{
					return true;
				}
#endif
			}
			return false;
		}

		public static void AddPointerClickListener(GameObject go, UnityAction<BaseEventData> call)
		{
			var trigger = go.GetComponent<EventTrigger>();
			if (trigger == null)
			{
				trigger = go.AddComponent<EventTrigger>();
			}

			var entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerClick;
			entry.callback.AddListener(call);
			trigger.triggers.Clear();
			trigger.triggers.Add(entry);
		}

		public static bool IsEqualVector3(Vector3 lhs, Vector3 rhs)
		{
			return Mathf.Approximately(lhs.x, rhs.x)
				&& Mathf.Approximately(lhs.y, rhs.y)
				&& Mathf.Approximately(lhs.z, rhs.z);
		}

		public static bool IsZeroVector3(Vector3 v)
		{
			return IsEqualVector3(v, Vector3.zero);
		}

		/*
		 * 線分と平面の交点を求める
		 * linePoint1 : 線分の点
		 * linePoint2 : 線分の点
		 * planeNormal : 平面の法線ベクトル
		 * planePoint : 平面上の任意の点
		 * intersection : 交点
		 */
		public static bool GetLineSegmentAndPlaneIntersection(Vector3 linePoint1, Vector3 linePoint2, Vector3 planeNormal, Vector3 planePoint, out Vector3 intersection)
		{
			intersection = Vector3.zero;
			var pa = linePoint1 - planePoint;
			var pb = linePoint2 - planePoint;
			var dotA = Vector3.Dot(pa, planeNormal);
			var dotB = Vector3.Dot(pb, planeNormal);

			// 内積が0の場合交点がなく、直線が平面に含まれる
			var minDot = 0.000001f;
			if (Mathf.Abs(dotA) < minDot)
			{
				dotA = 0.0f;
			}
			if (Mathf.Abs(dotB) < minDot)
			{
				dotB = 0.0f;
			}

			if ((Mathf.Approximately(dotA, 0.0f) && Mathf.Approximately(dotB, 0.0f)) || (dotA * dotB > 0.0f))
			{
				// 交差していない
				return false;
			}

			var ab = linePoint2 - linePoint1;
			var ratio = Mathf.Abs(dotA) / (Mathf.Abs(dotA) + Mathf.Abs(dotB));
			intersection = linePoint1 + ab * ratio;
			return true;
		}

		[Conditional("FWK_UTILITIES_DEBUG")]
		private static void Log(object message)
		{
			UnityEngine.Debug.Log(message);
		}

		[Conditional("FWK_UTILITIES_DEBUG")]
		private static void LogFormat(string format, params object[] args)
		{
			UnityEngine.Debug.LogFormat(format, args);
		}
	}
} // Fwk.Core
