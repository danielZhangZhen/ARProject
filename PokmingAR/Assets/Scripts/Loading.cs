using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour {

	public static string nextSceneName = "";




	// Use this for initialization
	void Start () {
		if(!string.IsNullOrEmpty(nextSceneName))
			SceneManager.LoadSceneAsync (nextSceneName);
	}
	

}
