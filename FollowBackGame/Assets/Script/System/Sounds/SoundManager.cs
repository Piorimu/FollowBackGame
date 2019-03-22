using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// 音再生系管理クラス
/// </summary>
public class SoundManager : SingletonMonobehaviour<SoundManager>
{
	//! 効果音同時再生数
	static readonly int MAX_PLAY_SE = 4;

	/// <summary>
	/// 効果音列挙
	/// </summary>
	public enum eSE
	{
		Click,
	}

	//! 効果音
	[SerializeField]
	AudioClip[] _sounds = null;

	//! 効果音再生コンポーネント（複数音管理したいので配列に）
	[SerializeField]
	AudioSource[] _players_se = null;

#if UNITY_EDITOR
	/// <summary>
	/// 初期設定
	/// </summary>
	private void Reset()
	{
		// 既についてるオーディオソース削除
		var old_sources = GetComponents<AudioSource>();
		foreach ( var source in old_sources )
		{
			DestroyImmediate( source );
		}

		// オーディオソース必要分作成
		_players_se = new AudioSource[ MAX_PLAY_SE ];
		for ( int i = 0; i < _players_se.Length; i++ )
		{
			_players_se[ i ] = gameObject.AddComponent<AudioSource>();
			_players_se[ i ].playOnAwake = false;
		}
	}
#endif

	/// <summary>
	/// 効果音再生
	/// </summary>
	/// <param name="se_no"></param>
	public void Play( eSE se_no )
	{
		for ( int i = 0; i < _players_se.Length; i++ )
		{   // 再生できるやつに再生させる
			var source = _players_se[ i ];
			if ( source.isPlaying )
			{   // 再生できないので次
				continue;
			}

			PlayBySource( se_no, source );
			return;
		}

		// もし空きがなければ0番目を強制的に使う
		PlayBySource( se_no, _players_se[ 0 ] );
	}

	/// <summary>
	/// AudioSourceで再生
	/// </summary>
	/// <param name="se_no"></param>
	/// <param name="source"></param>
	private void PlayBySource( eSE se_no, AudioSource source )
	{
		// 再生
		source.clip = _sounds[ (int)se_no ];
		source.Play();
	}
}
