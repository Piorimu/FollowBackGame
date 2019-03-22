using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase
{
	/// <summary>
	/// 開始前処理
	/// </summary>
	public virtual void OnStart()
	{
	}

	/// <summary>
	/// メイン処理
	/// </summary>
	/// <param name="next"></param>
	/// <returns></returns>
	public abstract IEnumerator OnProcess( Action<StateBase> next );

	/// <summary>
	/// 終了時処理
	/// </summary>
	public virtual void OnFinish()
	{
	}
}
