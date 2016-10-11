using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CxInteraction : MonoBehaviour {

	public Texture2D[] texs;
	public string[] des;

	public RawImage image;
	public Text text;

	public GameObject interactionUI;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		#if UNITY_EDITOR 
		if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()){
			Debug.Log("aa");
			if(interactionUI.activeSelf)
				interactionUI.SetActive(false);
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray,out hit)){
				if(hit.collider.name.Contains("Cube")){
					int index = int.Parse( hit.transform.parent.name);
					image.texture = texs[index];
					text.text = des[index];
					if(!interactionUI.activeSelf)
						interactionUI.SetActive(true);
				}
			}
		}


		#elif UNITY_ANDROID || UNITY_IPHONE

		if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)){
			Debug.Log("aa");
			if(interactionUI.activeSelf)
				interactionUI.SetActive(false);
			Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
			RaycastHit hit;
			if(Physics.Raycast(ray,out hit)){
				if(hit.collider.name.Contains("Cube")){
					int index = int.Parse( hit.transform.parent.name);
					image.texture = texs[index];
					text.text = des[index];
					if(!interactionUI.activeSelf)
						interactionUI.SetActive(true);
				}
			}
		}

		#endif
	}
}
