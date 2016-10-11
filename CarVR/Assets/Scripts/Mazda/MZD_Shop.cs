using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MZD_Shop : MonoBehaviour {
	public string webShopURL = "";
	private Button btn;

	void Awake(){
		btn = GetComponent<Button> ();
		btn.onClick.AddListener (delegate{ Shop();});
	}

	private void Shop(){
		#if UNITY_ANDROID
		OpenWeb.URL=webShopURL;
		Application.LoadLevel ("V1_Web");
		#elif UNITY_IPHONE
		OpenWeb.URL=webShopURL;
		V1_Loading.LocalSceneName = "V1_Web";
		Application.LoadLevel ("V1_Loding_tall");
		#endif
	}
	void OnDestroy(){
		btn.onClick.RemoveListener (delegate {
			Shop ();
		});
	}
}
