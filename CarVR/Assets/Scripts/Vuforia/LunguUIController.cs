using UnityEngine;
using System.Collections;

public class LunguUIController : MonoBehaviour {

	public Material mat;
	public Texture[] lunguTex;

	public void SetTexture(int index){
		if (mat) {
			Debug.Log (mat);
			mat.mainTexture = lunguTex [index];
			Debug.Log (mat.mainTexture);
		}
	}

}
