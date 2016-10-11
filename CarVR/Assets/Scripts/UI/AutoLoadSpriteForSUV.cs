//**********************************************************
//** 姓名 ：林宇敬
//** 日期 ：2016年03月22日
//** 类作用： 读取Resources/UI/Sprite目录下资源，
//**          根据图标名称加载到屏幕UI上；
//**********************************************************
using UnityEngine;
using System.Collections;
using System.Globalization;
using UnityEngine.UI;
public  class AutoLoadSpriteForSUV : MonoBehaviour

{
    //public GameObject allScriptsObj;
    public static AutoLoadSpriteForSUV _instance;

    public GameObject togglePrefab;
    public string currentCarModel = "";//当前型号

  
    private RectTransform seat;//座椅
	private RectTransform footpad;//脚垫
	private RectTransform hub;//轮毂
    private RectTransform navigation;//导航
    private RectTransform windowFilm; //太阳膜
    private RectTransform pedal;    //踏板
    private RectTransform electricPedal;   //电动踏板
    private RectTransform guardPlate;  //护板

    private RectTransform seatToggle;
    private RectTransform footpadToggle;
    private RectTransform hubToggle;
    private RectTransform navigationToggle;
    private RectTransform windowFilmToggle;
    private RectTransform pedalToggle;    
    private RectTransform electricPedalToggle;   
    private RectTransform guardPlateToggle;
    
    private RectTransform exhouse;         //尾喉
    private RectTransform fender;         //挡泥板
    private RectTransform trim;          //内饰
    private RectTransform fogLight;        //雾灯罩
    private RectTransform zhongWang;      //中网
    private RectTransform exhaust;        //排气管
    private RectTransform taillight;         //尾灯
    private RectTransform fullshow360;    //360全景显示

    private RectTransform exhouseToggle;         //尾喉
    private RectTransform fenderToggle;         //挡泥板
    private RectTransform trimToggle;          //内饰
    private RectTransform fogLightToggle;        //雾灯罩
    private RectTransform zhongWangToggle;      //中网
    private RectTransform exhaustToggle;        //排气管
    private RectTransform taillightToggle;         //尾灯
    private RectTransform fullshow360Toggle;    //360全景显示

    [HideInInspector]
    public  int zuoyiNum = 0,   //座椅
        jiaodianNum = 0,        //脚垫
        lunguNum = 0,       //轮毂
        navigationNum = 0,      //导航
        windowFilmNum = 0,      //隔热膜、太阳膜
        pedalNum = 0,           //踏板
        electricPedalNum = 0,   //电动踏板
        guardPlateNum = 0,      //护板
        exhouseNum = 0,         //尾喉
        fenderNum = 0,          //挡泥板
        trimNum = 0,            //内饰
        fogLightNum = 0,        //雾灯罩
        zhongWangNum = 0,       //中网
        exhaustNum = 0,         //排气管
        taillightNum = 0,          //尾灯
        fullshow360Num = 0;     //360全景显示

