using UnityEngine;
using System.Collections;

public class LoadingTest : MonoBehaviour {

	WWW bundle;
	string url;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//	if (www!=null) {
//			Debug.Log(www.progress);
//		}
	}

	void OnClick()
	{
		StartCoroutine (DownloadScene("http://lmsj-assets.oss-cn-qingdao.aliyuncs.com/QuGuangA%2FV2%2FTest%2FExportTestiPhone.unity3d"));
	}

	IEnumerator DownloadScene(string path)
	{
		bundle = WWW.LoadFromCacheOrDownload ("http://lmsj-assets.oss-cn-qingdao.aliyuncs.com/QuGuangA%2FV2%2FTest%2FExportTestiPhone.unity3d",0);// new WWW ("http://lmsj-assets.oss-cn-qingdao.aliyuncs.com/QuGuangA%2FV2%2FTest%2FExportTestiPhone.unity3d");
		
		yield return bundle;
		StartCoroutine (LoadSceneAsy());
	}

	IEnumerator LoadSceneAsy()
	{
		AsyncOperation asyncOperation;
		yield return asyncOperation = Application.LoadLevelAsync("ExportTest");
	}
}
