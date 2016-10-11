//*************************************************
//** 姓名 ：张振
//** 日期 ：2015年1月11日
//** 类作用： (高仿) 移动平台触摸轴
//*************************************************
using UnityEngine;
using System.Collections;

public class MobileTouchMgr : VirtualInputMgr
{
    private void AddBtn(string name)
    {
        CrossPlatformInputMgr.ResgisterVirtualBtn(new CrossPlatformInputMgr.VirtualButton(name));
    }

    private void AddAxis(string name)
    {
        CrossPlatformInputMgr.RegisterVirtualAxis(new CrossPlatformInputMgr.VirtualAxis(name));
    }


    public override float GetAxis(string name, bool raw)
    {
        if (dicVirtualAxis.ContainsKey(name))
        {
            return dicVirtualAxis[name].GetValue;
        }
        return 0;
   
    }



    public override bool GetButton(string name)
    {
        if (!dicVirtualBtns.ContainsKey(name))
        {
            AddBtn(name);
        }
        return dicVirtualBtns[name].GetButton;

    }

    public override bool GetButtonDown(string name)
    {
        if (!dicVirtualBtns.ContainsKey(name))
        {
            AddBtn(name);
        }
        return dicVirtualBtns[name].GetButtonDown;
    }

    public override bool GetButtonUp(string name)
    {
        if (!dicVirtualBtns.ContainsKey(name))
        {
            AddBtn(name);
        }
        return dicVirtualBtns[name].GetButtonUp;
    }

    public override void SetButtonDown(string name)
    {
        if (!dicVirtualBtns.ContainsKey(name))
        {
            AddBtn(name);
        }
        dicVirtualBtns[name].Pressed();
    }

    public override void SetButtonUp(string name)
    {
        if (!dicVirtualBtns.ContainsKey(name))
        {
            AddBtn(name);
        }
        dicVirtualBtns[name].Released();
    }

    public override void SetAxisPositive(string name)
    {
        if (!dicVirtualAxis.ContainsKey(name))
        {
            AddAxis(name);
        }
         dicVirtualAxis[name].Update(1f);
    }

    public override void SetAxisNegative(string name)
    {
        if (!dicVirtualAxis.ContainsKey(name))
        {
            AddAxis(name);
        }
        dicVirtualAxis[name].Update(-1f);
    }

    public override void SetAxisZero(string name)
    {
        if (!dicVirtualAxis.ContainsKey(name))
        {
            AddAxis(name);
        }
        dicVirtualAxis[name].Update(0f);
    }

    public override void SetAxis(string name, float value)
    {
        if (!dicVirtualAxis.ContainsKey(name))
        {
            AddAxis(name);
        }
        dicVirtualAxis[name].Update(value);
    }

    public override Vector3 MousePosition()
    {
        return vecVirtualMousePosition;
    }
}
