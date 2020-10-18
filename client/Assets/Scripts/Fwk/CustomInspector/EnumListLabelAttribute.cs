/**
 * 概要：Enum のフィールド名を上書きできる
 * (C) Copyright 2017 Urahimono Project Inc.
 * 参考は以下サイト
 * https://www.urablog.xyz/entry/2017/02/12/165706
 * https://gist.github.com/urahimono/a75c81e5ecb248699cb90caed6eb5fc5
 */
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif // UNITY_EDITOR

namespace Fwk.DebugSystem.CustomInspector
{
	public class EnumListLabelAttribute : PropertyAttribute
	{
		public System.Type m_enumType  = null;

		public EnumListLabelAttribute( System.Type i_type )
		{
			m_enumType  = i_type;
		}

	} // class EnumListLabelAttribute

#if UNITY_EDITOR
	[CustomPropertyDrawer( typeof( EnumListLabelAttribute ) )]
	public class EnumListLabelDrawer : PropertyDrawer
	{
		public override void OnGUI( Rect i_position, SerializedProperty i_property, GUIContent i_label )
		{
			int     index   = GetElementIndex( i_property );
			string enumName = GetEnumName( index );
			if( !string.IsNullOrEmpty( enumName ) )
			{
				i_label.text    = enumName;
			}
			EditorGUI.PropertyField( i_position, i_property, i_label, true );
		}


		public override float GetPropertyHeight( SerializedProperty property, GUIContent label )
		{
			return EditorGUI.GetPropertyHeight( property );
		}


		private new EnumListLabelAttribute attribute
		{
			get
			{
				return (EnumListLabelAttribute)base.attribute;
			}
		}

		private int GetElementIndex( SerializedProperty i_property )
		{
			string propertyPath = i_property.propertyPath;

			var propertys   = propertyPath.Split( '.' );

			// このデータが配列内の要素ならば、「aaaa.Array.data[...]」の形になるはずだ！
			if( propertys.Length < 3 )
			{
				return -1;
			}

			// クラスを経由して、パスが長くなった場合でも、このデータが配列内の要素ならば、その後ろから二番目は「Array」になるはずだ！
			string arrayProperty = propertys[ propertys.Length-2 ];
			if( arrayProperty != "Array" )
			{
				return -1;
			}

			// このデータが配列内の要素ならば、data[...]の形になっているはずだ！
			var paths       = propertyPath.Split( '.' );
			var lastPath    = paths[ propertys.Length - 1 ];
			if( !lastPath.StartsWith( "data[" ) )
			{
				return -1;
			}

			// 数字の要素だけ抜き出すんだ！
			var regex       = new System.Text.RegularExpressions.Regex( @"[^0-9]" );
			var countText   = regex.Replace( lastPath, "" );
			int index       = 0;
			if( !int.TryParse( countText, out index ) )
			{
				return -1;
			}

			return index;
		}

		private string GetEnumName( int i_index )
		{
			if( i_index < 0 )
			{
				return null;
			}

			if( System.Enum.IsDefined( attribute.m_enumType, i_index ) )
			{
				return System.Enum.GetName( attribute.m_enumType, i_index );
			}

			return null;
		}

	} // class EnumListLabelDrawer
#endif // UNITY_EDITOR

} // namespace Fwk.DebugSystem.CustomInspector

