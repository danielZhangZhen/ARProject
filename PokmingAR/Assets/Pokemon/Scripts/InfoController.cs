using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using LitJson;


public class InfoController : PannelController
{



	readonly string str_InfotDataURL = "http://m.lmoar.com/api/ar_activity/info?name=PokemonTest";

    [SerializeField]
    private Text Text_Info;

    
    public override void OnEnable()
    {
     
        base.OnEnable();
        WWW www = new WWW(str_InfotDataURL);
        StartCoroutine(LoadData_About(www));
    }


    JsonData jd;
    IEnumerator LoadData_About(WWW www)
    {
        yield return www;
        string str_info = www.text;
        jd = JsonMapper.ToObject(str_info);
        Debug.Log(str_info);
        if ( (int) jd["result"] !=-1)
        {
            Text_Info.text = (string)jd["message"];
            Debug.Log("OkOKOKOK");
        }
        else
        {
            Debug.Log("获取Info时，服务器返回-1错误");
        }
    }

    public override void CloseWindow()
    {
        base.CloseWindow();
    }
}
