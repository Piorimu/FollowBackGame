using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

/// <summary>
/// プレイヤーアカウント情報モーダル
/// </summary>
public class Modal_Entry : ModalBase
{
	/// <summary>
	/// ボタン種別列挙
	/// </summary>
	public enum eButton
	{
		NewGame,        //!< ニューゲーム
		Lisense,        //!< 権利表記
	}

	//! ニューゲームボタン
	[SerializeField]
	Button _button_newgame = null;
	//! 権利表記ボタン
	[SerializeField]
	Button _button_lisense = null;

	//! ユーザ名フィールド
	[SerializeField]
	TMPro.TMP_InputField _input_username = null;

	//! ニューゲームボタン通知
	Subject<eButton> _subject_click = new Subject<eButton>();
	public IObservable<eButton> OnClickedButton
	{
		get
		{
			return _subject_click;
		}
	}

	//! 名前
	ReactiveProperty<string> _entry_name = new ReactiveProperty<string>();
	public IObservable<string> OnChangedName
	{
		get
		{
			return _entry_name;
		}
	}

	/// <summary>
	/// UI更新設定
	/// </summary>
	public void Setup()
	{
		// ボタンコールバック
		_button_newgame.onClick.AsObservable().Subscribe( unit =>
		{
			_subject_click.OnNext( eButton.NewGame );
		} );
		_button_lisense.onClick.AsObservable().Subscribe( unit =>
		{
			_subject_click.OnNext( eButton.Lisense );
		} );
	}

	/// <summary>
	/// ユーザ名変更時処理
	/// </summary>
	public void OnChangedInputName()
	{
		_entry_name.Value = _input_username.text;
	}
}
