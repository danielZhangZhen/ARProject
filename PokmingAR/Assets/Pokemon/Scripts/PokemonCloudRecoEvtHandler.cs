using UnityEngine;
using System.Collections;
using Vuforia;
using System;

public delegate void  PokemonCloudRecoEvent(string data);


public class PokemonCloudRecoEvtHandler : MonoBehaviour, ICloudRecoEventHandler
{
    public PokemonCloudRecoEvent PokemonCloudRecoAction;
    void Start()
    {
        CloudRecoBehaviour cloudRecoBehaviour = GetComponent<CloudRecoBehaviour>();
        if (cloudRecoBehaviour)
        {
            cloudRecoBehaviour.RegisterEventHandler(this);
        }
    }


    public void OnInitError(TargetFinder.InitState initError)
    {
        
    }

    public void OnInitialized()
    {
        Debug.Log("Cloud Reco initialized successfully.");
    }

    public void OnNewSearchResult(TargetFinder.TargetSearchResult targetSearchResult)
    {
        Debug.Log("PokemonNewSerchResult:"+targetSearchResult.TargetName);
        if (targetSearchResult.MetaData!=null)
        {
            //如果多次扫描同一张图，这里只会执行一次
            PokemonCloudRecoAction(targetSearchResult.MetaData);
        }
        else
        {
            Debug.Log("元数据为空");
        }
    }

    public void OnStateChanged(bool scanning)
    {
        Debug.Log("StateChanged:"+scanning);
        
    }

    public void OnUpdateError(TargetFinder.UpdateState updateError)
    {
        throw new NotImplementedException();
    }


    public  void PauseCloudReco(bool b_stopCamera)
    {

        // This takes care of stopping and starting the targetFinder internally upon switching the camera
        CloudRecoBehaviour cloudRecoBehaviour = GameObject.FindObjectOfType(typeof(CloudRecoBehaviour)) as CloudRecoBehaviour;//找到当前的云识别组件
        cloudRecoBehaviour.CloudRecoEnabled = false;//禁用云识别，当前摄像头依然是活动的，仅可以识别当前的识别卡
        if (b_stopCamera)
        {
            CameraDevice.Instance.Stop();//停止当前的摄像头
        }
        //CameraDevice.Instance.Deinit();//将当前的摄像头禁用掉（反实例化），以便重新启用新的摄像头
 
    }
    public  void CuntinueCloudReco()
    {
        // CameraDevice.Instance.Init(CameraDevice.CameraDirection.CAMERA_DEFAULT);//启用新的摄像头，实例化
        CameraDevice.Instance.Start();//打开当前的摄像头
        // This takes care of stopping and starting the targetFinder internally upon switching the camera
        CloudRecoBehaviour cloudRecoBehaviour = GameObject.FindObjectOfType(typeof(CloudRecoBehaviour)) as CloudRecoBehaviour;//找到当前的云识别组件
        cloudRecoBehaviour.CloudRecoEnabled = true;//启用云识别

    }

}
