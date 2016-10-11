using UnityEngine;
using System.Collections;
using LitJson;
using System;
using UnityEngine.UI;

public class V1_Loading : MonoBehaviour
{

	//除了打开网页之外，所有场景的跳转都要经过Loading场景
	//Loading场景有竖屏和横评两个，根据用户当前的场景方向选择载入哪一个，两者的脚本是相同的
	//要么是下载场景，要么是直接跳转场景
	//判断的依据是LoadSceneName是否为空
	//如果为空，则将label条设置为下载进度，否则将进度条设置为则将label直接设置为正在载入
	
	public static string DownloadURL, DownloadSceneName;
	public static int DownloadBundleID;
	public static string LocalSceneName;
	JsonData jData;
	public static WWW bundle;
	public static bool DontRecord = false;
    public Text ProgressLabel;
	bool lsIsDownloading = false;
	// Use this for initialization
	void Start ()
	{
		Debug.Log ("Enter Loading Start Function");

//		if (!String.IsNullOrEmpty(BackController.Instance.BundleName)) {
//			Debug.Log ("Go Download Scene ");
//			DownloadedScene scene = new DownloadedScene (BackController.Instance.BundleURL, BackController.Instance.BundleName, BackController.Instance.BundleID);
//			BackController.Instance.AddScene (scene);
//			Debug.Log ("下载场景已记录：" + DownloadSceneName);
//			StartCoroutine (DownloadScene (BackController.Instance.BundleURL, BackController.Instance.BundleID));
//		}	
//		else {
//			Debug.Log ("Go Local Scene ");
//		}

//		下载并载入场景
		if (!String.IsNullOrEmpty (DownloadSceneName)) {

			//这里需要记录即将进入的场景信息
			DownloadedScene scene = new DownloadedScene (DownloadURL, DownloadSceneName, DownloadBundleID);
			if (!DontRecord) {
				BackController.Instance.AddScene (scene);
				Debug.Log ("下载场景已记录：" + DownloadSceneName);
			} else {
				DontRecord = false;
			}

				
			StartCoroutine (DownloadScene (DownloadURL, DownloadBundleID));

		}

//		载入本地场景
		else {
			//这里需要记录即将进入的场景信息
			LocalScene scene;
			if (LocalSceneName == "V1_MoYan") {
				scene = LocalScene.AR;
				Debug.Log ("载入本地场景已记录" + LocalSceneName);
			} else if (LocalSceneName == "V1_Home") {
				if (!DontRecord) {
					BackController.Instance.list.Clear ();
				}
				scene = LocalScene.Home;		
				Debug.Log ("载入本地场景已记录" + LocalSceneName);
			} 
			else if (LocalSceneName == "Audi_A1") {
				scene = LocalScene.Audi_A1;
			}
			else if (LocalSceneName == "Audi_A3") {
				scene = LocalScene.Audi_A3;
			}
			else if (LocalSceneName == "Audi_A4") {
				scene = LocalScene.Audi_A4;
			}
			else if (LocalSceneName == "Audi_A5") {
				scene = LocalScene.Audi_A5;			}
			else if (LocalSceneName == "Audi_A6") {
				scene = LocalScene.Audi_A6;			}
			else if (LocalSceneName == "Audi_A7") {

				scene = LocalScene.Audi_A7;			}
			else if (LocalSceneName == "Audi_A8") {
				scene = LocalScene.Audi_A8;
			}
			else if (LocalSceneName == "Audi_Q3") {
				scene = LocalScene.Audi_Q3;
			}
			else if (LocalSceneName == "Audi_Q5") {
				scene = LocalScene.Audi_Q5;
			}
			else if (LocalSceneName == "Audi_Q7") {
				scene = LocalScene.Audi_Q7;
			}
			else if (LocalSceneName == "Audi_ZhanTing") {
				BackController.Instance.list.RemoveRange(1,BackController.Instance.list.Count-1);
				scene = LocalScene.Audi_ZhanTing;

			}

			else {
				scene = LocalScene.Home;
				Debug.Log ("载入本地场景存储出现问题，默认存储为Home，当前场景：" + LocalSceneName);
				BackController.Instance.list.Clear ();
				if (DontRecord) {
					BackController.Instance.AddScene (scene);	
				}
			}
			if (!DontRecord) {
				BackController.Instance.AddScene (scene);
			} else {
				DontRecord = false;
			}

			StartCoroutine (LoadLocalSceneAsy ());
		}
	}

	float time;

	void Update ()
	{
		if (lsIsDownloading) {

			if (time >= 0.5) {
				time = 0;
				Debug.Log (bundle.progress);
				ProgressLabel.text = (bundle.progress * 100).ToString ("00") + "%";
			} else {
				time += Time.deltaTime;

			}
		}
	}

	IEnumerator DownloadScene (string url, int bundleID)
	{
		bundle = WWW.LoadFromCacheOrDownload (url, bundleID);
		lsIsDownloading = true;
		yield return bundle;
		if (bundle.error != null) {
			if (bundle.error.Contains ("404")) {
				//				注意：
				//					这里出现404说明服务器没有该资源包
				//				解决方案：
				//						递减向下寻找，直到找到位置
				//				www.assetBundle.Unload(true);
				bundle = null;
				int num = int.Parse (url.Substring (url.IndexOf ('.') - 3, 3));
				if (num >= 100) {
					Debug.Log ("开始递归查找正确的文件地址");
					Debug.Log ("尝试地址：" + url.Replace (url.Substring (url.IndexOf ('.') - 3, 3), (--num).ToString ()));
					DownloadScene (url.Replace (url.Substring (url.IndexOf ('.') - 3, 3), (--num).ToString ()), bundleID);

					
				} else {
					Debug.Log ("没有对应的资源包");
					yield break;
				} 
			}
		} 

		lsIsDownloading = false;
		Debug.Log ("BundleDownload secuess");
		StartCoroutine (LoadSceneAsy (DownloadSceneName)); 



	}

	IEnumerator LoadLocalSceneAsy ()
	{
		AsyncOperation asyncOperation;
		yield return asyncOperation = Application.LoadLevelAsync (LocalSceneName);
	}

	IEnumerator LoadSceneAsy (string sceneName)
	{
		DownloadURL = null;
		DownloadSceneName = null;

//		BackController.Instance.BundleName = null;
//		BackController.Instance.BundleID = 0;

		AsyncOperation asyncOperation;
		yield return asyncOperation = Application.LoadLevelAsync (sceneName);
		bundle.assetBundle.Unload (false);
		Debug.Log ("Asset has been unloaded!");
	}
}
