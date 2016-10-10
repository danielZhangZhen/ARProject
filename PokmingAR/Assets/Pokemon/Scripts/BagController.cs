using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class BagController : PannelController {

    [SerializeField]
    private Text T_MyCount;
  
 
  



   public  override void OnEnable()
    {
        //设置"我捉到的小妖数"
        base.OnEnable();
        string name = PlayerPrefs.GetString("ActivityName");
        if (T_MyCount.text != PlayerPrefs.GetInt(name).ToString())
        {
            T_MyCount.text = PlayerPrefs.GetInt(name).ToString();
        }
    }

    /// <summary>
    /// 验证，清除记录
    /// </summary>
    public void Clear()
    {
        PlayerPrefs.SetInt(PlayerPrefs.GetString("ActivityName"),0);
        //刷新数量
        T_MyCount.text = PlayerPrefs.GetInt(PlayerPrefs.GetString("ActivityName")).ToString();
    }

    public override void CloseWindow()
    {
        base.CloseWindow();
    }
}
