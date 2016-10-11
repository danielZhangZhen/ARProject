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
public  class AutoLoadSpriteForCar : MonoBehaviour

{
    public GameObject allScriptsObj;
    public GameObject togglePrefab;
    public string currentCarModel = "";//当前型号

  
    private RectTransform seat;
	private RectTransform footpad;
	private RectTransform hub;
    private RectTransform navigation;
    private RectTransform windowFilm;

    private RectTransform seatToggle;
    private RectTransform footpadToggle;
    private RectTransform hubToggle;
    private RectTransform navigationToggle;
    private RectTransform windowFilmToggle;

    private GameObject preClickBtn = null;


    void Awake()
    {
        seat = transform.FindChild("CarSeat/Scroll/Grid").GetComponent<RectTransform>();
        seatToggle = transform.FindChild("CarSeat/Toggle").GetComponent<RectTransform>();
        footpad = transform.FindChild("CarFootPad/Scroll/Grid").GetComponent<RectTransform>();
        footpadToggle = transform.FindChild("CarFootPad/Toggle").GetComponent<RectTransform>();
        hub = transform.FindChild("CarHub/Scroll/Grid").GetComponent<RectTransform>();
        hubToggle = transform.FindChild("CarHub/Toggle").GetComponent<RectTransform>();
        navigation = transform.FindChild("CarNavigation/Scroll/Grid").GetComponent<RectTransform>();
        navigationToggle = transform.FindChild("CarNavigation/Toggle").GetComponent<RectTransform>();
        windowFilm = transform.FindChild("CarWindowFilm/Scroll/Grid").GetComponent<RectTransform>();
        windowFilmToggle = transform.FindChild("CarWindowFilm/Toggle").GetComponent<RectTransform>();
    }
    void Start () 
	{
        MyLoadSprite();       
	}
    void MyLoadSprite()
    {
        GameObject[] goArr = Resources.LoadAll<GameObject>("UI/Sprite");
		int zuoyiNum = 0, 
		jiaodianNum = 0,
		lunguNum = 0,
        navigationNum = 0,
        windowFilmNum = 0;
        //Debug.Log("11");
        for (int i = 0; i < goArr.Length; i++)
        {
            string strName = goArr[i].name;
            string carModel = strName.Substring(0, 6);
           //当前读取模型为当前车模型
            if (string.Compare(strName.Substring(0, 6), currentCarModel) == 0)
            {
                string spriteName = strName.Substring(7, 5);
                switch (spriteName)
                {
                    case "zuoyi":
                        zuoyiNum++;
                        CreatItem(goArr[i], seat);
                        //设置Content的宽高，用于限制icon					           
                        break;
                    case "jiaod":
                        CreatItem(goArr[i], footpad);
                        jiaodianNum++;
                        break;
                    case "lungu":
                            CreatItem(goArr[i], hub);
                            lunguNum++;
                        break;
                    case "taiya":
                        CreatItem(goArr[i], windowFilm);
                        windowFilmNum++;
                        break;
                    case "daoha":
                        CreatItem(goArr[i], navigation);
                        navigationNum++;
                        break;
                }
            }
        }
        SetGridWidth(zuoyiNum, seat);
        SetGridWidth(jiaodianNum, footpad);
        SetGridWidth(lunguNum, hub);
        SetGridWidth(navigationNum, navigation);
        SetGridWidth(windowFilmNum, windowFilm);

        AddToggleChild(zuoyiNum, seatToggle);
        AddToggleChild(jiaodianNum, footpadToggle);
        AddToggleChild(lunguNum, hubToggle);
        AddToggleChild(navigationNum, navigationToggle);
        AddToggleChild(windowFilmNum, windowFilmToggle);
    }
    
    void CreatItem(GameObject obj, RectTransform trans)
    {
        GameObject go = new GameObject(obj.name);
        go.layer = LayerMask.NameToLayer("UI");
        go.transform.SetParent(trans);
        go.transform.localScale = Vector3.one;
		Image image = go.AddComponent<Image>();
        image.sprite = obj.GetComponent<SpriteRenderer>().sprite;
        image.SetNativeSize();
        RectTransform rect = go.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(140, 140);
		Button btn = go.AddComponent<Button> ();
		btn.targetGraphic = image;
		//鼠标点击事件
        //btn.SetOnClick(() => ChangeTexture(btn));		
    }

    void SetGridWidth(int num, RectTransform trans)
    {
        if (num <= 0)
            return;
        GridLayoutGroup grid = trans.GetComponent<GridLayoutGroup>();
        float widthItem = grid.cellSize.x + grid.spacing.x;
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
