using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace GameMain
{
	public class State_Result : StateBase
	{
		public override IEnumerator OnProcess( Action<StateBase> next )
		{
			yield return null;
		}
	}

}