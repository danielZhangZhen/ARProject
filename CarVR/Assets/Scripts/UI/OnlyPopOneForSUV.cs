using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class OnlyPopOneForSUV : MonoBehaviour {

    //MainMenu下的Button
    private Transform mainMenuGrid;
    private List<Button> carBtnList = new List<Button>();//存放DownLayer的退出Button
    private List<GameObject> centerTexList = new List<GameObject>();//存放中间窗口贴图对象
    
    //bottomLayer按钮
    private Button carColor;
    private Button carHub;          //轮毂
    private Button carFootPad;      //脚垫
    private Button carWindowFilm;   //隔热膜
    private Button carNavigation;
    private Button carPedal;           //踏板
    private Button carElectricPedal;    //电动踏板
    private Button carGuardPlate;   //护板
    private Button carSeat;

    private Button carExhouse;         //尾喉
    private Button carFender;         //挡泥板
    private Button carTrim;          //内饰
    private Button carFogLight;        //雾灯罩
    private Button carZhongWang;      //中网
    private Button carExhaust;        //排气管
    private Button carTaillight;         //尾灯
    private Button carFullshow360;    //360全景显示

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

    //中间窗口贴图对象 UIBgSUV
    private GameObject seatObj;
    private GameObject footpadObj;
    private GameObject hubObj;
    private GameObject windowFilmObj;
    private GameObject navigationObj;
    private GameObject pedalObj;
    private GameObject electricPedalObj;
    private GameObject guardPlateObj;

    private GameObject exhouseObj;         //尾喉
    private GameObject fenderObj;         //挡泥板
    private GameObject trimObj;          //内饰
    private GameObject fogLightObj;        //雾灯罩
    private GameObject zhongWangObj;      //中网
    private GameObject exhaustObj;        //排气管
    private GameObject taillightObj;         //尾灯
    private GameObject fullshow360Obj;    //360全景显示

    private Text showInfoText;

    #region GetSet属性
    //底部按钮BottomLayer
    private Text ShowInfoText
    {
        get
        {
            if (null == showInfoText)
                showInfoText = transform.Find("UpLayer/UIBgSUV/ShowInfoText/Text").GetComponent<Text>();
            return showInfoText;
        }

        set
        {
            showInfoText = value;
        }
    }
    private Button CarColor
    {
        get
        {
            if(null == carColor)
                carColor = mainMenuGrid.Find("CarColor").GetComponent<Button>();
            return carColor;
        }

        set
        {
            carColor = value;
        }
    }
    private Button CarHub
    {
        get
        {
            if (null == carHub)
                carHub = mainMenuGrid.Find("CarHub").GetComponent<Button>();
            return carHub;
        }

        set
        {
            carHub = value;
        }
    }
    private Button CarFootPad
    {
        get
        {
            if (null == carFootPad)
                carFootPad = mainMenuGrid.Find("CarFootPad").GetComponent<Button>();
            return carFootPad;
        }

        set
        {
            carFootPad = value;
        }
    }
    private Button CarWindowFilm
    {
        get
        {
            if (null == carWindowFilm)
                carWindowFilm = mainMenuGrid.Find("CarWindowFilm").GetComponent<Button>();
            return carWindowFilm;
        }

        set
        {
            carWindowFilm = value;
        }
    }
    private Button CarNavigation
    {
        get
        {
            if (null == carNavigation)
                carNavigation = mainMenuGrid.Find("CarNavigation").GetComponent<Button>();
            return carNavigation;
        }

        set
        {
            carNavigation = value;
        }
    }
    private Button CarPedal
    {
        get
        {
            if (null == carPedal)
                carPedal = mainMenuGrid.Find("CarPedal").GetComponent<Button>();
            return carPedal;
        }

        set
        {
            carPedal = value;
        }
    }
    private Button CarElectricPedal
    {
        get
        {
            if (null == carElectricPedal)
                carElectricPedal = mainMenuGrid.Find("CarElectricPedal").GetComponent<Button>();
            return carElectricPedal;
        }

        set
        {
            carElectricPedal = value;
        }
    }
    private Button CarGuardPlate
    {
        get
        {
            if (null == carGuardPlate)
                carGuardPlate = mainMenuGrid.Find("CarGuardPlate").GetComponent<Button>();
            return carGuardPlate;
        }

        set
        {
            carGuardPlate = value;
        }
    }
    private Button CarSeat
    {
        get
        {
            if (null == carSeat)
                carSeat = mainMenuGrid.Find("CarSeat").GetComponent<Button>();
            return carSeat;
        }

        set
        {
            carSeat = value;
        }
    }
    private Button CarExhouse
    {
        get
        {
            if (null == carExhouse)
                carExhouse = mainMenuGrid.Find("CarExhouse").GetComponent<Button>();
            return carExhouse;
        }

        set
        {
            carExhouse = value;
        }
    }
    private Button CarFender
    {
        get
        {
            if (null == carFender)
                carFender = mainMenuGrid.Find("CarFender").GetComponent<Button>();
            return carFender;
        }

        set
        {
            carFender = value;
        }
    }
    private Button CarTrim
    {
        get
        {
            if (null == carTrim)
                carTrim = mainMenuGrid.Find("CarTrim").GetComponent<Button>();
            return carTrim;
        }

        set
        {
            carTrim = value;
        }
    }
    private Button CarFogLight
    {
        get
        {
            if (null == carFogLight)
                carFogLight = mainMenuGrid.Find("CarFogLight").GetComponent<Button>();
            return carFogLight;
        }

        set
        {
            carFogLight = value;
        }
    }
    private Button CarZhongWang
    {
        get
        {
            if (null == carZhongWang)
                carZhongWang = mainMenuGrid.Find("CarZhongWang").GetComponent<Button>();
            return carZhongWang;
        }

        set
        {
            carZhongWang = value;
        }
    }
    private Button CarExhaust
    {
        get
        {
            if (null == carExhaust)
                carExhaust = mainMenuGrid.Find("CarExhaust").GetComponent<Button>();
            return carExhaust;
        }

        set
        {
            carExhaust = value;
        }
    }
    private Button CarTaillight
    {
        get
        {
            if (null == carTaillight)
                carTaillight = mainMenuGrid.Find("CarTaillight").GetComponent<Button>();
            return carTaillight;
        }

        set
        {
            carTaillight = value;
        }
    }
    private Button CarFullshow360
    {
        get
        {
            if (null == carFullshow360)
                carFullshow360 = mainMenuGrid.Find("CarFullshow360").GetComponent<Button>();
            return carFullshow360;
        }

        set
        {
            carFullshow360 = value;
        }
    }

    //视野外的退出按钮 DownLayer Button
    private Button Seat
    {
        get
        {
            if (null == seat)
                seat = transform.Find("DownLayer/Seat/1back").GetComponent<Button>();
            return seat;
        }

        set
        {
            seat = value;
        }
    }
    private Button Footpad
    {
        get
        {
            if (null == footpad)
                footpad = transform.Find("DownLayer/FootPad/0back").GetComponent<Button>();
            return footpad;
        }

        set
        {
            footpad = value;
        }
    }
    private Button Hub
    {
        get
        {
            if (null == hub)
                hub = transform.Find("DownLayer/Hud/back").GetComponent<Button>();
            return hub;
        }

        set
        {
            hub = value;
        }
    }
    private Button WindowFilm
    {
        get
        {
            if (null == windowFilm)
                windowFilm = transform.Find("DownLayer/WindowFilm/back").GetComponent<Button>();
            return windowFilm;
        }

        set
        {
            windowFilm = value;
        }
    }
    private Button Color
    {
        get
        {
            if(null == color)
                color = transform.Find("DownLayer/Color/back").GetComponent<Button>();
            return color;
        }

        set
        {
            color = value;
        }
    }

    //底部及视野外动画组件获取 DownLayer Tween
    private CommonTWPos SeatCommon
    {
        get
        {
            if (null == seatCommon)
                seatCommon = transform.Find("DownLayer/Seat").GetComponent<CommonTWPos>();
            return seatCommon;
        }

        set
        {
            seatCommon = value;
        }
    }
    private CommonTWPos FootpadCommon
    {
        get
        {
            if (null == footpadCommon)
                footpadCommon = transform.Find("DownLayer/FootPad").GetComponent<CommonTWPos>();
            return footpadCommon;
        }

        set
        {
            footpadCommon = value;
        }
    }
    private CommonTWPos HubCommon
    {
        get
        {
            if (null == hubCommon)
                hubCommon = transform.Find("DownLayer/Hud").GetComponent<CommonTWPos>();
            return hubCommon;
        }

        set
        {
            hubCommon = value;
        }
    }
    private CommonTWPos WindowFilmCommon
    {
        get
        {
            if (null == windowFilmCommon)
                windowFilmCommon = transform.Find("DownLayer/WindowFilm").GetComponent<CommonTWPos>();
            return windowFilmCommon;
        }

        set
        {
            windowFilmCommon = value;
        }
    }
    private CommonTWPos ColorCommon
    {
        get
        {
            if(null== colorCommon)
                colorCommon = transform.Find("DownLayer/Color").GetComponent<CommonTWPos>();
            return colorCommon;
        }

        set
        {
            colorCommon = value;
        }
    }

    //屏幕中贴图显示对象  UIBgSUV
    private GameObject SeatObj
    {
        get
        {
            if (null == seatObj)
                seatObj = transform.Find("UpLayer/UIBgSUV/CarSeat").gameObject;
            return seatObj;
        }

        set
        {
            seatObj = value;
        }
    }
    private GameObject FootpadObj
    {
        get
        {
            if (null == footpadObj)
                footpadObj = transform.Find("UpLayer/UIBgSUV/CarFootPad").gameObject;
            return footpadObj;
        }

        set
        {
            footpadObj = value;
        }
    }
    private GameObject HubObj
    {
        get
        {
            if (null == hubObj)
                hubObj = transform.Find("UpLayer/UIBgSUV/CarHub").gameObject;
            return hubObj;
        }

        set
        {
            hubObj = value;
        }
    }
    private GameObject WindowFilmObj
    {
        get
        {
            if (null == windowFilmObj)
                windowFilmObj = transform.Find("UpLayer/UIBgSUV/CarWindowFilm").gameObject;
            return windowFilmObj;
        }

        set
        {
            windowFilmObj = value;
        }
    }
    private GameObject NavigationObj
    {
        get
        {
            if (null == navigationObj)
                navigationObj = transform.Find("UpLayer/UIBgSUV/CarNavigation").gameObject;
            return navigationObj;
        }

        set
        {
            navigationObj = value;
        }
    }
    private GameObject PedalObj
    {
        get
        {
            if (null == pedalObj)
                pedalObj = transform.Find("UpLayer/UIBgSUV/CarPedal").gameObject;
            return pedalObj;
        }

        set
        {
            pedalObj = value;
        }
    }
    private GameObject ElectricPedalObj
    {
        get
        {
            if (null == electricPedalObj)
                electricPedalObj = transform.Find("UpLayer/UIBgSUV/CarElectricPedal").gameObject;
            return electricPedalObj;
        }

        set
        {
            electricPedalObj = value;
        }
    }
    private GameObject GuardPlateObj
    {
        get
        {
            if (null == guardPlateObj)
                guardPlateObj = transform.Find("UpLayer/UIBgSUV/CarGuardPlate").gameObject;
            return guardPlateObj;
        }

        set
        {
            guardPlateObj = value;
        }
    }
    private GameObject ExhouseObj
    {
        get
        {
            if (null == exhouseObj)
                exhouseObj = transform.Find("UpLayer/UIBgSUV/CarExhouse").gameObject;
            return exhouseObj;
        }

        set
        {
            exhouseObj = value;
        }
    }
    private GameObject FenderObj
    {
        get
        {
            if (null == fenderObj)
                fenderObj = transform.Find("UpLayer/UIBgSUV/CarFender").gameObject;
            return fenderObj;
        }

        set
        {
            fenderObj = value;
        }
    }
    private GameObject TrimObj
    {
        get
        {
            if (null == trimObj)
                trimObj = transform.Find("UpLayer/UIBgSUV/CarTrim").gameObject;
            return trimObj;
        }

        set
        {
            trimObj = value;
        }
    }
    private GameObject FogLightObj
    {
        get
        {
            if (null == fogLightObj)
                fogLightObj = transform.Find("UpLayer/UIBgSUV/CarFogLight").gameObject;
            return fogLightObj;
        }

        set
        {
            fogLightObj = value;
        }
    }
    private GameObject ZhongWangObj
    {
        get
        {
            if (null == zhongWangObj)
                zhongWangObj = transform.Find("UpLayer/UIBgSUV/CarZhongWang").gameObject;
            return zhongWangObj;
        }

        set
        {
            zhongWangObj = value;
        }
    }
    private GameObject ExhaustObj
    {
        get
        {
            if (null == exhaustObj)
                exhaustObj = transform.Find("UpLayer/UIBgSUV/CarExhaust").gameObject;
            return exhaustObj;
        }

        set
        {
            exhaustObj = value;
        }
    }
    private GameObject TaillightObj
    {
        get
        {
            if (null == taillightObj)
                taillightObj = transform.Find("UpLayer/UIBgSUV/CarTaillight").gameObject;
            return taillightObj;
        }

        set
        {
            taillightObj = value;
        }
    }
    private GameObject Fullshow360Obj
    {
        get
        {
            if(null == fullshow360Obj)
                fullshow360Obj = transform.Find("UpLayer/UIBgSUV/CarFullshow360").gameObject;
            return fullshow360Obj;
        }

        set
        {
            fullshow360Obj = value;
        }
    }

    #endregion

    void Awake()
    {
        
        
        //屏幕中贴图显示对象  UIBgSUV
        //SeatObj = transform.Find("UpLayer/UIBgSUV/CarSeat").gameObject;
        //FootpadObj = transform.Find("UpLayer/UIBgSUV/CarFootPad").gameObject;
        //HubObj = transform.Find("UpLayer/UIBgSUV/CarHub").gameObject;
        //WindowFilmObj = transform.Find("UpLayer/UIBgSUV/CarWindowFilm").gameObject;
        //NavigationObj = transform.Find("UpLayer/UIBgSUV/CarNavigation").gameObject;
        //PedalObj = transform.Find("UpLayer/UIBgSUV/CarPedal").gameObject;
        //ElectricPedalObj = transform.Find("UpLayer/UIBgSUV/CarElectricPedal").gameObject;
        //GuardPlateObj = transform.Find("UpLayer/UIBgSUV/CarGuardPlate").gameObject;

        //ExhouseObj = transform.Find("UpLayer/UIBgSUV/CarExhouse").gameObject;
        //FenderObj = transform.Find("UpLayer/UIBgSUV/CarFender").gameObject;
        //TrimObj = transform.Find("UpLayer/UIBgSUV/CarTrim").gameObject;
        //FogLightObj = transform.Find("UpLayer/UIBgSUV/CarFogLight").gameObject;
        //ZhongWangObj = transform.Find("UpLayer/UIBgSUV/CarZhongWang").gameObject;
        //ExhaustObj = transform.Find("UpLayer/UIBgSUV/CarExhaust").gameObject;
        //TaillightObj = transform.Find("UpLayer/UIBgSUV/CarTaillight").gameObject;
        //Fullshow360Obj = transform.Find("UpLayer/UIBgSUV/CarFullshow360").gameObject;
        //底部按钮bottomLayer
        mainMenuGrid = transform.Find("UpLayer/MainBg/MainMenu/Grid");
        //CarColor = mainMenuGrid.Find("CarColor").GetComponent<Button>();
        //CarHub = mainMenuGrid.Find("CarHub").GetComponent<Button>();
        //CarFootPad = mainMenuGrid.Find("CarFootPad").GetComponent<Button>();
        //CarWindowFilm = mainMenuGrid.Find("CarWindowFilm").GetComponent<Button>();
        //CarNavigation = mainMenuGrid.Find("CarNavigation").GetComponent<Button>();
        //CarPedal = mainMenuGrid.Find("CarPedal").GetComponent<Button>();
        //CarElectricPedal = mainMenuGrid.Find("CarElectricPedal").GetComponent<Button>();
        //CarGuardPlate = mainMenuGrid.Find("CarGuardPlate").GetComponent<Button>();
        //CarSeat = mainMenuGrid.Find("CarSeat").GetComponent<Button>();

        //CarExhouse = mainMenuGrid.Find("CarExhouse").GetComponent<Button>();
        //CarFender = mainMenuGrid.Find("CarFender").GetComponent<Button>();
        //CarTrim = mainMenuGrid.Find("CarTrim").GetComponent<Button>();
        //CarFogLight = mainMenuGrid.Find("CarFogLight").GetComponent<Button>();
        //CarZhongWang = mainMenuGrid.Find("CarZhongWang").GetComponent<Button>();
        //CarExhaust = mainMenuGrid.Find("CarExhaust").GetComponent<Button>();
        //CarTaillight = mainMenuGrid.Find("CarTaillight").GetComponent<Button>();
        //CarFullshow360 = mainMenuGrid.Find("CarFullshow360").GetComponent<Button>();

        //视野外的退出按钮 DownLayer Button
        //Seat = transform.Find("DownLayer/Seat/1back").GetComponent<Button>();
        //Footpad = transform.Find("DownLayer/FootPad/0back").GetComponent<Button>();
        //Hub = transform.Find("DownLayer/Hud/back").GetComponent<Button>();
        //WindowFilm = transform.Find("DownLayer/WindowFilm/back").GetComponent<Button>();
        //Color = transform.Find("DownLayer/Color/back").GetComponent<Button>();

        //底部及视野外动画组件获取 DownLayer Tween
        mainMenuCommon = mainMenuGrid.parent.parent.GetComponent<CommonTWPos>();
        //SeatCommon = transform.Find("DownLayer/Seat").GetComponent<CommonTWPos>();
        //FootpadCommon = transform.Find("DownLayer/FootPad").GetComponent<CommonTWPos>();
        //HubCommon = transform.Find("DownLayer/Hud").GetComponent<CommonTWPos>();
        //WindowFilmCommon = transform.Find("DownLayer/WindowFilm").GetComponent<CommonTWPos>();
        //ColorCommon = transform.Find("DownLayer/Color").GetComponent<CommonTWPos>();


    }

    void Start()
    {
        Init();
        //CarColor.onClick.AddListener(delegate { OnClickBottomLayer(Color, ColorCommon, false); });
        //CarHub.onClick.AddListener(delegate { OnClickBottomLayer(Hub, HubCommon, true, HubObj,AutoLoadSpriteForSUV._instance.lunguNum ); });
        //CarFootPad.onClick.AddListener(delegate { OnClickBottomLayer(Footpad, FootpadCommon, true, FootpadObj, AutoLoadSpriteForSUV._instance.jiaodianNum); });
        //CarWindowFilm.onClick.AddListener(delegate { OnClickBottomLayer(WindowFilm, WindowFilmCommon, true, WindowFilmObj, AutoLoadSpriteForSUV._instance.windowFilmNum); });
        //CarNavigation.onClick.AddListener(delegate { OnClickBottomLayer(WindowFilm, WindowFilmCommon, true, NavigationObj, AutoLoadSpriteForSUV._instance.navigationNum); });
        //CarSeat.onClick.AddListener(delegate { OnClickBottomLayer(Seat, SeatCommon, true, SeatObj, AutoLoadSpriteForSUV._instance.zuoyiNum); });
        //CarPedal.onClick.AddListener( delegate { OnClickBottomLayer(Hub, HubCommon, true, PedalObj, AutoLoadSpriteForSUV._instance.pedalNum); });
        //CarElectricPedal.onClick.AddListener(delegate { OnClickBottomLayer(Hub, HubCommon, true, ElectricPedalObj, AutoLoadSpriteForSUV._instance.electricPedalNum); });
        //CarGuardPlate.onClick.AddListener(delegate { OnClickBottomLayer(Hub, HubCommon, true, GuardPlateObj, AutoLoadSpriteForSUV._instance.guardPlateNum); });

        ////主要填写后两个参数，前两个参数的效果不需要了
        //CarExhouse.onClick.AddListener(delegate { OnClickBottomLayer(Hub, HubCommon, true, ExhouseObj, AutoLoadSpriteForSUV._instance.exhouseNum); });
        //CarFender.onClick.AddListener(delegate { OnClickBottomLayer(Hub, HubCommon, true, FenderObj, AutoLoadSpriteForSUV._instance.fenderNum); });
        //CarTrim.onClick.AddListener(delegate { OnClickBottomLayer(Hub, HubCommon, true, TrimObj, AutoLoadSpriteForSUV._instance.trimNum); });
        //CarFogLight.onClick.AddListener(delegate { OnClickBottomLayer(Hub, HubCommon, true, FogLightObj, AutoLoadSpriteForSUV._instance.fogLightNum); });
        //CarZhongWang.onClick.AddListener(delegate { OnClickBottomLayer(Hub, HubCommon, true, ZhongWangObj, AutoLoadSpriteForSUV._instance.zhongWangNum); });
        //CarExhaust.onClick.AddListener(delegate { OnClickBottomLayer(Hub, HubCommon, true, ExhaustObj, AutoLoadSpriteForSUV._instance.exhaustNum); });
        //CarTaillight.onClick.AddListener(delegate { OnClickBottomLayer(Hub, HubCommon, true, TaillightObj, AutoLoadSpriteForSUV._instance.taillightNum); });
        //CarFullshow360.onClick.AddListener(delegate { OnClickBottomLayer(Hub, HubCommon, true, Fullshow360Obj, AutoLoadSpriteForSUV._instance.fullshow360Num); });



        Seat.onClick.AddListener(delegate { OnClickDownLayer(Seat, SeatCommon); });
        Footpad.onClick.AddListener(delegate { OnClickDownLayer(Footpad, FootpadCommon); });
        Hub.onClick.AddListener(delegate { OnClickDownLayer(Hub, HubCommon); });
        WindowFilm.onClick.AddListener(delegate { OnClickDownLayer(WindowFilm, WindowFilmCommon); });
        Color.onClick.AddListener(delegate { OnClickDownLayer(Color, ColorCommon); });
    }

    void Update()
    {

        if (preClickObj)
        {
            if (!preClickObj.activeSelf)
            {
                mainMenuCommon.TWBack();
                if (preClickObj == SeatObj || preClickObj == FootpadObj) //如果当前显示的中间窗口贴图为座椅与脚垫，隐藏时需要执行反向动画
                {
                    preClickBtn.onClick.Invoke();
                }
                preClickObj = null;
            }

        }
    }

    /// <summary>
    /// 给按钮添加注册事件
    /// </summary>
    void Init()
    {
        //Button[] btns =  mainMenuGrid.GetComponentsInChildren<Button>();
        int childCount = mainMenuGrid.childCount;
        if (childCount > 0)
        {
            for (int i = 0; i < childCount; ++i)
            {
                switch (mainMenuGrid.GetChild(i).name)
                {
                    case "CarColor":
                        CarColor.onClick.AddListener(delegate { OnClickBottomLayer(Color, ColorCommon, false); });
                        break;
                    case "CarHub":
                        CarHub.onClick.AddListener(delegate { OnClickBottomLayer(Hub, HubCommon, true, HubObj, AutoLoadSpriteForSUV._instance.lunguNum); });
                        break;
                    case "CarFootPad":
                        CarFootPad.onClick.AddListener(delegate { OnClickBottomLayer(Footpad, FootpadCommon, true, FootpadObj, AutoLoadSpriteForSUV._instance.jiaodianNum); });
                        break;
                    case "CarWindowFilm":
                        CarWindowFilm.onClick.AddListener(delegate { OnClickBottomLayer(WindowFilm, WindowFilmCommon, true, WindowFilmObj, AutoLoadSpriteForSUV._instance.windowFilmNum); });
                        break;
                    case "CarNavigation":
                        CarNavigation.onClick.AddListener(delegate { OnClickBottomLayer(WindowFilm, WindowFilmCommon, true, NavigationObj, AutoLoadSpriteForSUV._instance.navigationNum); });
                        break;
                    case "CarSeat":
                        CarSeat.onClick.AddListener(delegate { OnClickBottomLayer(Seat, SeatCommon, true, SeatObj, AutoLoadSpriteForSUV._instance.zuoyiNum); });
                        break;
                    case "CarPedal":
                        CarPedal.onClick.AddListener(delegate { OnClickBottomLayer(Hub, HubCommon, true, PedalObj, AutoLoadSpriteForSUV._instance.pedalNum); });
                        break;
                    case "CarElectricPedal":
                        CarElectricPedal.onClick.AddListener(delegate { OnClickBottomLayer(Hub, HubCommon, true, ElectricPedalObj, AutoLoadSpriteForSUV._instance.electricPedalNum); });
                        break;
                    case "CarGuardPlate":
                        CarGuardPlate.onClick.AddListener(delegate { OnClickBottomLayer(Hub, HubCommon, true, GuardPlateObj, AutoLoadSpriteForSUV._instance.guardPlateNum); });
                        break;
                    case "CarExhouse":
                        CarExhouse.onClick.AddListener(delegate { OnClickBottomLayer(Hub, HubCommon, true, ExhouseObj, AutoLoadSpriteForSUV._instance.exhouseNum); });
                        break;
                    case "CarFender":
                        CarFender.onClick.AddListener(delegate { OnClickBottomLayer(Hub, HubCommon, true, FenderObj, AutoLoadSpriteForSUV._instance.fenderNum); });
                        break;
                    case "CarTrim":
                        CarTrim.onClick.AddListener(delegate { OnClickBottomLayer(Hub, HubCommon, true, TrimObj, AutoLoadSpriteForSUV._instance.trimNum); });
                        break;
                    case "CarFogLight":
                        CarFogLight.onClick.AddListener(delegate { OnClickBottomLayer(Hub, HubCommon, true, FogLightObj, AutoLoadSpriteForSUV._instance.fogLightNum); });
                        break;
                    case "CarZhongWang":
                        CarZhongWang.onClick.AddListener(delegate { OnClickBottomLayer(Hub, HubCommon, true, ZhongWangObj, AutoLoadSpriteForSUV._instance.zhongWangNum); });
                        break;
                    case "CarExhaust":
                        CarExhaust.onClick.AddListener(delegate { OnClickBottomLayer(Hub, HubCommon, true, ExhaustObj, AutoLoadSpriteForSUV._instance.exhaustNum); });
                        break;
                    case "CarTaillight":
                        CarTaillight.onClick.AddListener(delegate { OnClickBottomLayer(Hub, HubCommon, true, TaillightObj, AutoLoadSpriteForSUV._instance.taillightNum); });
                        break;
                    case "CarFullshow360":
                        CarFullshow360.onClick.AddListener(delegate { OnClickBottomLayer(Hub, HubCommon, true, Fullshow360Obj, AutoLoadSpriteForSUV._instance.fullshow360Num); });
                        break;
                }
            }
        }
    }

    /// <summary>
    /// //bottomLayer上的按钮被点击后调用
    /// </summary>
    /// <param name="downLayerBtn"></param> 隐藏在屏幕下的退出按钮，目前只有 车身色按钮 有用到
    /// <param name="commonTW"></param> 用于播放点击 车身色按钮 后的动画
    /// <param name="isAddToList"></param> 是否添加到显示/隐藏逻辑的判断中去
    /// <param name="centerTexObj"></param> 表示在中间隐藏/显示的贴图对象
    /// <param name="num"></param> 表示AutoLoadSpriteForSUV脚本内 读取的Num数量，数量为0表示没有贴图。
    private void OnClickBottomLayer(Button downLayerBtn,CommonTWPos commonTW,bool isAddToList,GameObject centerTexObj = null,int num = 0 )
    {
        //车身色判断
        if (downLayerBtn == Color)
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
            if (preClickBtn == Seat || preClickBtn == Footpad)
            {
                preClickBtn.onClick.Invoke();
            }

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
        if (isAddToList)
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

            ShowInfoText.color = UnityEngine.Color.white;
            //如果没读取到中间贴图
            if (num == 0)
            {
                ShowInfoText.text = "没有此类资源！";
                ShowInfoText.color = UnityEngine.Color.red;
            }
        }

        mainMenuCommon.TWForward();
        //commonTW.TWForward();
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
