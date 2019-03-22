using System.Collections;
using UnityEngine;
using UniRx;

namespace GameMain
{
	public class GameManager : SingletonMonobehaviour<GameManager>
	{
		//! 初期やる気
		[SerializeField]
		int startMotivation = 10;

		//! やる気
		public ReactiveProperty<int> motivation = new ReactiveProperty<int>();
		//! プレイヤーのフォワー
		public ReactiveProperty<int> playerFollow = new ReactiveProperty<int>();
		//! プレイヤーのフォロワー
		public ReactiveProperty<int> playerFollower = new ReactiveProperty<int>();

		//! プレイヤーアカウント
		Account _player_account = null;

		//! イベント発生率
		[SerializeField]
		float _eventProbability = 0.5f;
		public float eventProbability
		{
			get { return _eventProbability; }
		}

		public Account playerAccount
		{
			get { return _player_account; }
		}

		protected void Start()
		{
			// パラメータ初期化
			motivation.Value = startMotivation;
			playerFollow.Value = 0;
			playerFollower.Value = 0;
			// プレイヤーアカウント作成
			_player_account = AccountGenerator.instance.Generate();
			if ( string.IsNullOrWhiteSpace( GlobalData.instance.playerName ) == false )
			{
				_player_account.name = GlobalData.instance.playerName;
			}
			// リアクティブプロパティとリンクさせる
			playerFollow.Subscribe( follow => _player_account.follow = follow );
			playerFollower.Subscribe( follower => _player_account.follower = follower );

			// モーダル登録
			var modal_collecter = GetComponent<ModalCollecter>();
			modal_collecter.Register( ModalManager.instance );
			// モーダルの初期設定
			var modal_top = ModalManager.instance.GetModal<Modal_Motivation>( "Motivation" );
			modal_top.Setup();
			var modal_player = ModalManager.instance.GetModal<Modal_PlayerAccount>( "PlayerAccount" );
			modal_player.Setup( _player_account );

			// ステート開始
			var state_start = new State_Start();
			var state_machine = new StateMachine( state_start );

			StartCoroutine( state_machine.Process() );
		}
	}
}