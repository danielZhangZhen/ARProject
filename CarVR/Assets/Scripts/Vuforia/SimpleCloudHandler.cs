using UnityEngine;
using System.Collections;
using Vuforia;
using System;
using LitJson;
using UnityEngine.SceneManagement;

/// <summary>
/// 1。 在ImageTarget下添加空子物体， 将jeep添加到空物体下
/// 2. 0
/// </summary>
public class SimpleCloudHandler : MonoBehaviour, ICloudRecoEventHandler,ITrackableEventHandler
{
	public static SimpleCloudHandler Instance;

	public TextAsset txt;
	public GameObject lunguUI;
	public GameObject moreUI;
	[HideInInspector]
	public CloudRecoBehaviour mCloudRecoBehaviour;
	private bool mIsScanning = false;
	private string mTargetMetadata = null;
	public ImageTargetBehaviour ImageTargetTemplate;

	private bool canDown = true;
	//cantrol Download

	private string isiphone = "not run";
	private string jeepname = "name is null";

	//	private string preMetadata = null;
	//是否扫描到活动识别图
	[HideInInspector]
	public bool isScanActivity = false;
	//活动UI动画对象
	public Animator anim;

	private TrackableBehaviour mTrackableBehaviour;

	void Awake ()
	{
		Instance = this;

		// register this event handler at the cloud reco behaviour
		mCloudRecoBehaviour = GetComponent<CloudRecoBehaviour> ();
		if (mCloudRecoBehaviour) {
			mCloudRecoBehaviour.RegisterEventHandler (this);
		}
	}


	// Use this for initialization
	void Start ()
	{
		mTrackableBehaviour = ImageTargetTemplate.transform.GetComponent<TrackableBehaviour> ();
		if (mTrackableBehaviour) {
			Debug.Log ("sdfsfasfasfdsadf");
			mTrackableBehaviour.RegisterTrackableEventHandler (this);
		}
		canDown = true;
	}

	void Update ()
	{

		if (!string.IsNullOrEmpty (mTargetMetadata)) {
			if (canDown) {
				
				DownController ();
				Debug.Log ("downcontroller run");
				canDown = false;
			}
		}

//		if (!mIsScanning && TrackableBehaviour.Status == TrackableBehaviour.Status.NOT_FOUND) {
//			mCloudRecoBehaviour.CloudRecoEnabled = true;
//			mIsScanning = true;
//		}

	}

	public void OnInitialized ()
	{
		Debug.Log ("Cloud Reco initialized");
	}

	public void OnInitError (TargetFinder.InitState initError)
	{
		Debug.Log ("Cloud Reco init error " + initError.ToString ());
	}

	public void OnUpdateError (TargetFinder.UpdateState updateError)
	{
		Debug.Log ("Cloud Reco update error " + updateError.ToString ());
	}

	public void OnStateChanged (bool scanning)
	{
		mIsScanning = scanning;
		if (scanning) {
			//// clear all known trackables
			//ImageTracker tracker = TrackerManager.Instance.GetTracker<ImageTracker>();
			//tracker.TargetFinder.ClearTrackables(false);
		}
	}

