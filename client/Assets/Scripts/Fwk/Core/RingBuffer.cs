/**
 * 概要：循環バッファ（最大容量付き）
 * 参考：https://takap-tech.com/entry/2018/01/02/044400
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Fwk.Core
{
	public class RingBuffer<T> : IEnumerable<T>
	{
		// ----------------------------------------------------------------
		// Method
		// ----------------------------------------------------------------
		/// <summary>
		/// リングバッファーの最大要素数をしていしてオブジェクトを初期化します。
		/// </summary>
		public RingBuffer(int maxCapacity)
		{
			this.MaxCapacity = maxCapacity;
			m_Queue = new Queue<T>(maxCapacity);
		}

		/// <summary>
		/// 要素をクリアする
		/// </summary>
		public void Clear()
		{
			m_Queue.Clear();
		}

		/// <summary>
		/// 指定した要素をリングバッファーに追加します。
		/// </summary>
		public void Add(T item)
		{
			m_Queue.Enqueue(item);
			if (m_Queue.Count > this.MaxCapacity)
			{
				T removed = this.Pop();

				// デバッグ用の出力:
				// Console.WriteLine($"キャパシティを超えているためバッファの先頭データ破棄しました。{removed}");
			}
		}

		public void AddRangeWithCapacity(IEnumerable<T> collection)
		{
			foreach (var item in collection)
			{
				if (this.MaxCapacity <= m_Queue.Count)
				{
					break;
				}
				m_Queue.Enqueue(item);
			}
		}

		/// <summary>
		/// 末尾の要素を取得しバッファーからデータを削除します。
		/// </summary>
		public T Pop() => m_Queue.Dequeue();

		/// <summary>
		/// バッファーの先頭の要素を取得します。データは削除されません。
		/// </summary>
		public T First() => m_Queue.Peek();

		/// <summary>
		/// 指定した要素が存在するかどうかを確認します。
		/// </summary>
		public bool Contains(T item) => m_Queue.Contains(item);

		/// <summary>
		/// このオブジェクトが管理中の全データを配列として取得します。
		/// </summary>
		public T[] ToArray() => m_Queue.ToArray();

		/// <summary>
		/// 現在のリングバッファー内の要素を列挙します。
		/// （Linqによるより高度な操作をえるようにこのメソッドを定義しておく）
		/// </summary>
		public IEnumerator<T> GetEnumerator() => m_Queue.GetEnumerator();

		// IEnumerator の明示的な実装
		IEnumerator IEnumerable.GetEnumerator() => m_Queue.GetEnumerator();

		// ----------------------------------------------------------------
		// Operators
		// ----------------------------------------------------------------
		/// <summary>
		/// 指定した位置の要素を参照します。
		/// </summary>
		public T this[int index]
		{
			get
			{
				if (index < 0 || index > this.Count)
					throw new ArgumentOutOfRangeException(nameof(index), $"{nameof(index)}={index}");
				return m_Queue.ElementAt(index);
			}
		}

		// ----------------------------------------------------------------
		// Property
		// ----------------------------------------------------------------
		/// <summary>
		/// バッファーに格納されている要素の数を取得します。
		/// </summary>
		public int Count => this.m_Queue.Count;

		/// <summary>
		/// このバッファーの最大容量を取得します。
		/// </summary>
		public int MaxCapacity { get; private set; }

		// ----------------------------------------------------------------
		// Field
		// ----------------------------------------------------------------
		private readonly Queue<T> m_Queue; // データを実際に格納するオブジェクト
	}
} // Fwk.Core
