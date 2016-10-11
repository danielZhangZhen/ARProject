using UnityEngine;
using System.Collections;

public class MZD_SetOrientationWebBug : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (SetOrientation ());
	}
	
	IEnumerator SetOrientation(){
		yield return new WaitForSeconds (1f);
		if (string.Compare( OpenWeb.URL,"http://zslmzd.lmoar.com/")==0 ) {
			Screen.orientation = ScreenOrientation.AutoRotation;
		}
	}
}
