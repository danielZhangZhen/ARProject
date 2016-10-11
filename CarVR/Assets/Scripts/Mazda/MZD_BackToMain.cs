using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MZD_BackToMain : MonoBehaviour {

	private Button btn;

	void Awake(){
		btn = GetComponent<Button> ();
		btn.onClick.AddListener (delegate{ BackToMain();});
	}

	private void BackToMain(){
		V1_Loading.LocalSceneName = "V1_home";
		Application.LoadLevel ("V1_Loding_tall");
	}
	void OnDestroy(){
		btn.onClick.RemoveListener (delegate {
			BackToMain ();
		});
	}
}
