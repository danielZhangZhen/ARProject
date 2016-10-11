using UnityEngine;
using System.Collections;

public class CarRotateUIController : MonoBehaviour {

	private bool isHide = false;

	void OnGUI(){
		#if UNITY_EDITOR
		if (Event.current.type == EventType.MouseDrag) {
			gameObject.SetActive (false);
			isHide = true;
		}
		#endif
	}


	// Update is called once per frame
	void Update () {
		#if UNITY_IPHONE || UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer){
			if (!isHide && Input.GetTouch (0).phase == TouchPhase.Moved) {
				gameObject.SetActive (false);
				isHide = true;
			}
		}

		#endif
	}
}
