using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class Panel_Account : MonoBehaviour
{
	/// <summary>
	/// パネルに表示する情報構造体
	/// </summary>
	public class PanelInfo
	{
		//! アカウント
		public GameMain.Account account;
		//! フォロバ確率
		public float rateFollowBack;
		//! やる気上昇量
		public int motivationUp;
		//! ライン表示するか
		public bool isLine;
	}

	//! アイコン
	[SerializeField]
	private RawImage _rawimage_icon = null;

	//! 名前テキスト
	[SerializeField]
	private TMPro.TextMeshProUGUI _text_name = null;
	//! FFテキスト
	[SerializeField]
	private TMPro.TextMeshProUGUI _text_follows = null;

	//! ボタン
	[SerializeField]
	private Button _button_follow = null;

	//! ライン
	[SerializeField]
	private Image _image_line = null;

	//! ボタンの文字
	[SerializeField]
	private TMPro.TextMeshProUGUI _text_button = null;

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

	//! ボタンのマウスオーバー検知
	private Subject<bool> _subject_over_follow = new Subject<bool>();

	/// <summary>
	/// 初期化
	/// </summary>
	public void Initialize( PanelInfo panel_info )
	{
		// 各種表示設定
		_rawimage_icon.texture = panel_info.account.icon;
		_rawimage_icon.color = panel_info.account.iconColor;
		_text_name.text = panel_info.account.name;
		_text_follows.text = $"フォロー {panel_info.account.follow} フォロワー {panel_info.account.follower}";

		// 境界線の表示フラグ
		if ( !panel_info.isLine )
		{
			_image_line.enabled = false;
		}
		else
		{
			_image_line.enabled = true;
		}

		// ボタン登録
		_button_follow.onClick.AsObservable().Subscribe( _ =>
		{
			_subject_click_follow.OnNext( panel_info.account.uniqueID );
		} );

		// マウスオーバー時の表示切り替え
		_subject_over_follow.Subscribe( is_over =>
		{
			if ( is_over )
			{
				_text_button.text = $"{( Math.Min( panel_info.rateFollowBack * 100, 100f ) ).ToString( "0.###" )}%/+{panel_info.motivationUp}";
			}
			else
			{
				_text_button.text = "フォローする";
			}
		} );
	}

	public void OnPointerEnterButton()
	{
		_subject_over_follow.OnNext( true );
	}
	public void OnPointerExitButton()
	{
		_subject_over_follow.OnNext( false );
	}
}