    #region 获取Grid与Toggle
    private RectTransform Seat
    {
        get
        {
            if(null == seat)
                seat = transform.FindChild("CarSeat/Scroll/Grid").GetComponent<RectTransform>();
            return seat;
        }
    }
    private RectTransform Footpad
    {
        get
        {
            if(null == footpad)
                footpad = transform.FindChild("CarFootPad/Scroll/Grid").GetComponent<RectTransform>();
            return footpad;
        }
    }
    private RectTransform Hub
    {
        get
        {
            if (null == hub)
                hub = transform.FindChild("CarHub/Scroll/Grid").GetComponent<RectTransform>();
            return hub;
        }
    }
    private RectTransform Navigation
    {
        get
        {
            if (null == navigation)
                navigation = transform.FindChild("CarNavigation/Scroll/Grid").GetComponent<RectTransform>();
            return navigation;
        }
    }
    private RectTransform WindowFilm
    {
        get
        {
            if (null == windowFilm)
                windowFilm = transform.FindChild("CarWindowFilm/Scroll/Grid").GetComponent<RectTransform>();
            return windowFilm;
        }
    }
    private RectTransform Pedal
    {
        get
        {
            if (null == pedal)
                pedal = transform.FindChild("CarPedal/Scroll/Grid").GetComponent<RectTransform>();
            return pedal;
        }
    }
    private RectTransform ElectricPedal
    {
        get
        {
            if (null == electricPedal)
                electricPedal = transform.FindChild("CarElectricPedal/Scroll/Grid").GetComponent<RectTransform>();
            return electricPedal;
        }
    }
    private RectTransform GuardPlate
    {
        get
        {
            if (null == guardPlate)
                guardPlate = transform.FindChild("CarGuardPlate/Scroll/Grid").GetComponent<RectTransform>();
            return guardPlate;
        }
    }
    private RectTransform SeatToggle
    {
        get
        {
            if (null == seatToggle)
                seatToggle = transform.FindChild("CarSeat/Toggle").GetComponent<RectTransform>();
            return seatToggle;
        }
    }
    private RectTransform FootpadToggle
    {
        get
        {
            if (null == footpadToggle)
                footpadToggle = transform.FindChild("CarFootPad/Toggle").GetComponent<RectTransform>();
            return footpadToggle;
        }
    }
    private RectTransform HubToggle
    {
        get
        {
            if (null == hubToggle)
                hubToggle = transform.FindChild("CarHub/Toggle").GetComponent<RectTransform>();
            return hubToggle;
        }
    }
    private RectTransform NavigationToggle
    {
        get
        {
            if (null == navigationToggle)
                navigationToggle = transform.FindChild("CarNavigation/Toggle").GetComponent<RectTransform>();
            return navigationToggle;
        }
    }
    private RectTransform WindowFilmToggle
    {
        get
        {
            if (null == windowFilmToggle)
                windowFilmToggle = transform.FindChild("CarWindowFilm/Toggle").GetComponent<RectTransform>();
            return windowFilmToggle;
        }
    }
    private RectTransform PedalToggle
    {
        get
        {
            if (null == pedalToggle)
                pedalToggle = transform.FindChild("CarPedal/Toggle").GetComponent<RectTransform>();
            return pedalToggle;
        }
    }
    private RectTransform ElectricPedalToggle
    {
        get
        {
            if (null == electricPedalToggle)
                electricPedalToggle = transform.FindChild("CarElectricPedal/Toggle").GetComponent<RectTransform>();
            return electricPedalToggle;
        }
    }
    private RectTransform GuardPlateToggle
    {
        get
        {
            if (null == guardPlateToggle)
                guardPlateToggle = transform.FindChild("CarGuardPlate/Toggle").GetComponent<RectTransform>();
            return guardPlateToggle;
        }
    }

    private RectTransform Exhouse
    {
        get
        {
            if (null == exhouse)
                exhouse = transform.FindChild("CarExhouse/Scroll/Grid").GetComponent<RectTransform>();
            return exhouse;
        }
    }
    private RectTransform ExhouseToggle
    {
        get
        {
            if (null == exhouseToggle)
                exhouseToggle = transform.FindChild("CarExhouse/Toggle").GetComponent<RectTransform>();
            return exhouseToggle;
        }
    }
    private RectTransform Fender
    {
        get
        {
            if (null == fender)
                fender = transform.FindChild("CarFender/Scroll/Grid").GetComponent<RectTransform>();
            return fender;
        }
    }
    private RectTransform FenderToggle
    {
        get
        {
            if (null == fenderToggle)
                fenderToggle = transform.FindChild("CarFender/Toggle").GetComponent<RectTransform>();
            return fenderToggle;
        }
    }
    private RectTransform Trim
    {
        get
        {
            if (null == trim)
                trim = transform.FindChild("CarTrim/Scroll/Grid").GetComponent<RectTransform>();
            return trim;
        }
    }
    private RectTransform TrimToggle
    {
        get
        {
            if (null == trimToggle)
                trimToggle = transform.FindChild("CarTrim/Toggle").GetComponent<RectTransform>();
            return trimToggle;
        }
    }
    private RectTransform FogLight
    {
        get
        {
            if (null == fogLight)
                fogLight = transform.FindChild("CarFogLight/Scroll/Grid").GetComponent<RectTransform>();
            return fogLight;
        }
    }
    private RectTransform FogLightToggle
    {
        get
        {
            if (null == fogLightToggle)
                fogLightToggle = transform.FindChild("CarFogLight/Toggle").GetComponent<RectTransform>();
            return fogLightToggle;
        }
    }
    private RectTransform ZhongWang
    {
        get
        {
            if (null == zhongWang)
                zhongWang = transform.FindChild("CarZhongWang/Scroll/Grid").GetComponent<RectTransform>();
            return zhongWang;
        }
    }
    private RectTransform ZhongWangToggle
    {
        get
        {
            if (null == zhongWangToggle)
                zhongWangToggle = transform.FindChild("CarZhongWang/Toggle").GetComponent<RectTransform>();
            return zhongWangToggle;
        }
    }
    private RectTransform Exhaust
    {
        get
        {
            if (null == exhaust)
                exhaust = transform.FindChild("CarExhaust/Scroll/Grid").GetComponent<RectTransform>();
            return exhaust;
        }
    }
    private RectTransform ExhaustToggle
    {
        get
        {
            if (null == exhaustToggle)
                exhaustToggle = transform.FindChild("CarExhaust/Toggle").GetComponent<RectTransform>();
            return exhaustToggle;
        }
    }
    private RectTransform Taillight
    {
        get
        {
            if (null == taillight)
                taillight = transform.FindChild("CarTaillight/Scroll/Grid").GetComponent<RectTransform>();
            return taillight;
        }
    }
    private RectTransform TaillightToggle
    {
        get
        {
            if (null == taillightToggle)
                taillightToggle = transform.FindChild("CarTaillight/Toggle").GetComponent<RectTransform>();
            return taillightToggle;
        }
    }
    private RectTransform Fullshow360
    {
        get
        {
            if (null == fullshow360)
                fullshow360 = transform.FindChild("CarFullshow360/Scroll/Grid").GetComponent<RectTransform>();
            return fullshow360;
        }
    }
    private RectTransform Fullshow360Toggle
    {
        get
        {
            if (null == fullshow360Toggle)
                fullshow360Toggle = transform.FindChild("CarFullshow360/Toggle").GetComponent<RectTransform>();
            return fullshow360Toggle;
        }
    }


