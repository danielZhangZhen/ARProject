using UnityEngine;
using System.Collections;

public abstract class CrossPlatformInputMgr
{
    public enum ActiveInputMethod
    {
        Hardware,
        Touch
    }

    private static VirtualInputMgr mActiveInput;
    private static VirtualInputMgr mTouchInput;
    private static VirtualInputMgr mHardwareInput;

    static CrossPlatformInputMgr()
    {
        mTouchInput = new MobileTouchMgr();
        mHardwareInput = new StandaloneMgr();
#if MOBILE_INPUT
        mActiveInput = mTouchInput;
#else
        mActiveInput = mHardwareInput;
#endif
    }

    public static void SwitchActiveInputMethod(ActiveInputMethod activeInputMethod)
    {
        switch (activeInputMethod)
        {
            case ActiveInputMethod.Hardware:
                mActiveInput = mHardwareInput;
                break;
            case ActiveInputMethod.Touch:
                mActiveInput = mTouchInput;
                break;
            default:
                break;
        }
    }

    public static bool AxisExists(string name)
    {
        return mActiveInput.AxisExists(name);
    }

    public static bool ButtonExists(string name)
    {
        return mActiveInput.ButtonExists(name);
    }

    public static void RegisterVirtualAxis(VirtualAxis axis)
    {
        mActiveInput.RegisterVirtualAxis(axis);
    }

    public static void ResgisterVirtualBtn(VirtualButton btn)
    {
        mActiveInput.RegisterVirtualButton(btn);
    }

    public static void UnRegisterVirtualAxis(string name)
    {
        if (name == null || name == string.Empty)
            return;
        mActiveInput.UnRegisterVirtualAxis(name);
    }

    public static void UnRegisterVirtualBtn(string name)
    {
        mActiveInput.UnRegisterVirtualButton(name);
    }

    public static VirtualAxis VirtualAxisReference(string name)
    {
        return mActiveInput.VirtualAxisReference(name);
    }

    /// <summary>
    /// 根据名字返回相应平台合适的轴
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static float GetAxis(string name)
    {
        return GetAxis(name, false);
    }

    public static float GetAxisRaw(string name)
    {
        return GetAxis(name, false);
    }


    private static float GetAxis(string name, bool raw)
    {
        return mActiveInput.GetAxis(name, raw);
    }

    //-------btn-------------
    public static bool GetButton(string name)
    {
        return mActiveInput.GetButton(name);
    }

    public static bool GetButtonDown(string name)
    {
        return mActiveInput.GetButtonDown(name);
    }


    public static bool GetButtonUp(string name)
    {
        return mActiveInput.GetButtonUp(name);
    }


    public static void SetButtonDown(string name)
    {
        mActiveInput.SetButtonDown(name);
    }


    public static void SetButtonUp(string name)
    {
        mActiveInput.SetButtonUp(name);
    }


    public static void SetAxisPositive(string name)
    {
        mActiveInput.SetAxisPositive(name);
    }


    public static void SetAxisNegative(string name)
    {
        mActiveInput.SetAxisNegative(name);
    }


    public static void SetAxisZero(string name)
    {
        mActiveInput.SetAxisZero(name);
    }


    public static void SetAxis(string name, float value)
    {
        mActiveInput.SetAxis(name, value);
    }


    public static Vector3 mousePosition
    {
        get { return mActiveInput.MousePosition(); }
    }


    public static void SetVirtualMousePositionX(float x)
    {
        mActiveInput.SetVirtualMousePositionX(x);
    }


    public static void SetVirtualMousePositionY(float y)
    {
        mActiveInput.SetVirtualMousePositionY(y);
    }


    public static void SetVirtualMousePositionZ(float z)
    {
        mActiveInput.SetVirtualMousePositionZ(z);
    }

    public class VirtualAxis
    {
        public string name { get; private set; }
        private float mValue;
        public bool matchWithInputMgr { get; private set; }

        public VirtualAxis(string name)
            : this(name, true)
        {

        }

        public VirtualAxis(string name, bool matchToInputSetttings)
        {
            this.name = name;
            matchWithInputMgr = matchToInputSetttings;
        }

        public void ReMove()
        {
            UnRegisterVirtualAxis(name);
        }

        public void Update(float value)
        {
            mValue = value;
        }

        public float GetValue
        {
            get { return mValue; }
        }
    }

    public class VirtualButton
    {
        public string name { get; private set; }
        public bool matchWithInputMgr { get; private set; }

        private int mLastPressedFrame = -5;
        private int mReleasedFrame = -5;
        private bool mPressed;

        public VirtualButton(string name)
            : this(name, true)
        {

        }

        public VirtualButton(string name, bool matchToInputSetttings)
        {
            this.name = name;
            matchWithInputMgr = matchToInputSetttings;
        }

        public void Pressed()
        {
            if (mPressed)
            {
                return;
            }
            mPressed = true;
            mLastPressedFrame = Time.frameCount;
        }

        public void Released()
        {
            mPressed = false;
            mReleasedFrame = Time.frameCount;
        }

        public void Remove()
        {
            UnRegisterVirtualBtn(name);
        }

        public bool GetButton
        {
            get { return mPressed; }
        }

        public bool GetButtonDown
        {
            get
            {
                return mLastPressedFrame - Time.frameCount == -1;
            }
        }

        public bool GetButtonUp
        {
            get
            {
                return mReleasedFrame - Time.frameCount ==  - 1;
            }
        }
    }



}
