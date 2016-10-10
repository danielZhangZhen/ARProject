using UnityEngine;
using System.Collections;
using System;

public delegate void PokemonDisableEvent();
public delegate void PokemonPauseRecoEvent(bool  b_stopCam);
public class PokemonController : MonoBehaviour
{
    public PokemonDisableEvent PokemonTimeOutAction;
    public PokemonPauseRecoEvent PokemonPauseRecoAction;
    float totalTime;
    void OnEnable()
    {
        totalTime = 3;
        if (PokemonPauseRecoAction!=null)
        {
            PokemonPauseRecoAction(false);
        }
     
    }
    void Update()
    {
        if (totalTime <= 0)
        {
            PokemonTimeOut();
        }
        else
        {
            totalTime -= Time.deltaTime;
        }
    }

    void PokemonTimeOut()
    {
        //进行下一步处理
        PokemonTimeOutAction();
        this.gameObject.SetActive(false);
    }

    void OnDisable()
    {
        GC.Collect();
    }
}
