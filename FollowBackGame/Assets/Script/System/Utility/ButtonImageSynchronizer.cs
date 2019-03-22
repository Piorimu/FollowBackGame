using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.EventSystems;

[RequireComponent( typeof( Button ) )]
public class ButtonImageSynchronizer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
	[SerializeField]
	Button button = null;

	[SerializeField]
	Image image = null;

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
		image.color = colors.highlightedColor;
		//Debug.Log( "OnPointerEnter" );
	}

	public void OnPointerExit( PointerEventData eventData )
	{
		image.color = colors.normalColor;
		//Debug.Log( "OnPointerExit" );
	}

	public void OnPointerDown( PointerEventData eventData )
	{
		image.color = colors.pressedColor;
		//Debug.Log( "OnPointerDown" );
	}

	public void OnPointerUp( PointerEventData eventData )
	{
		image.color = colors.normalColor;
		//Debug.Log( "OnPointerUp" );
	}
}
