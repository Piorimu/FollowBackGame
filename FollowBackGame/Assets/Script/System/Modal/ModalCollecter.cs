using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModalCollecter : MonoBehaviour
{
	[SerializeField]
	ModalManager.ModalPair[] _modals = null;

	/// <summary>
	/// ヒエラルキー上のモーダルを収集
	/// </summary>
	private void Reset()
	{
		var modal_pair_list = new List<ModalManager.ModalPair>();
		var modals = FindObjectsOfType<ModalBase>();
		foreach( var modal in modals )
		{
			string key = modal.name.Substring( 6, modal.name.Length - 6 );
			modal_pair_list.Add( new ModalManager.ModalPair()
			{
				key = key,
				modal = modal,
			} );
		}
		_modals = modal_pair_list.ToArray();
	}

	/// <summary>
	/// 収集情報を登録
	/// Awakeでやると処理順が面倒なので受動的に行うように
	/// </summary>
	/// <param name="modal_manager"></param>
	public void Register( ModalManager modal_manager )
	{
		modal_manager.RegisterModal( _modals );
	}
}
