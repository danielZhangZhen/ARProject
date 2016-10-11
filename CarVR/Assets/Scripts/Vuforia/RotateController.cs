using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class RotateController : MonoBehaviour
{

	public Transform rotateObj; //被旋转的物体
	public float speed = 2f;  //鼠标拖动椅子旋转速度
	public float cameraSpeed = 2.0f;  //摄像机视野滚动速度

	private Vector3 preEulerAngle;
	private float preFieldOfView;

	private bool canControlRotate = true; //是否可以选择摄像机

	//摄像机fieldofview
	public float camSpeed = 1f;//摄像机缩放速度
	public float camViewMin = 30f;
	public float camViewMax = 100f;
	private Vector2 prePos = Vector2.zero;

	// Use this for initialization
	void Start()
	{
		transform.LookAt (Camera.main.transform);
		rotateObj = gameObject.transform;
		preEulerAngle = rotateObj.eulerAngles;
		preFieldOfView = Camera.main.fieldOfView;
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
		if (Input.GetMouseButton(0))
		{
			float h = speed * Input.GetAxis("Mouse X");
			float v = speed * Input.GetAxis("Mouse Y");
			if(Mathf.Abs(h)>Mathf.Abs(v))
				v = 0;
			else
				h = 0;
			rotateObj.transform.Rotate(-v, -h, 0, Space.World);
		}

		//鼠标滚轴实现视野缩放
		preFieldOfView -= Input.GetAxis("Mouse ScrollWheel") * cameraSpeed;
		Camera.main.fieldOfView = Mathf.Clamp(preFieldOfView, 40f, 70f);


		#elif UNITY_ANDROID || UNITY_IOS
		MobileInput();
		#endif
	}

	void MobileInput()
	{
		if (Input.touchCount == 1   )
		{
			if(Input.touches[0].phase == TouchPhase.Began)
				canControlRotate = true;
			if(Input.touches[0].phase == TouchPhase.Moved && canControlRotate)
			{
				float h = speed * Input.GetAxis("Mouse X");
				float v = speed * Input.GetAxis("Mouse Y");
				if(Mathf.Abs(h)>Mathf.Abs(v))
					v = 0;
				else
					h = 0;
				rotateObj.transform.Rotate(v, -h, 0, Space.World);
			}            
		}
		//多点触控
		else if (Input.touchCount > 1)
		{
			canControlRotate = false;
			if (Input.touches[0].phase == TouchPhase.Moved || Input.touches[1].phase == TouchPhase.Moved)
			{
				//计算移动方向
				Vector2 nowDir = Input.touches[1].position - Input.touches[0].position;

				//根据向量的大小判断当前手势是放大还是缩小
				if (nowDir.sqrMagnitude > prePos.sqrMagnitude)
				{
					preFieldOfView -= camSpeed;
				}
				else {
					preFieldOfView += camSpeed;
				}
				//限制距离
				preFieldOfView = Mathf.Clamp(preFieldOfView, camViewMin, camViewMax);
				prePos = nowDir;
				Camera.main.fieldOfView = preFieldOfView;
			}
		}


	}
}