    #endregion

    void Awake()
    {
        _instance = this; 
       // seat = transform.FindChild("CarSeat/Scroll/Grid").GetComponent<RectTransform>();
       // seatToggle = transform.FindChild("CarSeat/Toggle").GetComponent<RectTransform>();
       // footpad = transform.FindChild("CarFootPad/Scroll/Grid").GetComponent<RectTransform>();
       // footpadToggle = transform.FindChild("CarFootPad/Toggle").GetComponent<RectTransform>();
       // hub = transform.FindChild("CarHub/Scroll/Grid").GetComponent<RectTransform>();
       // hubToggle = transform.FindChild("CarHub/Toggle").GetComponent<RectTransform>();
       // navigation = transform.FindChild("CarNavigation/Scroll/Grid").GetComponent<RectTransform>();
       //navigationToggle = transform.FindChild("CarNavigation/Toggle").GetComponent<RectTransform>();
       // windowFilm = transform.FindChild("CarWindowFilm/Scroll/Grid").GetComponent<RectTransform>();
       // windowFilmToggle = transform.FindChild("CarWindowFilm/Toggle").GetComponent<RectTransform>();

       // pedal = transform.FindChild("CarPedal/Scroll/Grid").GetComponent<RectTransform>();
       // pedalToggle = transform.FindChild("CarPedal/Toggle").GetComponent<RectTransform>();
       // electricPedal = transform.FindChild("CarElectricPedal/Scroll/Grid").GetComponent<RectTransform>();
       // electricPedalToggle = transform.FindChild("CarElectricPedal/Toggle").GetComponent<RectTransform>();
       // guardPlate = transform.FindChild("CarGuardPlate/Scroll/Grid").GetComponent<RectTransform>();
       // guardPlateToggle = transform.FindChild("CarGuardPlate/Toggle").GetComponent<RectTransform>();
    }
    void Start () 
	{
        MyLoadSprite();       
	}
    void MyLoadSprite()
    {
        GameObject[] goArr = Resources.LoadAll<GameObject>("UI/Sprite/texture");
        




        //Debug.Log("11");
        for (int i = 0; i < goArr.Length; i++)
        {
            string strName = goArr[i].name;
            string carModel = strName.Substring(0, 6);
            //当前读取模型为当前车模型
            if (string.Compare(strName.Substring(0, 2), currentCarModel) == 0)
            {
                string texName = strName.Substring(3, 5);
                //Debug.Log(texName);
                switch (texName)
                {
                    case "daoha"://导航
                        CreatItem(goArr[i], Navigation);
                        navigationNum++;
                        break;
                    case "jiaod"://脚垫
                        CreatItem(goArr[i], Footpad);
                        jiaodianNum++;
                        break;
                    case "quanp"://360全景屏幕显示系统
                        CreatItem(goArr[i], Fullshow360);
                        fullshow360Num++;
                        break;
                    case "weiho"://尾喉
                        CreatItem(goArr[i], Exhouse);
                        exhouseNum++;
                        break;
                    case "dangn"://挡泥板
                        CreatItem(goArr[i], Fender);
                        fenderNum++;
                        break;
                    case "neish"://内饰
                        CreatItem(goArr[i], Trim);
                        trimNum++;
                        break;
                    case "paiqi"://排气管
                        CreatItem(goArr[i], Exhaust);
                        exhaustNum++;
                        break;
                    case "weide"://尾灯
                        CreatItem(goArr[i], Taillight);
                        taillightNum++;
                        break;
                    case "zhong"://中网
                        CreatItem(goArr[i], ZhongWang);
                        zhongWangNum++;
                        break;
                    case "wuden"://雾灯罩
                        CreatItem(goArr[i], FogLight);
                        fogLightNum++;
                        break;
                }
            }
            #region  老需求的代码
            //    //所有车型一起使用   //修改之前的代码
            //if (string.Compare(strName.Substring(0, 6), currentCarModel) == 0)
            //{
            //    string spriteName = strName.Substring(7, 5);
            //    switch (spriteName)
            //    {
            //        case "zuoyi":
            //            zuoyiNum++;
            //            CreatItem(goArr[i], Seat);
            //            //设置Content的宽高，用于限制icon					           
            //            break;
            //        case "jiaod":
            //            CreatItem(goArr[i], Footpad);
            //            jiaodianNum++;
            //            break;
            //        case "lungu":
            //                CreatItem(goArr[i], Hub);
            //                lunguNum++;
            //            break;
            //        case "taiya":
            //            CreatItem(goArr[i], WindowFilm);
            //            windowFilmNum++;
            //            break;
            //        case "daoha":
            //            CreatItem(goArr[i], Navigation);
            //            navigationNum++;
            //            break;
            //        case "taban":
            //            CreatItem(goArr[i], Navigation);
            //            pedalNum++;
            //            break;
            //        case "diand":
            //            CreatItem(goArr[i], Navigation);
            //            electricPedalNum++;
            //            break;
            //        case "huban":
            //            CreatItem(goArr[i], Navigation);
            //            guardPlateNum++;
            //            break;
            //    }
            //}
            #endregion
        }

        #region  设置Grid宽度、增加ToggleChild
        if(jiaodianNum > 0)
        {
            SetGridWidth(jiaodianNum, Footpad);
            AddToggleChild(jiaodianNum, FootpadToggle);
        }

        if (zuoyiNum > 0) {
            SetGridWidth(zuoyiNum, Seat);
            AddToggleChild(zuoyiNum, SeatToggle);
        }
        if (lunguNum > 0)
        {
            SetGridWidth(lunguNum, Hub);
            AddToggleChild(lunguNum, HubToggle);
        }
        if (navigationNum > 0)
        {
            SetGridWidth(navigationNum, Navigation);
            AddToggleChild(navigationNum, NavigationToggle);
        }
        if (windowFilmNum > 0)
        {
            SetGridWidth(windowFilmNum, WindowFilm);
            AddToggleChild(windowFilmNum, WindowFilmToggle);
        }
        if (pedalNum > 0)
        {
            SetGridWidth(pedalNum, Pedal);
            AddToggleChild(pedalNum, PedalToggle);
        }
        if (electricPedalNum > 0)
        {
            SetGridWidth(electricPedalNum, ElectricPedal);
            AddToggleChild(electricPedalNum, ElectricPedalToggle);
        }
        if (guardPlateNum > 0)
        {
            SetGridWidth(guardPlateNum, GuardPlate);
            AddToggleChild(guardPlateNum, GuardPlateToggle);
        }
        if (exhouseNum > 0)
        {
            SetGridWidth(exhouseNum, Exhouse);
            AddToggleChild(exhouseNum, ExhouseToggle);
        }
        if (fenderNum > 0)
        {
            SetGridWidth(fenderNum, Fender);
            AddToggleChild(fenderNum, FenderToggle);
        }
        if (trimNum > 0)
        {
            SetGridWidth(trimNum, Trim);
            AddToggleChild(trimNum,TrimToggle);
        }
        if (fogLightNum > 0)
        {
            SetGridWidth(fogLightNum, FogLight);
            AddToggleChild(fogLightNum, FogLightToggle);
        }
        if (zhongWangNum > 0)
        {
            SetGridWidth(zhongWangNum, ZhongWang);
            AddToggleChild(zhongWangNum, ZhongWangToggle);
        }
        if (exhaustNum > 0)
        {
            SetGridWidth(exhaustNum, Exhaust);
            AddToggleChild(exhaustNum, ExhaustToggle);
        }
        if (taillightNum > 0)
        {
            SetGridWidth(taillightNum,Taillight);
            AddToggleChild(taillightNum, TaillightToggle);
        }
        if (fullshow360Num > 0)
        {
            SetGridWidth(fullshow360Num,Fullshow360);
            AddToggleChild(fullshow360Num, Fullshow360Toggle);
        }
        #endregion

    }

