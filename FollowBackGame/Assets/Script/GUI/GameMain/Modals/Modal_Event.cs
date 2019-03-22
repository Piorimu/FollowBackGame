using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

/// <summary>
/// イベントモーダル
/// </summary>
public class Modal_Event : ModalBase
{
	/// <summary>
	/// イベント再生データ
	/// </summary>
	public class EventPlayData
	{
		//! ボタン表示タイプ
		public eDisplayType type;
		//! イベントテキスト
		public string eventText;
		//! 文字速度
		public float textSpeed;
	}

	/// <summary>
	/// 表示タイプ列挙
	/// </summary>
	public enum eDisplayType
	{
		OK,
		YesNo,
	}

	/// <summary>
	/// ボタン列挙
	/// </summary>
	public enum eButton
	{
		Yes,        //!< はい
		No,     //!< いいえ
		OK,     //!< すすむ
	}

	//! イベントテキスト
	[SerializeField]
	TMPro.TextMeshProUGUI _text_event = null;

	//! はいボタン
	[SerializeField]
	Button _button_yes = null;
	//! いいえボタン
	[SerializeField]
	Button _button_no = null;
	//! すすむボタン
	[SerializeField]
	Button _button_ok = null;

	//! はいいいえGroup
	[SerializeField]
	CanvasGroup _group_yesno = null;
	//! OKGroup
	[SerializeField]
	CanvasGroup _group_ok = null;

	//! フェード設定
	[SerializeField]
	ModalFader _fader = null;
	//! ボタンフェード設定
	[SerializeField]
	ModalFader _button_fader = null;

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
	public void Setup()
	{
		_button_yes.OnClickAsObservable().Subscribe( x =>
		{
			_subject_click_button.OnNext( eButton.Yes );
		} );
		_button_no.OnClickAsObservable().Subscribe( x =>
		{
			_subject_click_button.OnNext( eButton.No );
		} );
		_button_ok.OnClickAsObservable().Subscribe( x =>
		{
			_subject_click_button.OnNext( eButton.OK );
		} );
	}

	/// <summary>
	/// イベントデータを再生する
	/// </summary>
	/// <param name="data"></param>
	public IEnumerator Play( EventPlayData data )
	{
		// 開く
		_text_event.text = "";
		yield return Open();

		//文字送り
		bool is_end = false;
		var text_stream = new SingleAssignmentDisposable();
		text_stream.Disposable = Observable.Interval( TimeSpan.FromSeconds( data.textSpeed ) )
			.Subscribe( count =>
		{
			// 文字送りきったらボタンを出して終わり
			if ( count > data.eventText.Length )
			{
				is_end = true;
				switch ( data.type )
				{
					case eDisplayType.OK:
						_group_ok.blocksRaycasts = true;
						StartCoroutine( _button_fader.ProcessFadeIn( _group_ok ) );
						break;
					case eDisplayType.YesNo:
						_group_yesno.blocksRaycasts = true;
						StartCoroutine( _button_fader.ProcessFadeIn( _group_yesno ) );
						break;
				}

				text_stream.Dispose();
				return;
			}
			_text_event.text = data.eventText.Substring( 0, (int)count );
		} );

		while ( is_end == false )
		{
			yield return null;
		}
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
		// 再表示した際のために隠しておく
		_group_yesno.alpha = 0f;
		_group_yesno.blocksRaycasts = false;
		_group_ok.alpha = 0f;
		_group_ok.blocksRaycasts = false;
	}
}

