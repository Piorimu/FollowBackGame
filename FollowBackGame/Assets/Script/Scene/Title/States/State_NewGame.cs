using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;

namespace Title
{
	public class State_NewGame : StateBase
	{
		public override IEnumerator OnProcess( Action<StateBase> next )
		{
			yield return SceneManager.LoadSceneAsync("GameMain");
			next( null );
			yield break;
		}
	}
}