using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class PannelController : MonoBehaviour
{
    Tweener tweener;
    public PauseCloudRecoEvent PauseCloudRecoAction;
    public ResumeCloudRecoEvent ResumeCloueRecoAction;
    public virtual void OnEnable()
    {
        
        if (GameObject.FindGameObjectWithTag("ScalableUI"))
        {
            tweener = GameObject.FindGameObjectWithTag("ScalableUI").transform.DOScale(new Vector3(1, 1, 1), 0.25f);
            tweener.SetEase(Ease.InOutExpo);
        }
        if (PauseCloudRecoAction==null)
        {
            PauseCloudRecoAction = GameObject.Find("Canvas").GetComponent<MainController>().PauseCloudReco;
        }
        PauseCloudRecoAction(true);

    }

    public virtual void CloseWindow()
    {
        
        tweener = GameObject.FindGameObjectWithTag("ScalableUI").transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 0.3f);
        tweener.SetEase(Ease.OutExpo);

        tweener.OnComplete(delegate ()
        {
            if (ResumeCloueRecoAction == null)
            {
                ResumeCloueRecoAction = GameObject.Find("Canvas").GetComponent<MainController>().ResumeCloudReco;
            }
            ResumeCloueRecoAction();
            this.gameObject.SetActive(false);
        });        
    }
}


public delegate void PauseCloudRecoEvent(bool b_stopCam);
public delegate void ResumeCloudRecoEvent();