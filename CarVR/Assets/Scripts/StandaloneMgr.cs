//*************************************************
//** 姓名 ：张振
//** 日期 ：2015年1月11日
//** 类作用： (高仿) PC平台
//*************************************************
using UnityEngine;
using System.Collections;
using System;

public class StandaloneMgr : VirtualInputMgr
{
    public override float GetAxis(string name, bool raw)
    {
        return raw ? Input.GetAxisRaw(name) : Input.GetAxis(name);
    }

    public override bool GetButton(string name)
    {
        return Input.GetButton(name);
    }

    public override bool GetButtonDown(string name)
    {
        return Input.GetButtonDown(name);
    }

    public override bool GetButtonUp(string name)
    {
        return Input.GetButtonUp(name);
    }

    public override void SetButtonDown(string name)
    {
        throw new Exception(
              " 只能在移动触摸平台");
    }

    public override void SetButtonUp(string name)
    {
        throw new Exception(
              "只能在移动触摸平台");
    }

    public override void SetAxisPositive(string name)
    {
        throw new Exception(
               " 只能在移动触摸平台");
    }

    public override void SetAxisNegative(string name)
    {
        throw new Exception(
                "只能在移动触摸平台");
    }

    public override void SetAxisZero(string name)
    {
        throw new Exception(
                " 只能在移动触摸平台");
    }

    public override void SetAxis(string name, float value)
    {
        throw new Exception(
              "只能在移动触摸平台");
    }

    public override Vector3 MousePosition()
    {
        return Input.mousePosition;
    }
}
