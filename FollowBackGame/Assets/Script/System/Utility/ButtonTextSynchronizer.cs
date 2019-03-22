using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.EventSystems;

[RequireComponent( typeof( Button ) )]
public class ButtonTextSynchronizer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
	[SerializeField]
	Button button = null;

	[SerializeField]
	TMPro.TextMeshProUGUI text = null;

	[SerializeField]
	ColorBlock colors = new ColorBlock();

	/// <summary>
	/// 親のボタンを取得
	/// </summary>
	private void Reset()
	{
		button = GetComponent<Button>();
		colors = button.colors;
	}

	public void OnPointerEnter( PointerEventData eventData )
	{
		text.color = colors.highlightedColor;
		//Debug.Log( "OnPointerEnter" );
	}

	public void OnPointerExit( PointerEventData eventData )
	{
		text.color = colors.normalColor;
		//Debug.Log( "OnPointerExit" );
	}

	public void OnPointerDown( PointerEventData eventData )
	{
		text.color = colors.pressedColor;
		//Debug.Log( "OnPointerDown" );
	}

	public void OnPointerUp( PointerEventData eventData )
	{
		text.color = colors.normalColor;
		//Debug.Log( "OnPointerUp" );
	}
}
