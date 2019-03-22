using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// モーダル管理マネージャ
/// </summary>
public class ModalManager : SingletonMonobehaviour<ModalManager>
{
	/// <summary>
	/// 一括登録用ペア
	/// </summary>
	[System.Serializable]
	public class ModalPair
	{
		public string key;
		public ModalBase modal;
	}

	//! モーダルデータベース
	Dictionary<string, ModalBase> _modals = new Dictionary<string, ModalBase>();

	/// <summary>
	/// モーダル登録
	/// </summary>
	/// <param name="key"></param>
	/// <param name="modal"></param>
	public void RegisterModal( string key, ModalBase modal )
	{
		if( modal == null )
		{
			Debug.Log( $"{key}モーダルがnullです" );
			return;
		}
		if ( _modals.ContainsKey( key ) )
		{
			Debug.Log( $"{key}モーダルは登録済みです" );
			return;
		}
		_modals.Add( key, modal );
	}
	/// <summary>
	/// モーダル登録（一括）
	/// </summary>
	/// <param name="modals"></param>
	public void RegisterModal( ModalPair[] modals )
	{
		foreach( var pair in modals )
		{
			RegisterModal( pair.key, pair.modal );
		}
	}

	/// <summary>
	/// モーダル取得
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="key"></param>
	/// <returns></returns>
	public T GetModal<T>( string key ) where T : ModalBase
	{
		ModalBase modal = null;
		if( _modals.TryGetValue( key, out modal  ) )
		{
			return (T)modal;
		}
		else
		{
			Debug.Log( $"{key}モーダルは見つかりませんでした" );
		}
		return null;
	}
}
