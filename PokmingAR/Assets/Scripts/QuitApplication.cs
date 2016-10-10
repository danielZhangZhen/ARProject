using UnityEngine;
using System.Collections;

public class QuitApplication : MonoBehaviour {


	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape) && Application.platform == RuntimePlatform.Android ) {
			Application.Quit();
		}
	}
}
