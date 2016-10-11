using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class RotateControllerForHub : MonoBehaviour {

	public Transform rotateObj; //被旋转的物体
	public float speed = -50f;  //hub旋转速度

	private bool isrotate = false; //是否开始旋转
	private bool isclick = false; //是否已经点击过

	// Use this for initialization
	void Start()
	{
		//transform.LookAt (Camera.main.transform);
		rotateObj = gameObject.transform;
	}

	// Update is called once per frame
	void Update()
	{
		if (isrotate)
			rotateObj.transform.Rotate(0, 0, speed*Time.deltaTime, Space.Self);
		CameraCtl();
	}


	void CameraCtl()
	{
		#if UNITY_EDITOR || UNITY_STANDALONE

		//拖动椅子旋转
		if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
		{
			Debug.Log("left button click");
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray,out hit)){

				if(hit.collider.name.Contains("hub")){
					isrotate = !isrotate;
					Debug.Log("click");
				}
			}
		}
		#elif UNITY_ANDROID || UNITY_IOS
		MobileInput();
		#endif
	}

	void MobileInput()
	{
		if (Input.touchCount == 1   )
		{
			
			if(Input.touches[0].phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
				RaycastHit hit;
				if(Physics.Raycast(ray,out hit)){

					if(hit.collider.name.Contains("hub")){
						isrotate = !isrotate;
						Debug.Log("click");
					}
				}
			}            
		}


	}
}
