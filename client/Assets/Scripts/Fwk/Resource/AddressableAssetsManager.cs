/**
 * 概要：
 */
using Fwk.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace Fwk.Resource
{
	public class AddressableAssetsManager
		: SingletonBehaviour<AddressableAssetsManager>
		, IDisposable
	{
		// ----------------------------------------------------------------
		// Method
		// ----------------------------------------------------------------
		public ResourceHandle LoadAsync<TObject>(string key, out AddressableAssetsLoader loader)
			where TObject : UnityEngine.Object
		{
			// 既に読み込み済み
			if (m_ResourceMapList.ContainsKey(key))
			{
				loader = null;
				return null;
			}

			var handle = m_Loader.LoadAsync<TObject>(key, (objs) => m_ResourceMapList.Add(key, objs.Select(v => v as UnityEngine.Object).ToList()));
			loader = m_Loader;
			return handle;
		}

		public ResourceHandle LoadSceneAsync<TObject>(string key, out AddressableAssetsLoader loader, Action<Scene> callback)
			where TObject : UnityEngine.Object
		{
			var handle = m_Loader.LoadSceneAsync<TObject>(key, callback);
			loader = m_Loader;
			return handle;
		}

		public void Unload(ResourceHandle handle)
		{
			if (handle == null)
			{
				return;
			}

			// 登録されてない
			if (!m_ResourceMapList.ContainsKey(handle.Key))
			{
				return;
			}

			m_Loader.Unload(handle);
			m_ResourceMapList.Remove(handle.Key);
		}

		/// <summary>
		/// シングルモードで「Addressables.LoadSceneAsync()」してるので
		/// これを呼ぶ必要はないはず。
		/// </summary>
		public void UnloadSceneAsync(ResourceHandle handle, Action onFinish)
		{
			if (handle == null)
			{
				return;
			}

			m_Loader.UnloadSceneAsync(handle, onFinish);
		}

		public void UnloadAll()
		{
			m_Loader.UnloadAll();
			m_ResourceMapList.Clear();
		}

		public UnityEngine.Object GetAsset(string addressKey)
		{
			IList<UnityEngine.Object> results = null;
			if (m_ResourceMapList.TryGetValue(addressKey, out results))
			{
				return results.FirstOrDefault();
			}

			return null;
		}

		public UnityEngine.Object GetAsset(string labelKey, string addressKay)
		{
			IList<UnityEngine.Object> results = null;
			if (m_ResourceMapList.TryGetValue(labelKey, out results))
			{
				return results.FirstOrDefault(v => v.name == addressKay);
			}

			return null;
		}

		public IList<UnityEngine.Object> GetAssets(string labelKey)
		{
			IList<UnityEngine.Object> results = null;
			if (m_ResourceMapList.TryGetValue(labelKey, out results))
			{
				return results;
			}

			Assert.IsTrue(false, $"指定のアセットラベルは見つかりませんでした => {labelKey}");
			return null;
		}

		public void Dispose()
		{
			UnloadAll();
		}

		// ----------------------------------------------------------------
		// Field
		// ----------------------------------------------------------------
		Dictionary<string, IList<UnityEngine.Object>> m_ResourceMapList = new Dictionary<string, IList<UnityEngine.Object>>();
		AddressableAssetsLoader m_Loader = new AddressableAssetsLoader();
	}
} // Fwk.Resource
