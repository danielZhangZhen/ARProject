using UnityEngine;
using System.Collections;
using LitJson;
using UnityEngine.UI;
using System;
/// <summary>
/// 用来设置主界面的小妖数量，以及存储当前活动名称
/// </summary>
public class SetMainData : MonoBehaviour
{
    private PannelController[] controllerList;
    public Text Text_MainCount;


    private float totalTime;


	readonly string  str_MainDataURL= "http://m.lmoar.com//api/ar_activity/latest";

    // Use this for initialization
    void Start()
    {

        Text_MainCount.text = "--";
    }

    void Update()
    {
        if (totalTime < 3)
        {
            totalTime += Time.deltaTime;
        }
        else
        {
            totalTime = 0;
            WWW www = new WWW(str_MainDataURL);
            StartCoroutine(LoadData(www));
        }
    }


    JsonData jd;
    string str_Count;//小妖的剩余总数
    string str_ActivityName;//当前4S店活动名称
    IEnumerator LoadData(WWW www)
    {
        yield return www;
        string str = www.text;
        
        //解析JSON数据，设置剩余小妖数量和需要读取的PlayerPrefs的Key
        jd = JsonMapper.ToObject(str);
        if (((IDictionary)jd).Contains("elf_num"))
        {
            str_Count = ((int)jd["elf_num"]).ToString();

            if (Text_MainCount.text != str_Count)
            {
                Text_MainCount.text = str_Count;
            }
            //设置本地存储中的当前活动名称
            str_ActivityName = (string)jd["name"];
            if (PlayerPrefs.GetString(str_ActivityName) != str_ActivityName)
            {
                PlayerPrefs.SetString("ActivityName", str_ActivityName);
            }
        }
        else
        {
            gameObject.GetComponent<MainController>().ShowErrorMsg("服务器错误，此活动暂时不可用");
        }
       
    }





  
}





