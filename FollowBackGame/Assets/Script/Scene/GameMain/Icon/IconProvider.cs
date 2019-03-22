using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class IconProvider : SingletonMonobehaviour<IconProvider>
{
	//! テクスチャ配列
	[SerializeField]
	Texture2D[] _texturesIcon = null;

#if UNITY_EDITOR
	/// <summary>
	/// アイコンテクスチャ収集
	/// </summary>
	void Reset()
	{
		var guids_prefab = AssetDatabase.FindAssets( "t:texture", new string[] { "Assets/Textures/Icons" } );
		_texturesIcon = new Texture2D[ guids_prefab.Length ];
		for ( int i = 0; i < guids_prefab.Length; i++ )
		{
			string path_assets = AssetDatabase.GUIDToAssetPath( guids_prefab[ i ] );
			_texturesIcon[ i ] = AssetDatabase.LoadAssetAtPath<Texture2D>( path_assets );
		}
	}
#endif

	/// <summary>
	/// アイコン取得
	/// </summary>
	/// <returns></returns>
	public Texture2D GetRandomIcon()
	{
		return _texturesIcon[ UnityEngine.Random.Range( 0, _texturesIcon.Length ) ];
	}
}
