using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UpdateText : MonoBehaviour {
    private bool isShow = false;
    private RectTransform toggle;
    private RectTransform grid;
    private Text textUI;
    // private ShowInfoText infoText;
    //private Dictionary<string,string> dic;

    private int _index = 0;  //用于记录Toggle的index，防止选择第二个toggle后，再点击底下按钮后，text依然显示第一个

    void Awake()
    {
        textUI = transform.parent.Find("ShowInfoText/Text").GetComponent<Text>();
        toggle = transform.Find("Toggle").GetComponent<RectTransform>();
        grid = transform.Find("Scroll/Grid").GetComponent<RectTransform>();
        //infoText = transform.parent.GetComponent<ShowInfoText>();
    }

    void Start()
    {
        //Debug.Log(grid.GetChild(0).name);
        //string test = null;
        //bool isExit = ShowInfoText._instance.dic.TryGetValue(grid.GetChild(0).name, out test);
        //if (isExit)
        //{
        //    Debug.Log("读取到了");
        //    Debug.Log(test);
        //}
        //else
        //{
        //    Debug.Log("没读到");
        //}
        //Init();
    }

    //void Update()
    //{
    //    if (isShow)
    //    {
    //        UpdateTXT();
    //    }
    //}

    //获得需要显示的图片数量，图片名称，图片数量大于1的话，还需要获取Toggle的isOn状态，用来实时更新text文本显示
    void Init()
    {
        int childCount = grid.childCount;
        if (childCount <= 0)
            return;        
        if (childCount >= 1)
        {
            string key = grid.GetChild(0).name;
            string value = null;                     
            bool isExit = ShowInfoText._instance.dic.TryGetValue(grid.GetChild(_index).name, out value);
            if(isExit)
                textUI.text = value;
        }
    }
    //当有2张以上的图片展示时，需要实时更新text的内容
    public void UpdateTXT(int index)
    {
        string key = grid.GetChild(index).name;
        string value = null;
        bool isExit = ShowInfoText._instance.dic.TryGetValue(grid.GetChild(index).name, out value);
        //Debug.Log(key + isExit);
        if (isExit)
        {
            textUI.text = value;
            _index = index;
        }
    }

    void OnEnable()
    {
        Init();
        isShow = true;
        ShowText();
        
    }

    void OnDisable()
    {
        isShow = false;
        HideText();
    }

    void ShowText()
    {
        textUI.transform.parent.gameObject.SetActive(true);
    }

    void HideText()
    {
        textUI.transform.parent.gameObject.SetActive(false);
    }

    
}
