using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;

namespace GameMain
{
	public class State_GameOver : StateBase
	{
		public override IEnumerator OnProcess( Action<StateBase> next )
		{
			GameManager game_manager = GameManager.instance;

			var modal_gameover = ModalManager.instance.GetModal<Modal_GameOver>( "GameOver" );
			modal_gameover.Setup( game_manager.playerAccount );

			bool is_wait = true;
			modal_gameover.onButtonClick.Where( x => x == Modal_GameOver.eButton.ToTitle )
				.Subscribe( x =>
				{
					is_wait = false;
				} );

			// ランキング送信
			naichilab.RankingLoader.Instance.SendScoreAndShowRanking( game_manager.playerFollower.Value );

			yield return modal_gameover.Open();

			while( is_wait )
			{
				yield return null;
			}

			yield return SceneManager.LoadSceneAsync( "Title" );
			next( null );
		}
	}

}