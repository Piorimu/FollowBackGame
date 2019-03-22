using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameMain;
using UniRx;

/// <summary>
/// フォロー対象のアカウント
/// </summary>
public class Modal_OtherAccounts : ModalBase
{
	//! アカウントをひっかけるオブジェクト
	[SerializeField]
	GameObject _parent_accounts = null;

	//! アカウントのプレハブ
	[SerializeField]
	Panel_Account _prefab_account = null;

	//! フェードカーブ
	[SerializeField]
	AnimationCurve _curve_fade = null;

	//! 現表示アカウント
	List<Panel_Account> _current_panels = null;

	//! フェードイン時間
	[SerializeField]
	float _fadein_time = 0.25f;
	//! フェードアウト時間
	[SerializeField]
	float _fadeout_time = 0.5f;

	//! ボタンクリック通知
	private Subject<long> _subject_click_follow = new Subject<long>();
	//! フォロークリック
	public IObservable<long> OnClickedFollow
	{
		get
		{
			return _subject_click_follow;
		}
	}

	/// <summary>
	/// セットアップ
	/// </summary>
	/// <param name="accounts"></param>
	public void Setup( Panel_Account.PanelInfo[] panel_infos )
	{
		// 登録済みのパネルは削除
		if ( _current_panels != null )
		{
			foreach ( var panel in _current_panels )
			{
				Destroy( panel.gameObject );
			}
			_current_panels.Clear();
		}
		else
		{
			_current_panels = new List<Panel_Account>();
		}
		for ( int i = 0; i < panel_infos.Length; i++ )
		{
			var panel_info = panel_infos[ i ];
			// パネル作成
			var new_panel = Instantiate( _prefab_account, _parent_accounts.transform );
			new_panel.Initialize( panel_info );

			// ボタンのクリックを更に上に
			new_panel.OnClickedFollow.Subscribe( unique_id => _subject_click_follow.OnNext( unique_id ) );

			_current_panels.Add( new_panel );
		}
	}

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
