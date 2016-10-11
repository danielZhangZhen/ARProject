//*************************************************
//** 姓名 ：张振
//** 日期 ：2015年1月19日
//** 类作用： 汽车内部触点事件
//*************************************************
using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class IgnoreUITouch : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }
    float speed = 0.1f;
    // Update is called once per frame
    void Update()
    {
       
        Debug.Log(transform.position + "-----------" + Time.deltaTime);
      
    }

    private void GetLost()
    {
     
    }
}
