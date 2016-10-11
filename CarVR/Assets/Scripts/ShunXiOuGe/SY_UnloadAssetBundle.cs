using UnityEngine;
using System.Collections;

public class SY_UnloadAssetBundle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (BackController.Instance.bundle) {
			BackController.Instance.bundle.Unload (false);
		}
		if (BackController.Instance.bundleInCar) {
			BackController.Instance.bundleInCar.Unload (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
