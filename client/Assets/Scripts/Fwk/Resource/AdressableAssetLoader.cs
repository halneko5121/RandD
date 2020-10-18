/**
 * 概要：
 */
using Fwk.Core;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.SceneManagement;

namespace Fwk.Resource
{
	public sealed class AddressableAssetsLoader
		: Singleton<AddressableAssetsLoader>
		, IDisposable
	{
		public ResourceHandle LoadAsync<T>(string key, Action<IList<UnityEngine.Object>> callback)
		{
			var handle = new ResourceHandle(GenerateHandleId(), key, typeof(T));
			Assert.IsTrue(!handle.IsSceneObject);
			m_HandleList.Add(handle);

			// キーが存在するかチェックした上でロード
			var locationHandle = Addressables.LoadResourceLocationsAsync(key);
			m_ResourceLocationHandleDict.Add(key, locationHandle);
			locationHandle.Completed += op =>
			{
				// キーが存在するか
				var isExists = op.Status == AsyncOperationStatus.Succeeded && op.Result != null && 1 <= op.Result.Count;
				if (isExists)
				{
					// ロード
					handle.HandleAsssets = Addressables.LoadAssetsAsync<UnityEngine.Object>(key, null);
					handle.HandleAsssets.Completed += op2 =>
					{
						if (op2.Status == AsyncOperationStatus.Succeeded)
						{
#if UNITY_EDITOR
							Fwk.DebugSystem.Log.DebugUseResourceLog.Instance.Append("key:" + key);
							foreach (var obj in op2.Result)
							{
								Fwk.DebugSystem.Log.DebugUseResourceLog.Instance.Append("- " + obj.name);
							}
#endif
							callback(op2.Result);
						}
						else
						{
							m_HandleList.Remove(handle);
							Debug.LogFormat("Status[{0}]: key => {1}", op2.Status, handle.Key);
						}

						if (op2.OperationException != null)
						{
							Debug.LogWarningFormat("Load is null => {0}", op2.OperationException.ToString());
						}
					};
				}
				else
				{
					m_HandleList.Remove(handle);
					Debug.LogFormat("[key => {0}] 指定キーのリソースがない可能性があります", handle.Key);
					Debug.Log("実機でのみここに入ってくる場合、Addressable Group のスキーマ設定を見直して下さい");
				}

				// ResourceLocationHandle の後片付け
				AsyncOperationHandle<IList<IResourceLocation>> resultHandle;
				m_ResourceLocationHandleDict.TryGetValue(key, out resultHandle);
				m_ResourceLocationHandleDict.Remove(key);
				Addressables.Release(resultHandle);
			};

			return handle;
		}

		public ResourceHandle LoadSceneAsync<T>(string addressKey, Action<Scene> callback)
		{
			// @note:シーンのハンドルは保持しない
			var handle = new ResourceHandle(GenerateHandleId(), addressKey, typeof(Scene));

			handle.HandleScene = Addressables.LoadSceneAsync(addressKey);
			handle.HandleScene.Completed += op =>
			{
				if (op.Status == AsyncOperationStatus.Succeeded)
				{
#if UNITY_EDITOR
					Fwk.DebugSystem.Log.DebugUseResourceLog.Instance.Append("key:" + addressKey);
#endif
					callback(op.Result.Scene);
				}
				else
				{
					Debug.LogFormat("Status[{0}]: key => {1}", op.Status, handle.Key);
				}

				if (op.OperationException != null)
				{
					Debug.LogWarningFormat("LoadScene is null => {0}", op.OperationException.ToString());
				}
			};

			return handle;
		}

		public void Unload(ResourceHandle handle)
		{
			// 登録されてない
			if (!m_HandleList.Contains(handle))
			{
				return;
			}

			LoadCancel(handle);

			if (handle.IsSceneObject)
			{
				Assert.IsTrue(false, "UnloadScene() を呼んで下さい");
			}
			else
			{
				Addressables.Release(handle.HandleAsssets);
			}
		}

		public void UnloadSceneAsync(ResourceHandle handle, Action onFinish)
		{
			if (!handle.IsSceneObject)
			{
				Assert.IsTrue(false, "Unload() を呼んで下さい");
			}
			else
			{
				Addressables.UnloadSceneAsync(handle.HandleScene).Completed += op =>
				{
					onFinish?.Call();
				};
			}
		}

		public void UnloadAll()
		{
			ResourceHandle[] handleArray = m_HandleList.ToArray();

			for (int i = 0; i < handleArray.Length; i++)
			{
				ResourceHandle handle = handleArray[i];

				if (handle.IsSceneObject)
				{
					UnloadSceneAsync(handle, null);
				}
				else
				{
					Unload(handle);
				}
				handleArray[i] = null;
			}
		}

		public void LoadCancel(ResourceHandle handle)
		{
			// 登録されてない
			if (!m_HandleList.Contains(handle))
			{
				return;
			}

			// 除外
			m_HandleList.Remove(handle);
		}

		public void Dispose()
		{
			UnloadAll();
		}

		private int GenerateHandleId()
		{
			if (m_GenerateHandleId == int.MaxValue)
			{
				m_GenerateHandleId = 0;
			}
			else
			{
				m_GenerateHandleId++;
			}

			return m_GenerateHandleId;
		}

		// ----------------------------------------------------------------
		// Property
		// ----------------------------------------------------------------
		public bool IsDone
		{
			get
			{
				bool isDone = true;
				foreach (var handle in m_HandleList)
				{
					isDone = handle.IsDone;
				}
				return isDone;
			}
		}

		// ----------------------------------------------------------------
		// Field
		// ----------------------------------------------------------------
		List<ResourceHandle> m_HandleList = new List<ResourceHandle>();
		Dictionary<string, AsyncOperationHandle<IList<IResourceLocation>>> m_ResourceLocationHandleDict = new Dictionary<string, AsyncOperationHandle<IList<IResourceLocation>>>();
		int m_GenerateHandleId = int.MaxValue;
	}
} // Fwk.Resource
