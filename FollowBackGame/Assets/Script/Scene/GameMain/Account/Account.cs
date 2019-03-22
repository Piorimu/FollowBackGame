using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameMain
{
	public class Account
	{
		//! ユニークID
		public long uniqueID;
		//! 名前
		public string name;
		//! フォロー数
		public int follow;
		//! フォロワー数
		public int follower;
		//! アイコン
		public Texture icon;
		//! アイコン色
		public Color iconColor;

		/// <summary>
		/// フォロワー/フォロー率計算
		/// </summary>
		/// <returns></returns>
		public float CalculateFFRate()
		{
			float rate = 0f;
			if ( follow > 0 )
			{
				rate = (float)follower / follow;
			}
			return rate;
		}

		/// <summary>
		/// フォロバ確率計算
		/// </summary>
		/// <param name="account"></param>
		/// <param name="follwer"></param>
		/// <returns></returns>
		public static float CalculateFFRate( Account account, Account follwer )
		{
			float actor_ffrate = account.CalculateFFRate();
			float target_ffrate = follwer.CalculateFFRate();

			float player_target_rate = 1f;
			if ( actor_ffrate < float.Epsilon )
			{
				actor_ffrate = 1f;
			}
			if ( target_ffrate >= float.Epsilon )
			{
				player_target_rate = actor_ffrate / target_ffrate;
			}
			float follow_back_rate = player_target_rate * 0.5f;
			return follow_back_rate;
		}

		/// <summary>
		/// やる気変化量計算
		/// </summary>
		/// <param name="playerAccount"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static int CalculateMotivationChange( Account playerAccount, Account account )
		{
			return Mathf.Min( (int)( account.CalculateFFRate() * 2f ) + 1, 10 );
		}
	}
}