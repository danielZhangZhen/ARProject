using UnityEngine;

public class returnTo : MonoBehaviour
{

	public string BundleVersion = "1.0.0";


//	public static bool  Load;
//	public static int loadingSceneIndex=2;

	void Start ()
	{
		if (V1_Loading.bundle != null) {
			V1_Loading.bundle.assetBundle.Unload (false);
			Debug.Log ("Asset has been unloaded! 002");
		}
	}


//	void  Update ()
//	{
//		if (Load) {
//			Load=false;
//			Debug.Log("Ready to load Loading Scene");
//			Application.LoadLevel(loadingSceneIndex);
//			Debug.Log("Should gone , filed");
//		}
//	}

	public void ReturnF ()
	{
		if (BackController.Instance==null) {
			Debug.Log("BackController instance is null!!!");
		}
		else {
			Debug.Log("BackController instance is not null!!!");
			BackController.Instance.BackBtn();
		}
		 
//		V1_Loading.LocalSceneName="LS";
//		Application.LoadLevel("V1_Loding_wide");
//		string filePath = Application.persistentDataPath + "/profile.xml";
//		AnalysisProfile analysisProfile = new AnalysisProfile (filePath);
//		Platform ptf;
//		#if UNITY_IOS
//		ptf=Platform.iOS;
//		#endif
//		#if UNITY_ANDROID
//		ptf=Platform.Android;
//		#endif
//		BundleInstance bundleInstance = analysisProfile.GetZTBundle (ptf, BundleVersion);
//		if (bundleInstance != null) {
//			Debug.Log ("Get ZT Bundle info:");
//			Debug.Log (bundleInstance.url);
//			Debug.Log (bundleInstance.name);
//			Debug.Log (bundleInstance.bundleID);
//			
//			V1_Loading.DownloadURL = bundleInstance.url;
//			V1_Loading.DownloadSceneName = bundleInstance.name;
//			V1_Loading.DownloadBundleID = bundleInstance.bundleID;
//			if (Screen.orientation == ScreenOrientation.Portrait) {
//				Application.LoadLevel ("V1_Loding_tall");
//			} else {
//				Application.LoadLevel ("V1_Loding_wide");
//				
//			}
//		}
	}

	public void RetrunHome()
	{
		V1_Loading.LocalSceneName = "V1_Home";
		if (Screen.orientation == ScreenOrientation.Portrait) {
			Application.LoadLevel ("V1_Loding_tall");
		} else {
			Application.LoadLevel ("V1_Loding_wide");
			
		}
	}
}
