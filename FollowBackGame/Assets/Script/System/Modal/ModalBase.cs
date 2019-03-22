using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// モーダル基底クラス
/// </summary>
[RequireComponent(typeof(CanvasGroup),typeof(Canvas))]
public class ModalBase : MonoBehaviour
{
	//! キャンバス
	[SerializeField]
	protected Canvas _canvas = null;
	//! キャンバスグループ
	[SerializeField]
	protected CanvasGroup _canvas_group = null;

	//! 開いているか
	public bool isOpen { get; private set; } = false;

	//! 開閉中か
	public bool isAnimation { get; private set; } = false;

	/// <summary>
	/// デフォルトオブジェクト登録
	/// </summary>
	private void Reset()
	{
		_canvas = GetComponent<Canvas>();
		_canvas_group = GetComponent<CanvasGroup>();
	}

	/// <summary>
	/// モーダルを開く
	/// </summary>
	/// <returns></returns>
	public IEnumerator Open()
	{
		isAnimation = true;
		yield return ProcessOpen();
		_canvas_group.blocksRaycasts = true;
		isAnimation = false;
		isOpen = true;
	}

	/// <summary>
	/// 開いた際の演出用関数
	/// </summary>
	/// <returns></returns>
	protected virtual IEnumerator ProcessOpen()
	{
		yield break;
	}

	/// <summary>
	/// モーダルを閉じる
	/// 完全に見えないようにする
	/// </summary>
	/// <returns></returns>
	public IEnumerator Close()
	{
		isAnimation = true;
		isOpen = false;
		yield return ProcessClose();
		_canvas_group.blocksRaycasts = false;
		isAnimation = false;
	}

	protected virtual IEnumerator ProcessClose()
	{
		yield break;
	}
}
