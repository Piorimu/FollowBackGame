using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace GameMain
{
	public class State_ProcessFollow : StateBase
	{
		//! フォローするアカウント
		Account _follow_account = null;

		/// <summary>
		/// フォローするアカウント
		/// </summary>
		/// <param name="follow_account"></param>
		public State_ProcessFollow( Account follow_account )
		{
			_follow_account = follow_account;
		}

		/// <summary>
		/// フォロー時処理をする
		/// </summary>
		/// <param name="next"></param>
		/// <returns></returns>
		public override IEnumerator OnProcess( Action<StateBase> next )
		{
			var game_manager = GameManager.instance;

			Debug.Log( $"{_follow_account.ToString()}をフォロしました" );

			// フォローバック判定
			float follow_back_rate = Account.CalculateFFRate( game_manager.playerAccount, _follow_account );
			Debug.Log( $"Rate = {follow_back_rate * 100}%" );
			if ( UnityEngine.Random.value < follow_back_rate )
			{
				game_manager.playerFollower.Value++;
				game_manager.motivation.Value += Account.CalculateMotivationChange( game_manager.playerAccount, _follow_account );
			}

			//// イベント発生判定
			//if( UnityEngine.Random.value < game_manager.eventProbability )
			//{
			//	next( new State_Event() );
			//	yield break;
			//}

			next( new State_WaitFollow() );
			yield break;
		}
	}

}