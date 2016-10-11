using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class CarRotateCtl : MonoBehaviour,IDragHandler,IEndDragHandler,IBeginDragHandler

{
	[SerializeField]
	private Image carBg;

	[SerializeField]
	private Button BuyBtn;

	[SerializeField]
	private float rate=15;

	[SerializeField]
	string  m_UniWebViewURL="http://m.lmoar.com/wap/product/2402";

	void Awake()
	{
		BuyBtn.onClick.AddListener (Buy);
	}

	int nowFram;
	[SerializeField]
	string framePath ="Mazda/car1/";

	[SerializeField]
	int TexNum=24;


	private bool isDrag;
	protected float startPosX,nowPosX,distanceX;



	void Update()
	{
		if (Input.GetKey(KeyCode.Escape)) {
			Application.Quit ();
		}

		if (isDrag) {

			//当达到一定距离时，触发图片的切换，并重置初始位置，继续计算
			distanceX = nowPosX - startPosX;
			if (distanceX>rate) {
				ChangeTexture (-1);
				startPosX = Input.mousePosition.x;
			}
			else if (distanceX<-rate) {
				ChangeTexture (1);
				startPosX = Input.mousePosition.x;
			}
		}

	}

	public  void OnBeginDrag (PointerEventData eventData)
	{
		//开始拖拽，记录当前位置
		isDrag=true;
		startPosX = eventData.position.x;
		//		Debug.Log ("屏幕宽："+Screen.width+" 高："+Screen.height);

	}

	public  void OnDrag (PointerEventData eventData)
	{
		//Debug.Log ("当前位置X：" + eventData.position.x+"," +eventData.position.y);
		//ChangeTexture (n/15);	
		nowPosX = eventData.position.x;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		//拖拽结束
		//清理开始记录时的位置
		isDrag=false;
		nowPosX = 0;
		startPosX = 0;
	}

	public void ChangeGrop(string PrefixName)
	{
		framePath = PrefixName;
		carBg.sprite = Resources.Load<Sprite> (framePath + "01");
		Debug.Log ("ChangeToTex:"+framePath + "01");
	}

	public void ChangeTexNum(int m_TextNum)
	{
		TexNum = m_TextNum;

	}

	public void ChangeBuyURL(string URL)
	{
		m_UniWebViewURL = URL;
	}

	public  void  ChangeTexture(int count)
	{

		if (count==0) {
			return;
		}
		nowFram = Convert.ToInt32( carBg.mainTexture.name.Substring(carBg.mainTexture.name.Length-2,2));

		if (count>0) {
			for (int i = 0; i < count; i++) {
				if (nowFram>=TexNum) {
					nowFram = 1;
				} else {
					nowFram++;
				}
			}
		} else {
			for (int i = 0; i > count; i--) {
				if (nowFram<=1) {
					nowFram = TexNum;
				} else {
					nowFram--;
				}
			}
		}
		carBg.sprite = Resources.Load<Sprite> (framePath + nowFram.ToString ("00"));
		Debug.Log (framePath +"_"+ nowFram.ToString ("00"));
	}

	void Buy()
	{
		OpenWeb.URL = m_UniWebViewURL;
		Application.LoadLevel (1);
	}
}
