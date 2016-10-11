using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ShowInfoText : MonoBehaviour
{
    public static ShowInfoText _instance;
    //public static ShowInfoText Instance
    //{
    //    get
    //    {
    //        if (null == _instance)
    //            _instance = new ShowInfoText();
    //        return _instance;
    //    }
    //}
    public TextAsset taskinfoText;
    public  Dictionary<string, string> dic = new Dictionary<string, string>();

    void Awake()
    {
        _instance = this;
        GetTxt();
    }

    //获取txt内容
    public void GetTxt()
    {
        string taskinfoStr = taskinfoText.ToString();
        string[] strArray = taskinfoStr.Split('\n');
        foreach (string str in strArray)
        {
            string[] proStr = str.Split('|');  //proStr  property属性            
            dic.Add(proStr[0], proStr[1]);
            //Debug.Log(proStr[0] + " " + proStr[1]);
            
        }
        //Debug.Log("ceshi" + dic["Q3_daohang"]);
    }
}
