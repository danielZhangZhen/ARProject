using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class OnlyPopOneForCar : MonoBehaviour {

    //MainMenu下的Button
    private Transform mainMenuGrid;
    private List<Button> carBtnList = new List<Button>();//存放DownLayer的退出Button
    private List<GameObject> centerTexList = new List<GameObject>();//存放中间窗口贴图对象
    
    private Button carColor;
    private Button carHub;
    private Button carFootPad;
    private Button carWindowFilm;
    private Button carNavigation;
    private Button carSeat;

    //DownLayer关闭按钮
    private Button seat;
    private Button footpad;
    private Button hub;
    private Button windowFilm;
    private Button color;

    private Button preClickBtn = null;//存放前一次DownLayer的back按钮
    private GameObject preClickObj = null; //存放前一次中间窗口贴图对象

    private CommonTWPos mainMenuCommon;
    //
    private CommonTWPos seatCommon;
    private CommonTWPos footpadCommon;
    private CommonTWPos hubCommon;
    private CommonTWPos windowFilmCommon;
    private CommonTWPos colorCommon;

   // private CommonTWPos preCommon;//点击底部按钮

    //中间窗口贴图对象
    private GameObject seatObj;
    private GameObject footpadObj;
    private GameObject hubObj;
    private GameObject windowFilmObj;
    private GameObject navigationObj;

    void Awake()
    {
        //底部按钮
        mainMenuGrid = transform.Find("UpLayer/MainBg/MainMenu/Grid");
        carColor = mainMenuGrid.Find("CarColor").GetComponent<Button>();
        carHub = mainMenuGrid.Find("CarHub").GetComponent<Button>();
        carFootPad = mainMenuGrid.Find("CarFootPad").GetComponent<Button>();
        carWindowFilm = mainMenuGrid.Find("CarWindowFilm").GetComponent<Button>();
        carNavigation = mainMenuGrid.Find("CarNavigation").GetComponent<Button>();
        carSeat = mainMenuGrid.Find("CarSeat").GetComponent<Button>();
        //视野外的退出按钮
        seat = transform.Find("DownLayer/Seat/1back").GetComponent<Button>();
        footpad = transform.Find("DownLayer/FootPad/0back").GetComponent<Button>();
        hub = transform.Find("DownLayer/Hud/back").GetComponent<Button>();
        windowFilm = transform.Find("DownLayer/WindowFilm/back").GetComponent<Button>();
        color = transform.Find("DownLayer/Color/back").GetComponent<Button>();
        //底部及视野外动画组件获取
        mainMenuCommon = mainMenuGrid.parent.parent.GetComponent<CommonTWPos>();
        seatCommon = transform.Find("DownLayer/Seat").GetComponent<CommonTWPos>();
        footpadCommon = transform.Find("DownLayer/FootPad").GetComponent<CommonTWPos>(); 
        hubCommon = transform.Find("DownLayer/Hud").GetComponent<CommonTWPos>(); 
        windowFilmCommon = transform.Find("DownLayer/WindowFilm").GetComponent<CommonTWPos>(); 
        colorCommon = transform.Find("DownLayer/Color").GetComponent<CommonTWPos>();

        //屏幕中贴图显示对象
        seatObj = transform.Find("UpLayer/UIBgCar/CarSeat").gameObject;
        footpadObj = transform.Find("UpLayer/UIBgCar/CarFootPad").gameObject;
        hubObj = transform.Find("UpLayer/UIBgCar/CarHub").gameObject;
        windowFilmObj = transform.Find("UpLayer/UIBgCar/CarWindowFilm").gameObject;
        navigationObj = transform.Find("UpLayer/UIBgCar/CarNavigation").gameObject;
    }

    void Start()
    {
        carColor.onClick.AddListener(delegate { OnClickBottomLayer(color, colorCommon, false); });
        carHub.onClick.AddListener(delegate { OnClickBottomLayer(hub, hubCommon, true, hubObj); });
        carFootPad.onClick.AddListener(delegate { OnClickBottomLayer(footpad, footpadCommon, true, footpadObj); });
        carWindowFilm.onClick.AddListener(delegate { OnClickBottomLayer(windowFilm, windowFilmCommon, true, windowFilmObj); });
        carNavigation.onClick.AddListener(delegate { OnClickBottomLayer(windowFilm, windowFilmCommon, true, navigationObj); });
        carSeat.onClick.AddListener(delegate { OnClickBottomLayer(seat, seatCommon, true, seatObj); });

        seat.onClick.AddListener(delegate { OnClickDownLayer(seat, seatCommon); });
        footpad.onClick.AddListener(delegate { OnClickDownLayer(footpad, footpadCommon); });
        hub.onClick.AddListener(delegate { OnClickDownLayer(hub, hubCommon); });
        windowFilm.onClick.AddListener(delegate { OnClickDownLayer(windowFilm, windowFilmCommon); });
        color.onClick.AddListener(delegate { OnClickDownLayer(color, colorCommon); });
    }

    void Update()
    {

        if (preClickObj)
        {
            if (!preClickObj.activeSelf)
            {
                mainMenuCommon.TWBack();
                if(preClickObj == seatObj || preClickObj == footpadObj) //如果当前显示的中间窗口贴图为座椅与脚垫，隐藏时需要执行反向动画
                {
                    preClickBtn.onClick.Invoke();
                }
                preClickObj = null;
            }

        }
    }

    //bottomLayer上的按钮被点击后调用
    private void OnClickBottomLayer(Button downLayerBtn,CommonTWPos commonTW,bool isAddToList,GameObject centerTexObj = null )
    {

        //车身色判断
        if (downLayerBtn == color)
        {
            commonTW.TWForward();
            mainMenuCommon.TWForward();
            return;
        }

        //如果前一个按钮还在列表中，表示用户未手动点击DownLayer，需要程序执行DownLayer动画与隐藏前一个按钮对应的中间窗口贴图
        if (carBtnList.Contains(preClickBtn))
        {
            //执行DownLayer动画
            preClickBtn.transform.parent.GetComponent<CommonTWPos>().TWBack();

            //如果前一个按钮为带动画的座椅和脚垫，需要程序执行反向动画
            if (preClickBtn == seat || preClickBtn == footpad)
                preClickBtn.onClick.Invoke();

            carBtnList.Remove(preClickBtn);
            preClickBtn = null;
        }

        //隐藏前一个按钮对应的中间窗口贴图
        if (centerTexList.Contains(preClickObj))
        {
            if (preClickObj.activeSelf)//如果未手动隐藏
            {
                preClickObj.SetActive(false);
            }
            centerTexList.Remove(preClickObj);
            //preClickObj = null;
        }
        if (isAddToList) //如果不显示中间窗口贴图的按钮则不需要加入链表中
        {
            carBtnList.Add(downLayerBtn);
            preClickBtn = downLayerBtn;
        }
        //将当前中间贴图对象加入list
        if (centerTexObj != null)
        {
            centerTexList.Add(centerTexObj);
            preClickObj = centerTexObj;
            centerTexObj.SetActive(true);
        }

        mainMenuCommon.TWForward();
        //commonTW.TWForward();  //需要判断是否车身色
      

    }

    private void OnClickDownLayer(Button downLayerBtn,CommonTWPos commonTW)
    {
        if (carBtnList.Contains(downLayerBtn))
        {
            carBtnList.Remove(downLayerBtn);
            preClickBtn = null;
        }
        mainMenuCommon.TWBack();
        commonTW.TWBack();

        if (centerTexList.Count > 0)
        {
            for(int i =0; i<centerTexList.Count; ++i)
            {
                centerTexList[i].SetActive(false);
                centerTexList.RemoveAt(i);
            }
        }
    }

    //private void AutoClickClose(Button closeBtn)
    //{
    //    if (preClickBtn != null)
    //    {
    //        ExecuteEvents.Execute(preClickBtn.gameObject, pointer, ExecuteEvents.pointerClickHandler);
    //    }
    //    preClickBtn = closeBtn;        
    //}
    //private void AutoClick(Button btn)
    //{
    //    ExecuteEvents.Execute(btn.gameObject, pointer, ExecuteEvents.pointerClickHandler);
    //}

    //public void OnClick1(RectTransform trans)
    //{
    //    //Debug.Log("switch");
    //    switch (trans.name)
    //    {
    //        case "CarSeat":
    //            //Debug.Log("123");
    //            seat.SetActive(true);
    //            if (preClickBtn != null)
    //                preClickBtn.SetActive(false);
    //            preClickBtn = seat.gameObject;
    //            break;
    //        case "CarFootPad":
    //            footpad.SetActive(true);
    //            if (preClickBtn != null)
    //                preClickBtn.SetActive(false);
    //            preClickBtn = footpad.gameObject;
    //            break;
    //        case "CarHub":
    //            hub.SetActive(true);
    //            if (preClickBtn != null)
    //                preClickBtn.SetActive(false);
    //            preClickBtn = hub.gameObject;
    //            break;
    //        case "CarWindowFilm":
    //            windowFilm.SetActive(true);
    //            if (preClickBtn != null)
    //                preClickBtn.SetActive(false);
    //            preClickBtn = windowFilm.gameObject;
    //            break;
    //        case "CarNavigation":
    //            navigation.SetActive(true);
    //            if (preClickBtn != null)
    //                preClickBtn.SetActive(false);
    //            preClickBtn = navigation.gameObject;
    //            break;
    //        default:
    //            if (preClickBtn != null)
    //            {
    //                preClickBtn.SetActive(false);
    //                preClickBtn = null;
    //            }
    //            //Debug.Log("click");
    //            break;
    //    }
    //}

   


}
