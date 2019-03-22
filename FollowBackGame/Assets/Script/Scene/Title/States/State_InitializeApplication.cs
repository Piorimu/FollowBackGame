using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Title
{
	/// <summary>
	/// ゲーム開始時処理をする
	/// </summary>
	public class State_InitializeApplication : StateBase
	{
		public override IEnumerator OnProcess( Action<StateBase> next )
		{
			// モーダル初期化
			var modal_entry = ModalManager.instance.GetModal<Modal_Entry>( "Entry" );
			var modal_lisense = ModalManager.instance.GetModal<Modal_Lisense>( "Lisense" );
			modal_entry.Setup();

			modal_entry.OnClickedButton.Where( x => x == Modal_Entry.eButton.Lisense )
				.Subscribe( x =>
				{
					// アニメ中は操作しない
					if( modal_lisense.isAnimation )
					{
						return;
					}

					if( modal_lisense.isOpen )
					{
						Observable.FromCoroutine( modal_lisense.Close ).Subscribe();
					}
					else
					{
						Observable.FromCoroutine( modal_lisense.Open ).Subscribe();
					}
				} );

			next( new State_WaitEntry() );
			yield break;
		}
	}
}