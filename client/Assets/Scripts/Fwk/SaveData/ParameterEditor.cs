/**
 * 概要：外部ファイルを使用したパラメータ保存クラス(Editorでしか保存できない)
 *		 このクラスの派生クラスは、メンバ変数がJsonに変換され
 *		 クラス名と同じファイル名で保存される。
 */
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Fwk.SaveData
{
#if UNITY_EDITOR
	public class ParameterEditor : Editor
	{
		// ----------------------------------------------------------------
		// Method（継承用）
		// ----------------------------------------------------------------
		protected void IntField(string name, ref int intValue)
		{
			var labelWidth = EditorGUIUtility.labelWidth;

			EditorGUIUtility.labelWidth = DefaultLabelWidth;
			intValue = EditorGUILayout.IntField(name, intValue, GUILayout.Width(SetFieldLabelWidth));
			EditorGUIUtility.labelWidth = labelWidth;
		}

		protected void IntField(string name, ref int intValue, int min ,int max)
		{
			intValue = EditorGUILayout.IntSlider(name, intValue, min, max);
		}

		protected void FloatField(string name, ref float floatValue)
		{
			var labelWidth = EditorGUIUtility.labelWidth;

			EditorGUIUtility.labelWidth = DefaultLabelWidth;
			floatValue = EditorGUILayout.FloatField(name, floatValue, GUILayout.Width(SetFieldLabelWidth));
			EditorGUIUtility.labelWidth = labelWidth;
		}

		protected void FloatField(string name, ref float floatValue, float min, float max)
		{
			floatValue = EditorGUILayout.Slider(name, floatValue, min, max);
		}

		protected void Vector2Field(string name, ref Vector2 vector)
		{
			vector = EditorGUILayout.Vector2Field(name, vector);
		}

		protected void Vector3Field(string name, ref Vector3 vector)
		{
			vector = EditorGUILayout.Vector3Field(name, vector);
		}

		protected void CurveField(string name, AnimationCurve curve)
		{
			curve = EditorGUILayout.CurveField(name, curve);
		}

		protected void GradientField(string name, Gradient gradient)
		{
			gradient = EditorGUILayout.GradientField(name, gradient);
		}

		protected void BoolField(string name, ref bool boolValue)
		{
			boolValue = EditorGUILayout.Toggle(name, boolValue);
		}

		protected void TextField(string name, ref string stringValue)
		{
			stringValue = EditorGUILayout.TextField(name, stringValue);
		}

		protected void LabelField(string labelName)
		{
			EditorGUILayout.LabelField(labelName);
		}

		protected void EnumField<T>(string name, ref T t) where T : System.Enum
		{
			t = (T)EditorGUILayout.EnumPopup(name, t);
		}

		protected void ColorField(string name, ref Color colorValue)
		{
			colorValue = EditorGUILayout.ColorField(name, colorValue);
		}

		// ----------------------------------------------------------------
		// Enum/Const
		// ----------------------------------------------------------------
		private const float DefaultLabelWidth = 350.0f;
		private const float SetFieldLabelWidth = 450.0f;
	}
#endif // UNITY_EDITOR
} // Fwk.SaveData
