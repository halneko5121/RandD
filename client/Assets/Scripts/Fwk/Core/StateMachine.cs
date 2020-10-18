/**
 * 概要：ステートマシン
 */
using System.Collections.Generic;

namespace Fwk.Core
{
	public sealed class StateMachine<T>
	{
		// ----------------------------------------------------------------
		// Method
		// ----------------------------------------------------------------
		/// ステートを追加します
		public void Add(T key, System.Action enterAct = null, System.Action updateAct = null, System.Action exitAct = null)
		{
			m_StateTable.Add(key, new State(enterAct, updateAct, exitAct));
		}

		/// 現在のステートを設定します
		public void ChangeState(T key)
		{
			NextState = key;

			if (m_CurrentState != null)
			{
				m_CurrentState.Exit();
			}

			NextState = default(T);

			OldState = CurrentState;

			m_CurrentState = m_StateTable[key];

			OnStateChanged?.Invoke(CurrentState, key);

			CurrentState = key;

			m_EnterAction?.Invoke();

			m_CurrentState.Enter();
		}

		/// 現在のステートを更新します
		public void Update()
		{
			if (m_CurrentState == null)
			{
				return;
			}

			m_CurrentState.Update();
		}

		/// ステートの終了処理
		public void Exit()
		{
			if (m_CurrentState == null)
			{
				return;
			}

			m_CurrentState.Exit();

			Clear();
		}

		/// すべてのステートを削除します
		public void Clear()
		{
			m_StateTable.Clear();

			m_CurrentState = null;
		}

		/// Enterに入る前に呼ばれるコールバックを設定します
		public void SetEnterAction(System.Action action)
		{
			m_EnterAction = action;
		}

		// ----------------------------------------------------------------
		// Property
		// ----------------------------------------------------------------
		public T CurrentState { get; private set; }
		public T OldState { get; private set; }
		public T NextState { get; private set; }
		public System.Action<T, T> OnStateChanged { get; set; }

		// ----------------------------------------------------------------
		// Field
		// ----------------------------------------------------------------
		private Dictionary<T, State> m_StateTable = new Dictionary<T, State>(); // ステートのテーブル
		private State m_CurrentState = null;     // 現在のステート
		private System.Action m_EnterAction = null;

		// ----------------------------------------------------------------
		// Class
		// ----------------------------------------------------------------
		/// ステート
		private class State
		{
			/// コンストラクタ
			public State(System.Action enterAct = null, System.Action updateAct = null, System.Action exitAct = null)
			{
				m_EnterAct = enterAct ?? delegate {};
				m_UpdateAct = updateAct ?? delegate {};
				m_ExitAct = exitAct ?? delegate {};
			}

			/// 開始します
			public void Enter()
			{
				m_EnterAct();
			}

			/// 更新します
			public void Update()
			{
				m_UpdateAct();
			}

			/// 終了します
			public void Exit()
			{
				m_ExitAct();
			}

			private readonly System.Action m_EnterAct;  // 初期化時に呼び出されるデリゲート
			private readonly System.Action m_UpdateAct; // 更新時に呼び出されるデリゲート
			private readonly System.Action m_ExitAct;   // 終了時に呼び出されるデリゲート
		}
	}
} // Fwk.Core
