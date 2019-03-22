using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Title
{
	public class State_Lisense : StateBase
	{
		public override IEnumerator OnProcess( Action<StateBase> next )
		{
			next( new State_WaitEntry() );
			yield break;
		}
	}
}