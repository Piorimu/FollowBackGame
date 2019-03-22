using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace GameMain
{
	/// <summary>
	/// ゲーム開始時処理
	/// </summary>
	public class State_Start : StateBase
	{
		public override IEnumerator OnProcess( Action<StateBase> next )
		{
			next( new State_WaitFollow() );
			yield break;
		}
	}

}