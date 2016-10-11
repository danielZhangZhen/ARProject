//**********************************************************
//** 姓名 ：林宇敬
//** 日期 ：2016年03月22日
//** 类作用： 房间内UI窗口各个Image是否显示逻辑控制   
//**          
//**********************************************************
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIBtnControlInRoom : MonoBehaviour {

    private GameObject seat;
    private GameObject footpad;
    private GameObject hub;
    private GameObject windowFilm;
    private GameObject navigation;
	// Use this for initialization

    private GameObject preClickBtn = null;

	void Awake () {
        seat = transform.Find("CarSeat").gameObject;
        footpad = transform.Find("CarFootPad").gameObject;
        hub = transform.Find("CarHub").gameObject;
        windowFilm = transform.Find("CarWindowFilm").gameObject;
        navigation = transform.Find("CarNavigation").gameObject;
	}

    public void OnClick(RectTransform trans)
    {
        //Debug.Log("switch");
        switch (trans.name)
        {
            case "CarSeat":
                //Debug.Log("123");
                seat.SetActive(true);
                if (preClickBtn != null)
                    preClickBtn.SetActive(false);
                preClickBtn = seat.gameObject;
                break;
            case "CarFootPad":
                footpad.SetActive(true);
                if (preClickBtn != null)
                    preClickBtn.SetActive(false);
                preClickBtn = footpad.gameObject;
                break;
            case "CarHub":
                hub.SetActive(true);
                if (preClickBtn != null)
                    preClickBtn.SetActive(false);
                preClickBtn = hub.gameObject;
                break;
            case "CarWindowFilm":
                windowFilm.SetActive(true);
                if (preClickBtn != null)
                    preClickBtn.SetActive(false);
                preClickBtn = windowFilm.gameObject;
                break;
            case "CarNavigation":
                navigation.SetActive(true);
                if (preClickBtn != null)
                    preClickBtn.SetActive(false);
                preClickBtn = navigation.gameObject;
                break;
            default:
                if (preClickBtn != null)
                {
                    preClickBtn.SetActive(false);
                    preClickBtn = null;
                }
                //Debug.Log("click");
                break;
        }
    }

}