    void CreatItem(GameObject obj, RectTransform trans)
    {
        GameObject go = new GameObject(obj.name);   //创建一个空物体
        go.layer = LayerMask.NameToLayer("UI");
        go.transform.SetParent(trans);
        go.transform.localScale = Vector3.one;
		Image image = go.AddComponent<Image>();         //给空物体添加Image组件
        image.sprite = obj.GetComponent<SpriteRenderer>().sprite;
        image.SetNativeSize();
        RectTransform rect = go.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(140, 140);
		//Button btn = go.AddComponent<Button> ();
		//btn.targetGraphic = image;
		//鼠标点击事件
        //btn.SetOnClick(() => ChangeTexture(btn));		
    }

    void SetGridWidth(int num, RectTransform trans)
    {
        if (num <= 0)
            return;
        GridLayoutGroup grid = trans.GetComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2( Screen.width / 1.4f,grid.cellSize.y);
        grid.spacing = new Vector2(Screen.width / 6, grid.spacing.y);
        float widthItem = grid.cellSize.x + grid.spacing.x;
        //计算图片外的背景大小
        RectTransform rectTrans = trans .parent.parent.GetComponent<RectTransform>();
        rectTrans.sizeDelta = new Vector2(widthItem, rectTrans.sizeDelta.y);
        //rectTrans.localPosition = new Vector3(widthItem / 2, rectTrans.position.y,rectTrans.position.z);
        rectTrans.localPosition = Vector3.zero;

        trans.sizeDelta = new Vector2(widthItem*num,trans.sizeDelta.y);
        
    }

