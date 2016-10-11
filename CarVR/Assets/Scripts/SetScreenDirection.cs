using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SetScreenDirection : MonoBehaviour {

    public  bool IsScreenHorizontal=false;

    void Awake () {

		Debug.Log ("当前屏幕方向："+ Screen.orientation);
        if (IsScreenHorizontal)
        {
            if (Screen.orientation!=ScreenOrientation.LandscapeLeft) {
				Debug.Log ("设置屏幕方向为："+ScreenOrientation.LandscapeLeft);
                Screen.orientation = ScreenOrientation.LandscapeLeft;
				Debug.Log ("设置完成，当前屏幕方向："+Screen.orientation);

            }
        }
        else
        {
            if (Screen.orientation != ScreenOrientation.Portrait) {
				Debug.Log ("设置屏幕方向为："+ScreenOrientation.Portrait);
                Screen.orientation = ScreenOrientation.Portrait;
				Debug.Log ("设置完成，当前屏幕方向："+Screen.orientation);
            }
        }
    }

	void Start(){
		if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("V1_Home"))
			Screen.orientation = ScreenOrientation.Portrait;
	}
}
