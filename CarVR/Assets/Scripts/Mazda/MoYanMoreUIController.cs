using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class MoYanMoreUIController : MonoBehaviour {

	public string helpURL = "";
	private Button moreBtn;
	private Button helpBtn;
	private DOTweenAnimation dt;
	private bool isDotween = false;
	public RectTransform fromPoint;

	void Awake(){
		moreBtn = GetComponent<Button> ();
		moreBtn.onClick.AddListener (delegate {
			OnMoreBtnClick();
		});

		helpBtn = transform.parent.parent.Find ("HelpDes/UrlBtn").GetComponent<Button> ();
		helpBtn.onClick.AddListener (delegate {
			OnHelpBtnClick();
		});

		dt = transform.parent.Find ("Mask/Content").GetComponent<DOTweenAnimation> ();

		fromPoint = transform.parent.parent.Find ("FromPoint").GetComponent<RectTransform> ();
		//		Debug.Log ("0000"+dt.transform.position+dt.target.transform.position);
		//		dt.transform.position = dt.target.transform.position; //将ContentUI设置到隐藏状态
	}
	void Start(){
		Debug.Log ("sdfsdf");
		dt.transform.GetComponent<RectTransform>().position = fromPoint.position; //将ContentUI设置到隐藏状态
		Debug.Log(dt.transform.position);
	}

	void OnMoreBtnClick(){
		if (!isDotween) {
			dt.DOPlayForward ();
			//dt.DOPlayBackwards ();
			isDotween = true;
			Debug.Log ("doplay");
		} else {
			dt.DOPlayBackwards ();
			//dt.DOPlayForward ();
			isDotween = false;
			Debug.Log ("dobackward");
		}

	}

	void OnHelpBtnClick(){
		Application.OpenURL (helpURL);
	}


	void OnDestroy(){
		moreBtn.onClick.RemoveAllListeners ();
		helpBtn.onClick.RemoveAllListeners ();
	}
}