    void AddToggleChild(int num, RectTransform toggle)
    {
        if (num <= 0)
            return;
        for (int i = 0; i < num; ++i)
        {
            GameObject go = Instantiate(togglePrefab, Vector3.zero, Quaternion.identity) as GameObject;
            Toggle tog = go.GetComponent<Toggle>();
            tog.group = toggle.GetComponent<ToggleGroup>();
            go.transform.parent = toggle;
            go.name = i.ToString();
            if (i == 0)
                tog.isOn = true;
        }
    }

	public void ChangeTexture(Button btn){
        //Debug.Log (btn.gameObject.name);
        //修改颜色 car_a1_lungu0
        string subName = btn.name.Substring(7, 5);
        string str = btn.name.Substring(btn.gameObject.name.Length - 1);
        int index;
        if (int.TryParse(str, out index))
        {
            switch (subName)
            {
                case "zuoyi":
        //            turnCarChair chair = allScriptsObj.GetComponent<turnCarChair>();
         //           chair.shareMat.mainTexture = chair.SomeTextures[index];
                    break;
                case "jiaod":
         //           turnFloorMat jiaodian = allScriptsObj.GetComponent<turnFloorMat>();
          //          jiaodian.shareMat.mainTexture = jiaodian.SomeTextures[index];
                    break;
                case "lungu":
           //         turnCarHud lungu = allScriptsObj.GetComponent<turnCarHud>();
            //        lungu.shareMat.mainTexture = lungu.SomeTextures[index];
                    break;
            }
        }     
	}


    //public void OnClick(RectTransform trans)
    //{
    //    Debug.Log("switch");
    //    switch (trans.name)
    //    {
    //        case "CarSeat":
    //            Debug.Log("123");
    //            seat.gameObject.SetActive(true);
    //            if(preClickBtn!=null)
    //                preClickBtn.SetActive(false);
    //            preClickBtn = seat.gameObject;
    //            break;
    //        case "CarFootPad":
    //            footpad.gameObject.SetActive(true);
    //            if(preClickBtn!=null)
    //                preClickBtn.SetActive(false);
    //            preClickBtn = footpad.gameObject;
    //            break;
    //        case "CarHub":
    //            hub.gameObject.SetActive(true);
    //            if(preClickBtn!=null)
    //                preClickBtn.SetActive(false);
    //            preClickBtn = hub.gameObject;
    //            break;
    //        case "CarWindowFilm":
    //            windowFilm.gameObject.SetActive(true);
    //            if(preClickBtn!=null)
    //                preClickBtn.SetActive(false);
    //            preClickBtn = windowFilm.gameObject;
    //            break;
    //        case "CarNavigation":
    //            navigation.gameObject.SetActive(true);
    //            if(preClickBtn!=null)
    //                preClickBtn.SetActive(false);
    //            preClickBtn = navigation.gameObject;
    //            break;
    //        default:
    //            if (preClickBtn != null)
    //                preClickBtn.SetActive(false);
    //            Debug.Log("click");
    //            break;
    //    }
    //}
}
