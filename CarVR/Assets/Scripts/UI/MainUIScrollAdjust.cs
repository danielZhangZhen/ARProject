using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainUIScrollAdjust : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
	public float moveSpeed = 4f;

	private ScrollRect scrollRect;
	private float startPositoin = 0f; //用于记录鼠标点下时滚动条位置
	private float targetPosition = 0f;
	private bool flag = false;
	private int childCount = 0; //item数量

	private bool isNeedGetChild = true;

	private int index = 0;//用于计数

	private RectTransform gridRect;//用于设置子物体Grid的长宽
	private GridLayoutGroup grid;//用于设置子物体GridLayoutGroup的子物体元素大小


	void Awake()
	{
		scrollRect = transform.GetComponent<ScrollRect>();

//		gridRect = transform.Find ("Grid").GetComponent<RectTransform> ();
//		gridRect.sizeDelta = new Vector2 (Screen.width, Screen.height);
//
//		grid = gridRect.GetComponent<GridLayoutGroup> ();
//		grid.cellSize = new Vector2 (Screen.width, Screen.height);
	}

	void Update()
	{
		if (flag)
		{
			scrollRect.horizontalNormalizedPosition = Mathf.Lerp(scrollRect.horizontalNormalizedPosition,
				targetPosition, Time.deltaTime * moveSpeed);
		}
	}
	//鼠标开始拖动事件监听
	public void OnBeginDrag(PointerEventData eventData)
	{
		startPositoin = scrollRect.horizontalNormalizedPosition;
		flag = false;
	}
	//鼠标结束拖动事件监听
	public void OnEndDrag(PointerEventData eventData)
	{
		flag = true;
		targetPosition = GetTargetPosition();
		//Debug.Log(targetPosition);
	}
	//获得孙子物体的个数
	void GetChildCount()
	{

		childCount = transform.FindChild("Grid").childCount;
		//Debug.Log(childCount);
	}
	//计算自适应位置
	float GetTargetPosition()
	{
		if (isNeedGetChild)
		{
			GetChildCount();
			isNeedGetChild = false;
		}

		float posx = scrollRect.horizontalNormalizedPosition;//鼠标拖动到的位置

		if (posx - startPositoin > 0.01) //当拖动大于0.06时
		{
			if(index < childCount-1)
				index++;
		}
		else if (posx - startPositoin < -0.01)
		{
			if (index >= 1)
				index--;
		}
		SetToggleIsOn(index);
		//Debug.Log(index);
		return (float)index * (1 / ((float)childCount - 1)); //需要适配到的位置
	}

	void SetToggleIsOn(int index)
	{
		scrollRect.transform.parent.FindChild("Toggle/" + index).GetComponent<Toggle>().isOn = true;
		//transform.parent.GetComponent< UpdateText>().UpdateTXT(index);
	}


}
