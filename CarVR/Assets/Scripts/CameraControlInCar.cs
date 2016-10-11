using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CameraControlInCar : MonoBehaviour
{

    public Transform rotateObj; //被旋转的物体
    public float speed = 5.0f;  //鼠标拖动椅子旋转速度
    public float cameraSpeed = 5.0f;  //摄像机视野滚动速度
    [HideInInspector]
    public Vector3 preEulerAngle;
    private Quaternion camRotation;

    private bool canControlRotate = true; //是否可以选择摄像机
    private bool isReset = false; //是否从重力感应球模式返回需摄像机角度

    // Use this for initialization
    void Awake()
    {
        preEulerAngle = rotateObj.eulerAngles;
        camRotation = Camera.main.transform.rotation;
    }

    //重力感应球切换回来后旋转角度修正
    void OnEnable()
    { 
        Camera.main.transform.rotation = camRotation;
    }

    // Update is called once per frame
    void Update()
    {
        CameraCtl(); 
    }

    void CameraCtl()
    {
#if UNITY_EDITOR || UNITY_STANDALONE

        //拖动椅子旋转
		if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            float h = Input.GetAxis("Mouse X");
            float v = Input.GetAxis("Mouse Y");
            preEulerAngle.x -= -v*speed;
            preEulerAngle.y += h * speed;
            rotateObj.eulerAngles = preEulerAngle;
        }

        //鼠标滚轴实现视野缩放
        //preFieldOfView -= Input.GetAxis("Mouse ScrollWheel") * cameraSpeed;
        //Camera.main.fieldOfView = Mathf.Clamp(preFieldOfView, 40f, 70f);


#elif UNITY_ANDROID || UNITY_IOS
        MobileInput();
#endif
    }


    void MobileInput()
    {
        if (Input.touchCount < 0)
            return;
        if (Input.touches[0].phase == TouchPhase.Began )
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                canControlRotate = false;
                return;
            }
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit, 100))
            {
                if (hit.collider.name.Contains("Cube"))
                {
                    canControlRotate = false;
                    return;
                }                
                
            }
            canControlRotate = true;
        }
        else if ( Input.touches[0].phase == TouchPhase.Moved && canControlRotate)
        {
            float h = Input.GetAxis("Mouse X");
            float v = Input.GetAxis("Mouse Y");
            preEulerAngle.x -= v * speed;
            preEulerAngle.y += h * speed;
            rotateObj.eulerAngles = preEulerAngle;
        }
    }


    
}
