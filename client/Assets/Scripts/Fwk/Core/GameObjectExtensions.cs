using System.Linq;
using UnityEngine;

namespace Fwk.Core
{
	public static class GameObjectExtensions
	{
		// ----------------------------------------------------------------
		// Method
		// ----------------------------------------------------------------
		public static T[] GetComponentsInChildrenWithoutSelf<T>(this GameObject self) where T : Component
		{
			return self.GetComponentsInChildren<T>()
				.Where((t) => t.gameObject != self)
				.ToArray();
		}

		public static T SafeAddComponent<T>(this GameObject self) where T : Component
		{
			T comp = self.GetComponent<T>();
			if (comp != null)
			{
				return comp;
			}

			return self.AddComponent<T>();
		}

		// コンポーネントを削除する
		public static void RemoveComponent<T>(this GameObject self) where T : Component
		{
			GameObject.Destroy(self.GetComponent<T>());
		}

		// コンポーネントを削除する
		public static void SafeRemoveComponent<T>(this GameObject self) where T : Component
		{
			T comp = self.GetComponent<T>();
			if (comp == null)
			{
				return;
			}

			GameObject.Destroy(self.GetComponent<T>());
		}

		// 現在のactive状態を判定して違っていたらSetActive()を呼ぶ
		public static void SetActiveFast(this GameObject self, bool active)
		{
			if( self.activeSelf != active )
			{
				self.gameObject.SetActive( active );
//				Fwk.Debug.LogFormat("SetActiveFast:({0}){1}",self.name,active);
			}
		}
	}
} // Fwk.Core
