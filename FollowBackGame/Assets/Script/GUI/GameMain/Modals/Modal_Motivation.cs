using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// やる気のモーダル
/// </summary>
public class Modal_Motivation : ModalBase
{
	//! テキスト
	[SerializeField]
	TMPro.TextMeshProUGUI _text = null;

	//! やる気の色
	[SerializeField]
	Color _color_motivation = Color.cyan;

	/// <summary>
	/// UI更新設定
	/// </summary>
	public void Setup()
	{
		GameMain.GameManager.instance.motivation.Subscribe( motivation => SetMotivation( motivation ) );
	}

	/// <summary>
	/// やる気のテキスト更新
	/// </summary>
	/// <param name="motivation"></param>
	void SetMotivation( int motivation )
	{
		_text.text = $"やる気:<color=#{ColorUtility.ToHtmlStringRGB( _color_motivation )}>{motivation}</color>";
	}
}
