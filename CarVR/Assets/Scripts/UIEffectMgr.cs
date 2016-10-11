//*************************************************
//** 姓名 ：张振
//** 日期 ：2015年1月13日
//** 类作用： 管理视角进入车中部分UI消失效果表现
//*************************************************
using UnityEngine;
using System.Collections;
using DG.Tweening;

public class UIEffectMgr : MonoBehaviour
{
    // Use this for initialization
    private Tween t;
    public GameObject uiChangeSeat;
    public GameObject downLayer;
    void Awake()
    {   
        t = GetComponent<CanvasGroup>().DOFade(0, 1f).OnComplete(() => uiChangeSeat.SetActive(true)).SetAutoKill(false).Pause();
    }

    void OnEnable()
    {
        t.PlayForward();
        downLayer.SetActive(false);
    }

    void OnDisable()
    {
        t.PlayBackwards();
        uiChangeSeat.SetActive(false);
        downLayer.SetActive(true);
      
    }

    void OnDestroy()
    {
        DOTween.KillAll();
    }
}