	// Here we handle a cloud target recognition event
	public void OnNewSearchResult (TargetFinder.TargetSearchResult targetSearchResult)
	{
		if (ImageTargetTemplate.gameObject.transform.childCount > 0) {
			DestroyImmediate (ImageTargetTemplate.gameObject.transform.GetChild (0).gameObject);
			mTrackableBehaviour.UnregisterTrackableEventHandler (this);
			Debug.Log ("解绑成功");
		}

		isScanActivity = false;
		moreUI.transform.parent.GetComponent<MoYanUIController> ().HideBgActivityUI (); 

		if (moreUI && moreUI.activeSelf)
			moreUI.SetActive (false);
		
		GameObject newImageTarget = Instantiate (ImageTargetTemplate.gameObject) as GameObject;
		newImageTarget.name = "ImageTarget(clone)";
		newImageTarget.transform.localPosition = Vector3.zero;

		mTrackableBehaviour = newImageTarget.transform.GetComponent<TrackableBehaviour> ();
		if (mTrackableBehaviour) {
			mTrackableBehaviour.RegisterTrackableEventHandler (this);
			Debug.Log ("注册成功");
		} else {
			Debug.Log("mTrackableBehaviour 为空");
		}
		Debug.Log("mTrackableBehaviour: " + mTrackableBehaviour );
		ImageTargetTemplate = newImageTarget.GetComponent<ImageTargetBehaviour> ();
		// do something with the target metadata
		mTargetMetadata = targetSearchResult.MetaData;
//		if (string.IsNullOrEmpty (preMetadata))
//			preMetadata = mTargetMetadata;
//		
//		if (string.Compare( preMetadata , mTargetMetadata) == 0 && ImageTargetTemplate) {
//			//DestroyImmediate (ImageTargetTemplate.gameObject.transform.GetChild (0).gameObject);
//			preMetadata = mTargetMetadata;
//		}

		//Debug.Log (mTargetMetadata);
		canDown = true;


		// stop the target finder (i.e. stop scanning the cloud)
		mCloudRecoBehaviour.CloudRecoEnabled = false;
		// Build augmentation based on target
		if (ImageTargetTemplate) {
			// enable the new result with the same ImageTargetBehaviour:
			ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker> ();
			ImageTargetBehaviour imageTargetBehaviour =
				(ImageTargetBehaviour)tracker.TargetFinder.EnableTracking (
					targetSearchResult, ImageTargetTemplate.gameObject);
		}
	}

