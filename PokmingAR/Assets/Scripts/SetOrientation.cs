using UnityEngine;
using System.Collections;

public class SetOrientation : MonoBehaviour {

	public bool isHorizontal = false;


	// Use this for initialization
	void Start () {
		if (isHorizontal) {
			Screen.orientation = ScreenOrientation.Landscape;		
		} else {
			Screen.orientation = ScreenOrientation.Portrait;
		}
	}
}
