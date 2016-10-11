//*************************************************
//** 姓名 ：张振
//** 日期 ：2015年12月29日
//** 类作用： 公共位移
//*************************************************
using UnityEngine;
using System.Collections;
using DG.Tweening;

public class CommonTWPos : MonoBehaviour
{
    public float fEndValueY ;
    public Ease EaseType = Ease.InOutBack;
    public float duration = 1.0f;
    private Tweener mTween;
    void Awake()
    {
        mTween = transform.DOLocalMoveY(fEndValueY, duration).SetEase(EaseType).SetRelative().SetAutoKill(false).Pause();
    }
    /// <summary>
    /// 正播放
    /// </summary>
    public void TWForward()
    {
        mTween.PlayForward();
    }

    /// <summary>
    /// 倒播放
    /// </summary>
    public void TWBack()
    {
        mTween.PlayBackwards();
    }

    void OnDestroy()
    {
        mTween.Kill();
    }

}
