using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

/// <summary>
/// プレイヤーアカウント情報モーダル
/// </summary>
public class Modal_GameOver : ModalBase
{
	/// <summary>
	/// ボタン列挙
	/// </summary>
	public enum eButton
	{
		ToTitle,    //!< タイトルへ
	}

	//! FF率テキスト
	[SerializeField]
	TMPro.TextMeshProUGUI _text_ffrate = null;

	//! タイトルボタン
	[SerializeField]
	Button _button_title = null;

	//! フェード設定
	[SerializeField]
	ModalFader _fader = null;

	//! ボタン通知
	Subject<eButton> _subject_click_button = new Subject<eButton>();
	public IObservable<eButton> onButtonClick
	{
		get
		{
			return _subject_click_button;
		}
	}

	/// <summary>
	/// UI更新設定
	/// </summary>
	public void Setup( GameMain.Account player_account )
	{
		_text_ffrate.text = $"{player_account.CalculateFFRate() * 100}%";

		_button_title.OnClickAsObservable().Subscribe( x =>
		{
			_subject_click_button.OnNext( eButton.ToTitle );
		} );
	}

	/// <summary>
	/// フェードイン
	/// </summary>
	/// <returns></returns>
	protected override IEnumerator ProcessOpen()
	{
		yield return _fader.ProcessFadeIn( _canvas_group );
	}
	/// <summary>
	/// フェードアウト
	/// </summary>
	/// <returns></returns>
	protected override IEnumerator ProcessClose()
	{
		yield return _fader.ProcessFadeOut( _canvas_group );
	}
}