	/// <summary>
	/// 使用本方法时需要先获取TrackableBehaviour组件，并调用RegisterEventHandler注册
	/// </summary>
	/// <param name="previousStatus">Previous status.</param>
	/// <param name="newStatus">New status.</param>
	public void OnTrackableStateChanged (
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
		    newStatus == TrackableBehaviour.Status.TRACKED ||
		    newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED) {
			//OnTrackingFound();
			Debug.Log ("33333");
			//mTrackableBehaviour.transform.LookAt (Camera.main.transform); mTrackableBehaviour is ImageTarget clone
			if (mTrackableBehaviour && mTrackableBehaviour.transform.childCount > 0
			    && mTrackableBehaviour.transform.GetChild (0).name.Contains ("hub"))
				lunguUI.SetActive (true);
			Debug.Log ("mTrackableBehaviour:" + mTrackableBehaviour);

			//表示当次扫描到活动识别图，需要显示活动UI
			if (isScanActivity) {
				anim.SetBool ("IsShow",true);
				moreUI.transform.parent.GetComponent<MoYanUIController> ().isBeginCheckScreenClick = true;
			}
		} else {
			//OnTrackingLost();
//			if (mCloudRecoBehaviour.CloudRecoInitialized && !mCloudRecoBehaviour.CloudRecoEnabled)
//			{
			if (!mIsScanning && !isScanActivity ){
				mCloudRecoBehaviour.CloudRecoEnabled = true;//需要注册mTrackableBehaviour此方法才会被调用
				Debug.Log("cloudrecoenabled = true");
			}
			if (anim.GetBool ("IsShow")) {
				mCloudRecoBehaviour.CloudRecoEnabled = false;
				Debug.Log ("显示活动UI，关闭扫描");
			}				
			if (lunguUI.activeSelf) {
				lunguUI.SetActive (false);
				Debug.Log ("hide");
			}
				
			
//			}

		}
	}



	//加载控制
	public void DownController ()
	{
		Metadata meta = GetMetadata (mTargetMetadata);
		Debug.Log (mTargetMetadata.ToString ());
		switch (meta.type) {
		case "web":
			OpenWeb.URL = meta.weburl;
			isiphone += "web";
			SceneManager.LoadScene ("V1_Web");				
			break;
		case "model":
			if (Application.platform == RuntimePlatform.Android) {
				StartCoroutine (DownloadABs (meta.androidurl, meta));
			} else if (Application.platform == RuntimePlatform.IPhonePlayer) {
				Debug.Log ("run in iphone");
				StartCoroutine (DownloadABs (meta.iphoneurl, meta));
			} else if (Application.platform == RuntimePlatform.OSXEditor) {
				Debug.Log ("run in Editor");
				StartCoroutine (DownloadABs (meta.androidurl, meta));
			}
			break;
		case "webandmodel":
			if (Application.platform == RuntimePlatform.Android) {
				StartCoroutine (DownloadABs (meta.androidurl, meta));
			} else if (Application.platform == RuntimePlatform.IPhonePlayer) {
				Debug.Log ("run in iphone");
				StartCoroutine (DownloadABs (meta.iphoneurl, meta));
			} else if (Application.platform == RuntimePlatform.OSXEditor) {
				Debug.Log ("run in Editor");
				StartCoroutine (DownloadABs (meta.androidurl, meta));
			}
			moreUI.transform.parent.GetComponent<MoYanUIController> ().metadata = meta;
			if (meta.isMoreUIShow)
				moreUI.SetActive (true);
			break;
		case "webandactivity":
			moreUI.transform.parent.GetComponent<MoYanUIController> ().SetBgActivityTexture (meta);
			GameObject obj = new GameObject();
			obj.transform.SetParent (ImageTargetTemplate.gameObject.transform); 
			break;
		}

	}
	//解析元数据
	public Metadata GetMetadata (string metadataStr)
	{
		Metadata metadata = new Metadata ();
		JsonData js = JsonMapper.ToObject (metadataStr);
		metadata.name = js ["name"].ToString ();
		metadata.version = int.Parse (js ["version"].ToString ());
		metadata.type = js ["type"].ToString (); //web or model
		switch (metadata.type) {
		case "web":
			metadata.weburl = js ["weburl"].ToString ();
			break;
		case "model":
			metadata.androidurl = js ["androidurl"].ToString ();
			metadata.iphoneurl = js ["iphoneurl"].ToString ();
			metadata.editorurl = js ["editorurl"].ToString ();
			break;
		case "webandmodel":
			metadata.weburl = js ["weburl"].ToString ();
			metadata.androidurl = js ["androidurl"].ToString ();
			metadata.iphoneurl = js ["iphoneurl"].ToString ();
			metadata.editorurl = js ["editorurl"].ToString ();
			break;
		case "webandactivity":
			metadata.weburl = js ["weburl"].ToString ();
			//此处editorurl存放的是需要显示的图片名称
			metadata.editorurl = js ["editorurl"].ToString ();
			isScanActivity = true; //暂停AR相机扫描
			break;
		}
		//metadata.id = int.Parse (js ["id"].ToString());
		//metadata.only = int.Parse (js ["only"].ToString());
		if (((IDictionary)js).Contains ("uselight"))
			metadata.useLight = bool.Parse (js ["uselight"].ToString ());
		if (((IDictionary)js).Contains ("urlshare")) {//元数据上是否包含分享数据
			metadata.urlShare = js ["urlshare"].ToString ();
			metadata.titleShare = js ["titleshare"].ToString ();
			metadata.imageurlShare = js ["imageurlshare"].ToString ();
			metadata.descriptionShare = js ["descriptionshare"].ToString ();
			metadata.contentShare = js ["contentshare"].ToString ();				
		}
		if (((IDictionary)js).Contains ("ismoreuishow")) { //是否显示更多UI按钮
			metadata.isMoreUIShow = bool.Parse (js ["ismoreuishow"].ToString ());
		}
		return metadata;
	}
	//下载AssetBundle
	IEnumerator DownloadABs (string url, Metadata metadata)
	{
		if (ImageTargetTemplate) {
			WWW www = WWW.LoadFromCacheOrDownload (url, metadata.version);
			yield return www;
			if (www.error != null) {
				Debug.LogError ("www.error :" + www.error);
				isiphone += www.error;
			} else {
				AssetBundle ab = www.assetBundle;
				AssetBundleRequest request = ab.LoadAssetAsync (metadata.name);
				yield return request;
				GameObject obj = request.asset as GameObject;
				GameObject o = Instantiate (obj, ImageTargetTemplate.transform.position, Quaternion.identity) as GameObject;
				o.transform.SetParent (ImageTargetTemplate.gameObject.transform); 
				if (!o.name.Contains ("hub")) { //模型
					o.AddComponent<RotateController> ();
					//o.transform.localScale = new Vector3 (0.25f, 0.25f, 0.25f);
				} else { //轮毂
					o.AddComponent<RotateControllerForHub> ();
					//o.transform.LookAt (Camera.main.transform);
					o.transform.localRotation = Quaternion.Euler (-90f, 0f, 0f);
					o.transform.localScale = new Vector3 (0.33f, 0.33f, 0.33f);
					lunguUI.SetActive (true);
					MeshRenderer render = o.transform.GetComponent<MeshRenderer> ();
					lunguUI.GetComponent<LunguUIController> ().mat = render.material;
				}
				if (metadata.useLight) {
					o.layer = LayerMask.NameToLayer ("MoYan"); //8表示MoYan Layer
					Transform[] trans = o.GetComponentsInChildren<Transform> ();
					foreach (var temp in trans) {
						temp.gameObject.layer = LayerMask.NameToLayer ("MoYan");
					}
				}
				ab.Unload (false);
			}
			www.Dispose ();
		}
			

	}

	//	void OnGUI()
	//	{
	//		// Display current 'scanning' status
	//		GUI.Box(new Rect(100, 100, 400, 50), mIsScanning ? "Scanning" : "Not scanning");
	//		// Display metadata of latest detected cloud-target
	//		//mTargetmetadata ： 上传的元数据内容。Metadata Package
	//		GUI.Box(new Rect(100, 200, 400, 50), "Metadata: " + mTargetMetadata);
	//		// If not scanning, show button
	//		// so that user can restart cloud scanning
	//		if (!mIsScanning)
	//		{
	//			if (GUI.Button(new Rect(100, 300, 400, 50), "Restart Scanning"))
	//			{
	//				// Restart TargetFinder
	//				mCloudRecoBehaviour.CloudRecoEnabled = true;
	//			}
	//		}
	//		GUI.TextArea(new Rect(0, 400, Screen.width, 100), isiphone);
	////		GUI.Box(new Rect(100, 500, 400, 50), ImageTargetTemplate.gameObject.transform.childCount>0?
	////			"havechild":"nochild");
	//		GUI.Box(new Rect(100, 600, 400, 50), jeepname);
	//		if (GUI.Button (new Rect (100, 700, 400, 50), "DownloadAssetBundle")) {
	//			StartCoroutine (DownloadTest ());	
	//		}
	//		if(GUI.Button(new Rect(100,800,400,50),"Open Web")){
	//			SceneManager.LoadScene ("V1_Web");
	//		}
	//	}

	IEnumerator DownloadTest ()
	{
		WWW www = WWW.LoadFromCacheOrDownload ("http://lmo.oss-cn-shenzhen.aliyuncs.com/2B/LMCar/iOS/carmodelios", 1);
		yield return www;
		if (www.error != null) {
			Debug.Log ("www.error :" + www.error);
		}
		AssetBundle ab = www.assetBundle;
		Debug.Log (ab);
		AssetBundleRequest request = ab.LoadAssetAsync ("jeep");
		yield return request;
		Debug.Log ("request : " + request);
		GameObject o = Instantiate (request.asset, new Vector3 (0.31f, 0.5f, 13.75f), Quaternion.identity) as GameObject;

	}

	void OnDestroy ()
	{
		mTrackableBehaviour.UnregisterTrackableEventHandler (this);
		mCloudRecoBehaviour.UnregisterEventHandler (this);
	}

}