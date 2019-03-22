using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステート進行
/// </summary>
public class StateMachine
{
	//! 開始ステート（引数にするとObservableに怒られるのでしょうがなくメンバに)
	StateBase _start_state = null;

	/// <summary>
	/// 開始ステートを指定
	/// </summary>
	/// <param name="start_state"></param>
	public StateMachine( StateBase start_state )
	{
		_start_state = start_state;
	}

	/// <summary>
	/// ステートを実行
	/// </summary>
	/// <param name="start_state"></param>
	/// <returns></returns>
	public IEnumerator Process()
	{
		StateBase current_state = _start_state;
		while ( current_state != null )
		{
			Debug.Log($"[StateMachine]:{current_state.ToString()} OnStart");
			current_state.OnStart();

			Debug.Log( $"[StateMachine]:{current_state.ToString()} OnProcess" );
			StateBase next_state = null;
			yield return current_state.OnProcess( result_state => next_state = result_state );

			Debug.Log( $"[StateMachine]:{current_state.ToString()} OnFinish" );
			current_state.OnFinish();

			current_state = next_state;
		}
		yield break;
	}

}
