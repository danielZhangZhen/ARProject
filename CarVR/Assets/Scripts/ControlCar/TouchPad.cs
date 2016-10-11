//*************************************************
//** 姓名 ：张振
//** 日期 ：2015年1月11日
//** 类作用： 车子内部镜头控制，以自己为自身旋转
//*************************************************
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchPad : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    //平板触摸选择哪一个轴
    public enum AxisOption
    {
        eBoth,
        eOnlyHorizontal,
        eOnlyVertical
    }

    public enum ControlStyle
    {
        Absolute,   //绝对于  触摸图片区域的中心的操作
        Relative,   //相对于  初始触摸中心
        Swipe      //滑动
    }

    public AxisOption eAxesToUse = AxisOption.eBoth;
    public ControlStyle eControlStyle = ControlStyle.Absolute;
    private string strHAxisName = "Mouse X";
    private string strVAxisName = "Mouse Y";
    public float Xsensitivity = 1f;
    public float Ysensitivity = 1f;

    Vector3 mStartPos;
    Vector2 mPreviousDelta;
    Vector3 mJoytickOutput;
    bool mUseX;
    bool mUseY;
    CrossPlatformInputMgr.VirtualAxis mHorizontalVirtualAxis;
    CrossPlatformInputMgr.VirtualAxis mVerticalVirtualAxis;
    bool mDragging;
    int mId = -1;
    Vector2 mPreviousTouchPos;


#if !UNITY_EDITOR
    private Vector3 mCenter;
    private Image mImage;
#endif
    Vector3 mPreviousMouse;


    void Start()
    {

#if !UNITY_EDITOR
        mImage = GetComponent<Image>();
        mCenter = mImage.transform.position;
#endif
    }

    void OnEnable()
    {
        CreateVirtualAxis();
    }

    void OnDisable()
    {
        if (CrossPlatformInputMgr.AxisExists(strHAxisName))
            CrossPlatformInputMgr.UnRegisterVirtualAxis(strHAxisName);
        if (CrossPlatformInputMgr.AxisExists(strVAxisName))
            CrossPlatformInputMgr.UnRegisterVirtualAxis(strVAxisName);
    }

    private void CreateVirtualAxis()
    {
        mUseX = (eAxesToUse == AxisOption.eBoth || eAxesToUse == AxisOption.eOnlyHorizontal);
        mUseY = (eAxesToUse == AxisOption.eBoth || eAxesToUse == AxisOption.eOnlyVertical);
        if (mUseX)
        {
            mHorizontalVirtualAxis = new CrossPlatformInputMgr.VirtualAxis(strHAxisName);
            CrossPlatformInputMgr.RegisterVirtualAxis(mHorizontalVirtualAxis);
        }
        if (mUseY)
        {
            mVerticalVirtualAxis = new CrossPlatformInputMgr.VirtualAxis(strVAxisName);
            CrossPlatformInputMgr.RegisterVirtualAxis(mVerticalVirtualAxis);
        }
    }

    private void UpdateVirtualAxis(Vector3 value)
    {
        value = value.normalized;
        if (mUseX)
            mHorizontalVirtualAxis.Update(value.x);
        if (mUseY)
            mVerticalVirtualAxis.Update(value.y);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        mDragging = true;
        mId = eventData.pointerId;
#if !UNITY_EDITOR
        if(eControlStyle != ControlStyle.Absolute)
        mCenter = eventData.position;
#endif
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        mDragging = false;
        mId = -1;
        UpdateVirtualAxis(Vector3.zero);
    }

    void Update()
    {
        if (!mDragging)
        {
            return;
        }
        if (Input.touchCount >= mId + 1 && mId != -1)
        {
            Vector2 pointerDelta;
#if !UNITY_EDITOR
            if (eControlStyle == ControlStyle.Swipe)
            {
                mCenter = mPreviousTouchPos;
                mPreviousTouchPos = Input.touches[mId].position;
            }
             pointerDelta = new Vector2(Input.touches[mId].position.x - mCenter.x, Input.touches[mId].position.y - mCenter.y).normalized;
            pointerDelta.x *= Xsensitivity;
            pointerDelta.y *= Ysensitivity;
#else
            float x = Input.mousePosition.x - mPreviousMouse.x;
            float y = Input.mousePosition.y - mPreviousMouse.y;
            pointerDelta = new Vector2(x, y);
            mPreviousMouse = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);

#endif
            UpdateVirtualAxis(new Vector3(pointerDelta.x, pointerDelta.y, 0));
        }
    }
}
