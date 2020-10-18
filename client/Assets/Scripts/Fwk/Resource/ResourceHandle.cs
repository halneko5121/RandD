/**
 * 概要：
 */
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Fwk.Resource
{
	public class ResourceHandle
	{
		// ----------------------------------------------------------------
		// Method
		// ----------------------------------------------------------------
		public ResourceHandle(int id, string key, Type systemType)
		{
			m_Id = id;
			m_Key = key;
			m_SystemType = systemType;
		}

		// ----------------------------------------------------------------
		// Property
		// ----------------------------------------------------------------
		public int Id
		{
			get { return m_Id; }
		}

		public string Key
		{
			get { return m_Key; }
		}

		public bool IsSceneObject
		{
			get { return m_SystemType == typeof(Scene); }
		}

		public AsyncOperationHandle<IList<UnityEngine.Object>> HandleAsssets
		{
			get { return m_HandleAsssets; }
			set { m_HandleAsssets = value; }
		}

		public AsyncOperationHandle<SceneInstance> HandleScene
		{
			get { return m_HandleScene; }
			set { m_HandleScene = value; }
		}

		public float Progress
		{
			get
			{
				if (m_SystemType == typeof(Scene))
				{
					if (!m_HandleScene.IsValid())
					{
						return 0.0f;
					}
					else
					{
						return m_HandleScene.PercentComplete;
					}
				}
				else
				{
					if (!m_HandleAsssets.IsValid())
					{
						return 0.0f;
					}
					else
					{
						return m_HandleAsssets.PercentComplete;
					}
				}
			}
		}

		public bool IsDone
		{
			get
			{
				if (m_SystemType == typeof(Scene))
				{
					if (!m_HandleScene.IsValid())
					{
						return false;
					}
					else
					{
						return m_HandleScene.IsDone;
					}
				}
				else
				{
					if (!m_HandleAsssets.IsValid())
					{
						return false;
					}
					else
					{
						return m_HandleAsssets.IsDone;
					}
				}
			}
		}

		// ----------------------------------------------------------------
		// Field
		// ----------------------------------------------------------------
		int m_Id = int.MinValue;
		string m_Key = default(string);
		Type m_SystemType = typeof(UnityEngine.Object);
		AsyncOperationHandle<IList<UnityEngine.Object>> m_HandleAsssets;
		AsyncOperationHandle<SceneInstance> m_HandleScene;
	}
} // Fwk.Resource
