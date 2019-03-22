using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// モノビヘイビアに依存しないシングルトン
/// </summary>
public abstract class Singleton<T> where T : Singleton<T>, new()
{
	static T _instance = null;

	public static T instance
	{
		get
		{
			if( _instance == null )
			{
				_instance = new T();
			}

			return _instance;
		}
	}
}