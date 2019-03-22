using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace GameMain
{
	public class State_WaitFollow : StateBase
	{
		public override IEnumerator OnProcess( Action<StateBase> next )
		{
			// やる気が0になったらゲームオーバー
			GameManager game_manager = GameManager.instance;
			if ( game_manager.motivation.Value <= 0 )
			{
				next( new State_GameOver() );
				yield break;
			}

			// アカウント作成
			Account[] account = new Account[]
				{
					AccountGenerator.instance.Generate(),
					AccountGenerator.instance.Generate(),
					AccountGenerator.instance.Generate(),
				};
			var modal_others = ModalManager.instance.GetModal<Modal_OtherAccounts>( "OtherAccounts" );

			// 実確率はフォロー数が増えてからなので、ここでフォロー数を増やしてからフォロー率計算させる
			game_manager.playerAccount.follow++;

			Panel_Account.PanelInfo[] panel_infos = new Panel_Account.PanelInfo[ account.Length ];
			for ( int i = 0; i < account.Length; i++ )
			{
				panel_infos[ i ] = new Panel_Account.PanelInfo();
				panel_infos[ i ].account = account[ i ];
				panel_infos[ i ].rateFollowBack = Account.CalculateFFRate( game_manager.playerAccount, account[ i ] );
				panel_infos[ i ].motivationUp = Account.CalculateMotivationChange( game_manager.playerAccount, account[ i ] );
				bool is_line = i < panel_infos.Length - 1; // 終端なら区切り線はいらない
				panel_infos[ i ].isLine = is_line;
			}

			modal_others.Setup( panel_infos );

			bool is_wait = true;
			long clicked_unique_id = 0;
			// フォローボタンが押されたら次へ
			modal_others.OnClickedFollow.Subscribe( account_unique_id =>
			{
				clicked_unique_id = account_unique_id;
				is_wait = false;
				SoundManager.instance.Play( SoundManager.eSE.Click );
			} );

			yield return modal_others.Open();

			// 待ち
			while ( is_wait )
			{
				yield return null;
			}

			game_manager.motivation.Value--;
			game_manager.playerFollow.Value++;

			// 次へ
			yield return modal_others.Close();

			// フォローしたアカウントを検索
			for ( int i = 0; i < account.Length; i++ )
			{
				if ( account[ i ].uniqueID == clicked_unique_id )
				{
					next( new State_ProcessFollow( account[ i ] ) );
				}
			}
			yield break;
		}
	}

}