//*************************************************
//** 姓名 ：张振
//** 日期 ：2015年1月11日
//** 类作用： 3D物体点击事件
//*************************************************
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TouchClick : myTool.onClickFunction
{
    public enum AniType
    {
        eCarDoor,    //车门
        eCarWheel,   //车轮
        eCarBack,    //车后备箱
    }
    public GameObject AniObj;
    public AniType AniTypeSelect;
    private  bool isPosiNega = true;     //来回播放判断
    private Animation ani;

    private CameraPathCtrl mCameraPathCtrl;


    void Awake()
    {
        ani = AniObj.GetComponent<Animation>();
        mCameraPathCtrl = Camera.main.transform.parent.GetComponent<CameraPathCtrl>();
    }



    protected override void playMyFunction()
    {
        base.playMyFunction();
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (ani != null)
            {
                if (!ani.isPlaying)
                {
                    string aniName = string.Empty;
                    switch (AniTypeSelect)
                    {
                        case AniType.eCarDoor:
                            aniName = "OpenDoor";
                            mCameraPathCtrl.SetCameraPaths(true);
                            break;
                        case AniType.eCarWheel:
                            aniName = "CarWheel";
                            break;
                        case AniType.eCarBack:
                            aniName = isPosiNega ? "OpenBack" : "CloseBack";
                            isPosiNega = !isPosiNega;
                            break;
                        default:
                            break;
                    }
                    PlayObjAni(aniName);
                }
                else
                {
                    if (ani.IsPlaying("CarWheel"))
                        ani.Stop();
                }

            }
        }
    }

    private void PlayObjAni(string name)
    {
        ani.Play(name, PlayMode.StopSameLayer);

    }



}
