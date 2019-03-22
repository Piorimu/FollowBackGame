using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ModalFader
{
	//! フェードイン時間
	[SerializeField]
	float _fadein_time = 0.25f;
	//! フェードアウト時間
	[SerializeField]
	float _fadeout_time = 0.5f;
	//! フェードカーブ
	[SerializeField]
	AnimationCurve _curve_fadein = null;
	//! フェードカーブ
	[SerializeField]
	AnimationCurve _curve_fadeout = null;

	/// <summary>
	/// フェードイン
	/// </summary>
	/// <returns></returns>
	public IEnumerator ProcessFadeIn( CanvasGroup canvas_group )
	{
		float time = 0f;
		while ( time < _fadein_time )
		{
			time += Time.deltaTime;
			float t = time / _fadein_time;
			canvas_group.alpha = _curve_fadein.Evaluate( t );
			yield return null;
		}
		canvas_group.alpha = 1f;
	}

	/// <summary>
	/// フェードアウト
	/// </summary>
	/// <returns></returns>
	public IEnumerator ProcessFadeOut( CanvasGroup canvas_group )
	{
		float time = 0f;
		while ( time < _fadeout_time )
		{
			time += Time.deltaTime;
			float t = time / _fadeout_time;
			canvas_group.alpha = 1f - _curve_fadeout.Evaluate( t );
			yield return null;
		}
		canvas_group.alpha = 0f;
	}
}
