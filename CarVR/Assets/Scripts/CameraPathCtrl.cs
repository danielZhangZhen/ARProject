//*************************************************
//** 姓名 ：张振
//** 日期 ：2015年1月13日
//** 类作用： 摄像机路径管理脚本
//*************************************************
using UnityEngine;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;


public class CameraPathCtrl : MonoBehaviour
{
    public UIEffectMgr uiEffectMgr;         //隐藏相应按钮界面效果管理
    private PathPointsMgr pathPointsMgr;       //路径点管理器
    public Transform innerCarGrid;           //在车内部点父类
    private Transform[] innerCarPointsView;  //内部点集
    public Transform mCamera;                   //主摄像机

    public Animation aniCar;                //汽车
    private Tween t;

    private myCameraContrl mOutCarCameraControl;      //车外摄像机控制器
    private FirstCharacterView mInnerCarCameraCtrl;         //车内摄像机控制器
    private Transform mTrans;


    void Awake()
    {
        mTrans = transform;
        pathPointsMgr = innerCarGrid.GetComponent<PathPointsMgr>();
        mOutCarCameraControl = GetComponent<myCameraContrl>();
        mInnerCarCameraCtrl = GetComponent<FirstCharacterView>();
        innerCarPointsView = new Transform[innerCarGrid.childCount];
        for (int i = 0; i < innerCarGrid.childCount; i++)
        {
            innerCarPointsView[i] = innerCarGrid.GetChild(i);
        }
    }

    /// <summary>
    /// 进入为 true
    /// </summary>
    /// <param name="isEnter"></param>
    public void SetCameraPaths(bool isEnter)
    {
        if (isEnter)
            StartCoroutine(EnterCar(isEnter));
        else
            OutCar();
    }

    IEnumerator EnterCar(bool tmp)
    {
        SetEnterOutCarUIEffect(true);
        yield return new WaitForSeconds(1f);
        mTrans.DOPath(pathPointsMgr.GetEnterCarPath(), 1.5f, PathType.Linear, PathMode.Full3D, 10, Color.green).SetOptions(false).OnComplete(() =>
        SetCameraTran(tmp));
    }

    //进入车里后转动到合适视角 true进入
    private void SetCameraTran(bool tmp)
    {
        //摄像机和其父类
        Quaternion q = tmp ? Quaternion.Euler(38, 0, 0) : Quaternion.identity;
        mTrans.DORotateQuaternion(Quaternion.identity, 0.5f);
        mCamera.DORotateQuaternion(q, 0.5f).OnComplete(() => mInnerCarCameraCtrl.enabled = tmp);


    }

    public void NextSeat()
    {
        mTrans.DOPath(pathPointsMgr.GetNextSeat(), 1.2f, PathType.Linear).SetOptions(false);
    }

    public void OutCar()
    {
        mTrans.DOPath(pathPointsMgr.GetOutCarPath(), 2f, PathType.Linear, PathMode.Full3D, 10, Color.green).SetOptions(false).OnComplete(() =>
        {
            SetEnterOutCarUIEffect(false);
            mInnerCarCameraCtrl.enabled = false;
            mTrans.localRotation = Quaternion.identity;
            mCamera.localRotation = Quaternion.identity;
       //     SetCameraTran(false);
            aniCar.Play("CloseDoor", PlayMode.StopSameLayer);
        });
    }

    //true为进入车内，false为出来
    private void SetEnterOutCarUIEffect(bool tmp)
    {
        mOutCarCameraControl.enabled = !tmp;
        uiEffectMgr.enabled = tmp;
        //   ResetCameraTran
    }
}
