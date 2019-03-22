using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameMain
{
	/// <summary>
	/// イベント種類
	/// </summary>
	public enum eEventType
	{
		Normal,
		YesNo,
	}

	/// <summary>
	/// イベントデータ
	/// </summary>
	[System.Serializable]
	public class EventData
	{
		//! 処理ID
		public int indexID;
		//! 次のイベント
		public int nextID;
		//! はい選択時イベント
		public int nextYesID;
		//! いいえ選択時イベント
		public int nextNoID;
		//! 判定成功時のイベント
		public int nextSuccessID;
		//! イベントタイプ
		public eEventType type;
		//! フォロワー数変化
		public int followChange;
		//! やる気変化
		public int motivationChange;
		//! テキスト
		public string text;
	}

	/// <summary>
	/// イベントバンドル
	/// </summary>
	[System.Serializable]
	public class EventBundle
	{
		//! バンドルID
		public int bundleID;
		//! イベント配列
		public EventData[] events;
	}

	/// <summary>
	/// イベント抽選データ
	/// </summary>
	[System.Serializable]
	public class EventSelectElement
	{
		//! ユニークID
		public int uniqueID;
		//! バンドルID
		public int bundleID;
		//! 確率
		public int probability;
		//! フォロー数指定
		public int followCheck;
	}
}