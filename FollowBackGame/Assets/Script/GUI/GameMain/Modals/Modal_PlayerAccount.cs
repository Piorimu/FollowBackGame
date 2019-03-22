using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

/// <summary>
/// プレイヤーアカウント情報モーダル
/// </summary>
public class Modal_PlayerAccount : ModalBase
{
	//! アイコン
	[SerializeField]
	RawImage _rawimage_icon = null;
	//! 名前テキスト
	[SerializeField]
	TMPro.TextMeshProUGUI _text_name = null;
	//! IDテキスト
	[SerializeField]
	TMPro.TextMeshProUGUI _text_id = null;
	//! フォローテキスト
	[SerializeField]
	TMPro.TextMeshProUGUI _text_follow = null;
	//! フォロワーテキスト
	[SerializeField]
	TMPro.TextMeshProUGUI _text_follower = null;

	//! フェード設定
	[SerializeField]
	ModalFader _fader = null;

	/// <summary>
	/// UI更新設定
	/// </summary>
	public void Setup( GameMain.Account player_account )
	{
		_rawimage_icon.texture = player_account.icon;
		_rawimage_icon.color = player_account.iconColor;
		_text_name.text = player_account.name;
		// リアクティブプロパティ設定
		GameMain.GameManager.instance.playerFollow.Subscribe( follow => _text_follow.text = follow.ToString() );
		GameMain.GameManager.instance.playerFollower.Subscribe( follower => _text_follower.text = follower.ToString() );
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
