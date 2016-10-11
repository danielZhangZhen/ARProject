using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using cn.sharesdk.unity3d;
using UnityEngine.EventSystems;

public class MoYanUIController : MonoBehaviour {
	
	private Button moreBtn;
	private Button shareBtn;
	private Button rewardBtn;
	public Metadata metadata = new Metadata();

	//BgActivity显示后，点击屏幕隐藏BgActivity开关
	[HideInInspector]
	public bool isBeginCheckScreenClick = false; 

	void Awake(){

		rewardBtn = transform.Find ("BgActivity/RewardBtn").GetComponent<Button> ();
		rewardBtn.onClick.AddListener (delegate {
			OpenURL ();
		});


		moreBtn = transform.Find("More").GetComponent<Button> ();
		moreBtn.onClick.AddListener (delegate {
			OpenURL();	
		});

		shareBtn = transform.Find ("share").GetComponent<Button> ();
		shareBtn.onClick.AddListener (delegate {
			Share();
		});
	}

	void Update(){
		if (isBeginCheckScreenClick) {
			#if UNITY_EDITOR 
			if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()){
				HideBgActivityUI();
			}


			#elif UNITY_ANDROID || UNITY_IPHONE

			if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)){
				HideBgActivityUI();
			}

			#endif
		}
	}


	void OpenURL(){
		if (!string.IsNullOrEmpty(metadata.weburl)) {
			OpenWeb.URL = metadata.weburl;
			SceneManager.LoadScene ("V1_Web");
		}
	}

	void Share(){
		if (!string.IsNullOrEmpty (metadata.urlShare)) {
			Hashtable content = new Hashtable();
			content["content"] = metadata.contentShare;
			content["image"] = metadata.imageurlShare;
			content["title"] = metadata.titleShare;
			content["description"] = metadata.descriptionShare;
			content["url"] = metadata.urlShare;
			content["siteUrl"] = metadata.urlShare;
			content["site"] = metadata.titleShare;
			content["type"] = ContentType.Webpage;
			content["musicUrl"] = "http://mp3.mwap8.com/destdir/Music/2009/20090601/ZuiXuanMinZuFeng20090601119.mp3";
			BtnScripts.Instance.MoYanShareBtn (content);
		} else {
			BtnScripts.Instance.ShareBtn ("MoYan");
		}
	}

	/// <summary>
	/// 接收从AR获取的数据，并对BgActivity UI做初始化
	/// </summary>
	/// <param name="metadata">Metadata.</param>
	public void SetBgActivityTexture(Metadata metadata){
		this.metadata = metadata;
		isBeginCheckScreenClick = true;
		//此处metadata.editorurl表示sprite name
		Debug.Log("editorurl: " + "Mazda/ARImage/"+ metadata.editorurl+".png");
		string str = "Mazda/ARImage/" + metadata.editorurl;
		Sprite sprite = Resources.Load <Sprite> (str);
		Debug.Log ("sprite: "+sprite.name);
		rewardBtn.transform.parent.Find ("Image").GetComponent<Image> ().sprite = sprite;
		rewardBtn.transform.parent.GetComponent<Animator> ().SetBool ("IsShow",true);
	}
	/// <summary>
	/// 点击活动UI外部后，隐藏UI
	/// </summary>
	public void HideBgActivityUI(){
		isBeginCheckScreenClick = false;
		rewardBtn.transform.parent.GetComponent<Animator> ().SetBool ("IsShow",false);

		SimpleCloudHandler.Instance.mCloudRecoBehaviour.CloudRecoEnabled = true;
	}

	void OnDestroy(){
		moreBtn.onClick.RemoveListener (delegate {
			OpenURL();
		});
		shareBtn.onClick.RemoveListener (delegate {
			Share();
		});
		rewardBtn.onClick.RemoveListener (delegate {
			OpenURL();
		});
	}
}
