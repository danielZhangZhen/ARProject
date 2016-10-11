//*************************************************
//** 姓名 ：张振
//** 日期 ：2015年1月13日
//** 类作用： 在车里摄像机以自身角度去观察
//*************************************************
using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class FirstCharacterView : MonoBehaviour
{
    [Serializable]
    public class TouchLook
    {
        public float XSensitivity = 2f;
        public float YSensitivity = 2f;
        public float minimumX = -90f;
        public float maximumX = 90f;
        public float smoothTime = 5f;
        private string AxisXName = "Mouse X";
        private string AxisYName = "Mouse Y";

        private Quaternion mCharacterTargetRot;
        private Quaternion mCameraTargetRot;

        public void Init(Transform character, Transform camera)
        {
            mCharacterTargetRot = character.localRotation;
            mCameraTargetRot = camera.localRotation;
        }

        public void TouchLookRot(Transform character, Transform camera)
        {
            float yRot = CrossPlatformInputMgr.GetAxis(AxisXName) * XSensitivity;
            float xRot = CrossPlatformInputMgr.GetAxis(AxisYName) * YSensitivity;

            mCharacterTargetRot *= Quaternion.Euler(0f, yRot, 0f);
            mCameraTargetRot *= Quaternion.Euler(-xRot, 0f, 0f);
            mCameraTargetRot = ClampRotationAroundXAxis(mCameraTargetRot);

            character.localRotation = Quaternion.Slerp(character.localRotation, mCharacterTargetRot, smoothTime * Time.deltaTime);
            camera.localRotation = Quaternion.Slerp(camera.localRotation, mCameraTargetRot, smoothTime * Time.deltaTime);
        }

        private Quaternion ClampRotationAroundXAxis(Quaternion q)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;
            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);
            angleX = Mathf.Clamp(angleX, minimumX, maximumX);
            q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);
            return q;
        }
    }

    public Transform mainCamera;
    private Transform mTrans;
    public GameObject touchImg;                 //触摸区域
    public TouchLook touchLook = new TouchLook();

    void Awake()
    {
        mTrans = transform;
    }

    void OnEnable()
    {
        touchLook.Init(mTrans, mainCamera);
        touchImg.GetComponent<Image>().enabled = true;
    }

    void OnDisable()
    {
        if (touchImg)
            touchImg.GetComponent<Image>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        RotateView();
    }

    //通过触摸或者其他旋转摄像机镜头
    private void RotateView()
    {
        touchLook.TouchLookRot(mTrans, mainCamera);

    }
}
