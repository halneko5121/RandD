using System;
using System.Collections.Generic;

namespace Fwk.Core
{
	/// <summary>
	/// valueが変化したら登録してあるコールバックが発火する
	/// UniRxのReactivePropertyみたいなやつ
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ReactiveField<T>
	{
		public ReactiveField( T initialValue, int capacity = 5 )
		{
			this.value = initialValue;
			m_ObserveFuncList = new List<System.Action<T>>( capacity );
		}

		public void AddObserve( System.Action<T> observe )
		{
			if( Utilities.IsNull( observe ) ) { return;}

			if( !m_ObserveFuncList.Contains( observe ) )
			{
				m_ObserveFuncList.Add( observe );
			}
		}

		public void Release()
		{
			for( int i = 0; i < m_ObserveFuncList.Count; i++ )
			{
				m_ObserveFuncList[ i ] = null;
			}
			m_ObserveFuncList = null;
		}

		public void FireAllCallBack()
		{
			foreach( var action in m_ObserveFuncList )
			{
				action.Call( this.value );
			}
		}

		private T value;

		public T Value
		{
			get
			{
				return value;
			}
			set
			{
				// 同じ値だったらイベント呼ばない
				if( EqualityComparer<T>.Default.Equals( this.value, value ) ) { return; }
				this.value = value;
				FireAllCallBack();
			}
		}

		private List<System.Action<T>> m_ObserveFuncList;
	}
}
