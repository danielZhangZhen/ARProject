//*************************************************
//** 姓名 ：张振
//** 日期 ：2015年1月11日
//** 类作用： (高仿) 虚拟轴管理类
//*************************************************
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class VirtualInputMgr
{
    public Vector3 vecVirtualMousePosition { get; private set; }

    protected Dictionary<string, CrossPlatformInputMgr.VirtualAxis> dicVirtualAxis =
        new Dictionary<string, CrossPlatformInputMgr.VirtualAxis>();
    protected Dictionary<string, CrossPlatformInputMgr.VirtualButton> dicVirtualBtns =
        new Dictionary<string, CrossPlatformInputMgr.VirtualButton>();
    protected List<string> mAlwaysUseVirtual = new List<string>();


    public bool AxisExists(string name)
    {
        return dicVirtualAxis.ContainsKey(name);
    }

    public bool ButtonExists(string name)
    {
        return dicVirtualBtns.ContainsKey(name);
    }

    public void RegisterVirtualAxis(CrossPlatformInputMgr.VirtualAxis axis)
    {
        if (dicVirtualAxis.ContainsKey(axis.name))
            Debug.LogError("这个虚拟轴已经存在" + axis.name);
        else
        {
            dicVirtualAxis.Add(axis.name, axis);
            if (!axis.matchWithInputMgr)
                mAlwaysUseVirtual.Add(axis.name);
        }
    }

    public void RegisterVirtualButton(CrossPlatformInputMgr.VirtualButton button)
    {
        if (dicVirtualBtns.ContainsKey(button.name))
        {
            Debug.LogError("这个虚拟轴已存在" + button.name);
        }
        else
        {
            dicVirtualBtns.Add(button.name, button);
            if (!button.matchWithInputMgr)
            {
                mAlwaysUseVirtual.Add(button.name);
            }
        }
    }

    public void UnRegisterVirtualAxis(string name)
    {
        if (dicVirtualAxis.ContainsKey(name))
        {
            dicVirtualAxis.Remove(name);
        }
    }

    public void UnRegisterVirtualButton(string name)
    {
        if (dicVirtualBtns.ContainsKey(name))
        {
            dicVirtualBtns.Remove(name);
        }
    }

    public CrossPlatformInputMgr.VirtualAxis VirtualAxisReference(string name)
    {
        return dicVirtualAxis.ContainsKey(name) ? dicVirtualAxis[name] : null;
    }

    public void SetVirtualMousePositionX(float x)
    {
        vecVirtualMousePosition = new Vector3(x, vecVirtualMousePosition.y, vecVirtualMousePosition.z);
    }

    public void SetVirtualMousePositionY(float y)
    {
        vecVirtualMousePosition = new Vector3(vecVirtualMousePosition.x, y, vecVirtualMousePosition.z);
    }

    public void SetVirtualMousePositionZ(float z)
    {
        vecVirtualMousePosition = new Vector3(vecVirtualMousePosition.x, vecVirtualMousePosition.y, z);
    }

    public abstract float GetAxis(string name, bool raw);
    public abstract bool GetButton(string name);
    public abstract bool GetButtonDown(string name);
    public abstract bool GetButtonUp(string name);

    public abstract void SetButtonDown(string name);

    public abstract void SetButtonUp(string name);
    public abstract void SetAxisPositive(string name);
    public abstract void SetAxisNegative(string name);
    public abstract void SetAxisZero(string name);
    public abstract void SetAxis(string name, float value);

    public abstract Vector3 MousePosition();

}
