using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameMain
{

	/// <summary>
	/// アカウント生成器
	/// </summary>
	public class AccountGenerator : SingletonMonobehaviour<AccountGenerator>
	{
		//! アカウントカウンタ
		static long _uuid_counter = 0;

		//! 名前生成元データベース
		[SerializeField, Multiline]
		string randomNames = "";

		//! フォロー数分布カーブ
		[SerializeField]
		AnimationCurve followCurve = null;
		//! フォロワー数分布カーブ
		[SerializeField]
		AnimationCurve followerCurve = null;

		List<string> _random_name_array = new List<string>( 256 );

		protected override void Awake()
		{
			base.Awake();

			var name_array = randomNames.Split( '\n' );
			foreach ( string name in name_array )
			{
				if ( string.IsNullOrEmpty( name ) )
				{
					continue;
				}
				_random_name_array.Add( name );
			}
		}

		/// <summary>
		/// アカウント作成
		/// </summary>
		/// <returns></returns>
		public Account Generate()
		{
			Account new_account = new Account();
			new_account.uniqueID = _uuid_counter;
			_uuid_counter++;

			new_account.follow = (int)( followCurve.Evaluate( UnityEngine.Random.value ) * 100000 );
			new_account.follower = (int)( followerCurve.Evaluate( UnityEngine.Random.value ) * 1000000 );
			new_account.name = _random_name_array[ UnityEngine.Random.Range( 0, _random_name_array.Count ) ];
			new_account.icon = IconProvider.instance.GetRandomIcon();
			new_account.iconColor = Color.HSVToRGB( UnityEngine.Random.value, 0.5f, 1f );

			return new_account;
		}
	}

}