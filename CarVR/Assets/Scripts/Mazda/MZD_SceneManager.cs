using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MZD_SceneManager : MonoBehaviour {

	public string nextSceneName = "";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		#if UNITY_EDITOR
		if (Input.GetMouseButtonDown (0)) {
			Debug.Log("11");
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray,out hit)){
				Debug.Log("22");
				if(string.Compare(nextSceneName, hit.transform.parent.name) == 0){
					LoadScene(nextSceneName);
				}
			}
		}
		#elif UNITY_IPHONE || UNITY_ANDROID
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
			RaycastHit hit;
			if(Physics.Raycast(ray,out hit)){
				if(hit.collider.name.Contains("Cube")){
					if(string.Compare(nextSceneName, hit.transform.parent.name) == 0){
						LoadScene(nextSceneName);
					}
				}
			}
		}

		#endif
	}

	public void LoadScene(string loadSceneName){
		V1_Loading.LocalSceneName = loadSceneName;
		SceneManager.LoadScene ("V1_Loding_wide");
	}

}
