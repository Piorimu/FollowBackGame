using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameMain;
using UniRx;

/// <summary>
/// フォロー対象のアカウント
/// </summary>
public class Modal_Lisense : ModalBase
{
	//! フェードカーブ
	[SerializeField]
	AnimationCurve _curve_fade = null;
	
	//! フェードイン時間
	[SerializeField]
	float _fadein_time = 0.25f;
	//! フェードアウト時間
	[SerializeField]
	float _fadeout_time = 0.5f;

	/// <summary>
	/// フェードイン
	/// </summary>
	/// <returns></returns>
	protected override IEnumerator ProcessOpen()
	{
		float time = 0f;
		while ( time < _fadein_time )
		{
			time += Time.deltaTime;
			float t = time / _fadein_time;
			_canvas_group.alpha = _curve_fade.Evaluate( t );
			yield return null;
		}
		_canvas_group.alpha = 1f;
	}

	/// <summary>
	/// フェードアウト
	/// </summary>
	/// <returns></returns>
	protected override IEnumerator ProcessClose()
	{
		float time = 0f;
		while ( time < _fadeout_time )
		{
			time += Time.deltaTime;
			float t = time / _fadeout_time;
			_canvas_group.alpha = 1f - _curve_fade.Evaluate( t );
			yield return null;
		}
		_canvas_group.alpha = 0f;
	}
}
