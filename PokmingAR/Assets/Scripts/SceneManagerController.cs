using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class SceneManagerController : MonoBehaviour {

	public static SceneManagerController Instance;

	public void LoadScene(string SceneName){
		SceneManager.LoadScene (SceneName);
	}
}
