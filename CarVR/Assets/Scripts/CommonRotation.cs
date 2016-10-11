//*************************************************
//** 姓名 ：张振
//** 日期 ：2015年12月29日
//** 类作用： 公共旋转方法
//*************************************************
using UnityEngine;
using System.Collections;
using DG.Tweening;

public class CommonRotation : MonoBehaviour
{
    public float fDuraion = 2f;
    /// <summary>
    /// 循环旋转
    /// </summary>
    void OnEnable()
    {
        transform.DORotate(new Vector3(0, 0, 360), fDuraion, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
    }

}
