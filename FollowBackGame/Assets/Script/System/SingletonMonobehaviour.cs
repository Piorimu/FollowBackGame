using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonobehaviour<T> : MonoBehaviour where T : SingletonMonobehaviour<T>
{
	//! 唯一のインスタンス
	static T _instance = null;

	public static T instance
	{
		get
		{
			if ( _instance == null )
			{
				// 検索しない
				Debug.Log( $"{typeof( T )}インスタンス初期化前にアクセスしようとしました。" );
				return null;
			}
			return _instance;
		}
	}

	public static bool isCreated
	{
		get { return _instance != null; }
	}

	protected virtual void Awake()
	{
		if ( _instance == null )
		{
			_instance = (T)this;
		}
		else
		{
			Destroy( this );
		}
	}
}
