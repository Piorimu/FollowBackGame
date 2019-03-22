using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Title
{
	public class State_WaitEntry : StateBase
	{
		public override IEnumerator OnProcess( Action<StateBase> next )
		{
			var modal_entry = ModalManager.instance.GetModal<Modal_Entry>( "Entry" );
			bool is_wait = true;
			string username = "";

			modal_entry.OnChangedName.Subscribe( name => username = name );
			modal_entry.OnClickedButton.Where( x => x == Modal_Entry.eButton.NewGame ).
				Subscribe( x =>
				{
					if ( string.IsNullOrWhiteSpace( username ) )
					{
						return;
					}
					is_wait = false;
				} );

			while ( is_wait )
			{
				yield return null;
			}

			GlobalData.instance.playerName = username;

			next( new State_NewGame() );
			yield break;
		}
	}
}