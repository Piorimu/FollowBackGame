using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Title
{
	/// <summary>
	/// タイトルマネージャ
	/// </summary>
	public class TitleManager : SingletonMonobehaviour<TitleManager>
	{
		void Start()
		{
			// モーダル登録
			var modal_collecter = GetComponent<ModalCollecter>();
			modal_collecter.Register( ModalManager.instance );
			// モーダル初期化

			// ステート開始
			var state_start = new State_InitializeApplication();
			var state_machine = new StateMachine( state_start );

			StartCoroutine( state_machine.Process() );
		}
	}
}