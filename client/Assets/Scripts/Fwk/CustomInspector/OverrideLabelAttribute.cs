/**
 * 概要：これを[SerializeField]で使うと、フィールド名を上書きできる
 */
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif // UNITY_EDITOR

namespace Fwk.DebugSystem.CustomInspector
{
	public class OverrideLabelAttribute : PropertyAttribute
	{
		public readonly string Value;

		public OverrideLabelAttribute(string value)
		{
			Value = value;
		}
	}

#if UNITY_EDITOR
	[CustomPropertyDrawer(typeof(OverrideLabelAttribute))]
	public class OverrideLabelDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			var newLabel = attribute as OverrideLabelAttribute;
			EditorGUI.PropertyField(position, property, new GUIContent(newLabel.Value), true);
		}
	}
#endif // UNITY_EDITOR
}
