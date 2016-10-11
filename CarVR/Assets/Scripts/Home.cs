using UnityEngine;
using System.Collections;
using LitJson;
using System;
using System.IO;
using System.Xml;
using UnityEngine.SceneManagement;

//主要功能：
//	1.下载软件的配置文件
//	2.将本地配置文件的日期与服务器端做对比，如果有修改，就重新下载配置文件（在下载完毕之后才可以跳转到其他场景）
//	3.使用本地配置文件下载对应的资源包并赋予资源包对应的版本号

public class Home : MonoBehaviour
{

	public string ProfileURL;
	string filePath;
	bool profileReady = false;
	string checkURL = "http://119.29.74.206/app/aliyun_ts?key=";
	public string VersoinCode;
	AnalysisProfile analysisProfile;

	void Awake ()
	{
		#if UNITY_EDITOR
		filePath = Application.dataPath + "/profile.xml";

		#else
		filePath = Application.persistentDataPath + "/profile.xml";

		#endif

		string FullURL = checkURL + ProfileURL.Replace ("http://lmsj-assets.oss-cn-qingdao.aliyuncs.com/", "");
		WWW www = new WWW ("http://lmo.oss-cn-shenzhen.aliyuncs.com/2B/LMCar/profile.xml");
		StartCoroutine (CheckProfile(www));
		analysisProfile = new AnalysisProfile (filePath);
//		if (GameObject.Find ("BackControllerObj") == null) {
//			GameObject.Instantiate (Resources.Load ("BackControllerObj") as GameObject);
//		}
	}
	#if UNITY_ANDROID
	void Update()
	{
		if (Input.GetKey(KeyCode.Escape)) {
		Application.Quit();
		}
	}
	#endif

	IEnumerator CheckProfile (WWW www)
	{
		print ("Start to check profile!");
		Debug.Log (checkURL + ProfileURL.Replace ("http://lmsj-assets.oss-cn-qingdao.aliyuncs.com/", ""));
		print ("Before return www");
		yield return www;
		print ("AFTER return www");
		
		if (www.isDone) {
			print("www is done");
			if (File.Exists (filePath)) {
			
				if (PlayerPrefs.HasKey ("profileDate")) {
					//将本地配置文件的日期与服务器端做对比
					if (PlayerPrefs.GetString ("profileDate") == www.text) {
						//本地配置文件与服务器一致
						profileReady = true;
						print ("profile is macth with server!");
//						yield break; 
					} else {
						//本地配置文件过期
						File.Delete (filePath);
						print ("profile is out of date, delete!");
						
					}
				}
			
			}

		}

		WWW downloadwww = new WWW (ProfileURL);
		StartCoroutine (DownloadProfile (downloadwww));
		PlayerPrefs.SetString ("profileDate", www.text);
		print ("Set new profileDate!");
	}

	IEnumerator DownloadProfile (WWW www)
	{
		print ("Start download new profile!");
		
		yield return www;
		if (www.isDone) {
			Debug.Log ("profile download sucess");
			byte[] bts = www.bytes;
			int length = bts.Length;
			CreateXMLDoc (filePath, bts, length);
		}
	}

	void CreateXMLDoc (string path, byte[] info, int lenth)
	{
		Debug.Log ("Start to create XML");
		Stream sw;
		FileInfo t = new FileInfo (path);
		if (!t.Exists) {
			sw = t.Create ();
			
		} else {
			return;
		}
		sw.Write (info, 0, lenth);
		sw.Close ();
		sw.Dispose ();
		Debug.Log ("XML create sucess");
		profileReady = true;


	}

	public void ZhanTingBtn ()
	{
		V1_Loading.LocalSceneName = "SXOG_01";
		Application.LoadLevel ("V1_Loding_wide");

		#region 在版本1.0中，展厅不再下载，直接载入使用

//
//		if (File.Exists (filePath)) {
//			Platform ptf;
//#if UNITY_IOS
//			ptf=Platform.iOS;
//#endif
//#if UNITY_ANDROID
//			ptf=Platform.Android;
//#elif UNITY_EDITOR
//            ptf = Platform.UnityEditor;
//#endif
//			BundleInstance bundleInstance = analysisProfile.GetZTBundle (ptf, VersoinCode);
//			if (bundleInstance != null) {
//				Debug.Log("Get ZT Bundle info:");
//				Debug.Log(bundleInstance.url);
//				Debug.Log(bundleInstance.name);
//				Debug.Log(bundleInstance.bundleID);
//				
//				V1_Loading.DownloadURL = bundleInstance.url;
//				V1_Loading.DownloadSceneName = bundleInstance.name;
//				V1_Loading.DownloadBundleID = bundleInstance.bundleID;
//
////				BackController.Instance.BundleURL=bundleInstance.url;
////				BackController.Instance.BundleName=bundleInstance.name;
////				BackController.Instance.BundleID= bundleInstance.bundleID;
//
//				Application.LoadLevel ("V1_Loding_tall");
//
//			}
//	}
		
			#endregion
	}

	public void MoYanBtn ()
	{
		V1_Loading.LocalSceneName = "V1_MoYan";
		Application.LoadLevel ("V1_Loding_tall");
	}

	public void WebShopBtn ()
	{
		if (File.Exists (filePath)) {
			string webShopURL = analysisProfile.GetWebShopURL ();
			OpenWeb.URL="http://sxacura.lmoar.com/";

			Application.LoadLevel ("V1_Web");
		}
	}


}



