//**********************************************************
//** 姓名 ：林宇敬
//** 日期 ：2016年03月15日
//** 类作用： 动态读取UI图标，并根据图标名称改变汽车部件材质
//**********************************************************
using UnityEngine;
using System.Collections;
using System.Globalization;
using UnityEngine.UI;
public  class AutoLoadIcon : MonoBehaviour

{
    public GameObject allScriptsObj;
    public string currentCarModel = "";//当前型号

  
    private RectTransform zuoyi;
	private RectTransform jiaodian;
	private RectTransform lungu;
    private ScrollRect jiaodianScrollRect;


    void Awake()
    {
        zuoyi = transform.FindChild("Seat/colorMenuTran/Grid").GetComponent<RectTransform>();
        jiaodian = transform.FindChild("FootPad/fourMats/Grid").GetComponent<RectTransform>();
        lungu = transform.FindChild("Hud/carHud/Grid").GetComponent<RectTransform>();
    }
    void Start () 
	{
        MyLoadSprite();       
	}
    void MyLoadSprite()
    {
        GameObject[] goArr = Resources.LoadAll<GameObject>("UI/Icon");
		float zuoyiNum = 0, 
		jiaodianNum = 0,
		lunguNum = 0;
        for (int i = 0; i < goArr.Length; i++)
        {
            string strName = goArr[i].name;
            string spriteName = strName.Substring(7, 5);
           // Debug.Log(spriteName);
            switch (spriteName)
            {
				case "zuoyi":
                    zuoyiNum++;
                    //由于A5座椅只有2张，所以用代码特殊处理
                    if (zuoyiNum == 3 && string.Compare(currentCarModel, "car_a5") == 0)
                        break;
					CreatItem (goArr[i], zuoyi);
					//设置Content的宽高，用于限制icon					           
                    break;
                case "jiaod":
                    CreatItem(goArr[i], jiaodian);
					jiaodianNum++;
                    break;
                case "lungu":
                    //判断是否当前车型的轮子
                    if (string.Compare(strName.Substring(0, 6), currentCarModel) == 0)
                    {                     
                        CreatItem(goArr[i], lungu);
                        lunguNum++;
                    }
                    break;
            }
        }
        if (zuoyiNum <= 5)
            zuoyi.parent.GetComponent<ScrollRect>().horizontal = false;
        else
            zuoyi.parent.GetComponent<ScrollRect>().horizontal = true;
        if (jiaodianNum <= 5)
            jiaodian.parent.GetComponent<ScrollRect>().horizontal = false;
        else
            jiaodian.parent.GetComponent<ScrollRect>().horizontal = true;
        if (lunguNum <= 5)
            lungu.parent.GetComponent<ScrollRect>().horizontal = false;
        else
            lungu.parent.GetComponent<ScrollRect>().horizontal = true;
        
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
        btn.SetOnClick(() => ChangeTexture(btn));		
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
                    turnCarChair chair = allScriptsObj.GetComponent<turnCarChair>();
                    chair.shareMat.mainTexture = chair.SomeTextures[index];
                    break;
                case "jiaod":
                    turnFloorMat jiaodian = allScriptsObj.GetComponent<turnFloorMat>();
                    jiaodian.shareMat.mainTexture = jiaodian.SomeTextures[index];
                    break;
                case "lungu":
                    turnCarHud lungu = allScriptsObj.GetComponent<turnCarHud>();
                    lungu.shareMat.mainTexture = lungu.SomeTextures[index];
                    break;
            }
        }     
	}
}
