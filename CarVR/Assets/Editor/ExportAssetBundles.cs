using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;


public class ExportAssetBundles : EditorWindow {

	public static string assetbundleName;

	[MenuItem("Custom Editor/GetWindow")]
	public static void ShowWindow(){
		EditorWindow.GetWindow (typeof(ExportAssetBundles));
	}


	void OnGUI(){
		assetbundleName = EditorGUILayout.TextField ("assetbundleName", assetbundleName);
		if(GUILayout.Button("Create Select iOS AssetBundle")){
			CreateSelectAssetBundlesInIphone ();
		}
		if (GUILayout.Button ("Create Select Android AssetBundel")) {
			CreateSelectAssetBundlesInAndroid ();
		}
		if (GUILayout.Button ("Create Select OSX AssetBundle")) {
			CreateSelectAssetBundlesInOsx ();
		}
		if (GUILayout.Button ("Create Select AndroidScene AssetBundle")) {
			CreateSelectAssetBundlesSceneInAndroid ();
		}

		if (GUILayout.Button ("Create Select IOSScene AssetBundle")) {
			CreateSelectAssetBundlesSceneInIOS ();
		}

	}


	static void CreateSelectAssetBundlesSceneInAndroid(){
		Object[] selects = Selection.GetFiltered (typeof(Object), SelectionMode.DeepAssets) ;
		AssetBundleBuild[] build = new AssetBundleBuild[1];
		//需要打包的场景名
		string[] scenes = new string[selects.Length];
		build[0] = new AssetBundleBuild();
		build[0].assetBundleName = assetbundleName;
		string[] assetName = new string[selects.Length];
		for (int i = 0; i < selects.Length; ++i) {
			assetName [i] = AssetDatabase.GetAssetPath (selects [i]);
			Debug.Log ("assetName :" + assetName [i]);
			Debug.Log (i);
			scenes [i] = assetName[i];
		}
		build[0].assetNames = assetName;
		if (!Directory.Exists (Application.dataPath + "/StreamingAssets")) 
			Directory.CreateDirectory(Application.dataPath + "/StreamingAssets");



		BuildPipeline.BuildPlayer(scenes, Application.dataPath + "/StreamingAssets/"+assetbundleName,
			BuildTarget.Android,BuildOptions.BuildAdditionalStreamedScenes);
		AssetDatabase.Refresh();
	}

	static void CreateSelectAssetBundlesSceneInIOS(){
		Object[] selects = Selection.GetFiltered (typeof(Object), SelectionMode.DeepAssets) ;
		AssetBundleBuild[] build = new AssetBundleBuild[1];
		//需要打包的场景名
		string[] scenes = new string[selects.Length];
		build[0] = new AssetBundleBuild();
		build[0].assetBundleName = assetbundleName;
		string[] assetName = new string[selects.Length];
		for (int i = 0; i < selects.Length; ++i) {
			assetName [i] = AssetDatabase.GetAssetPath (selects [i]);
			Debug.Log ("assetName :" + assetName [i]);
			Debug.Log (i);
			scenes [i] = assetName[i];
		}
		build[0].assetNames = assetName;
		if (!Directory.Exists (Application.dataPath + "/StreamingAssets")) 
			Directory.CreateDirectory(Application.dataPath + "/StreamingAssets");



		BuildPipeline.BuildPlayer(scenes, Application.dataPath + "/StreamingAssets/"+assetbundleName,
			BuildTarget.iOS,BuildOptions.BuildAdditionalStreamedScenes);
		AssetDatabase.Refresh();
	}

	[MenuItem("Custom Editor/Build Select AssetBundles In Iphone")]
	static void CreateSelectAssetBundlesInIphone(){
		Object[] selects = Selection.GetFiltered (typeof(Object), SelectionMode.DeepAssets) ;
		AssetBundleBuild[] build = new AssetBundleBuild[1];
		build[0] = new AssetBundleBuild();
		build[0].assetBundleName = assetbundleName;
		string[] assetName = new string[selects.Length];
		for (int i = 0; i < selects.Length; ++i) {
			assetName [i] = AssetDatabase.GetAssetPath (selects [i]);
		}
		build[0].assetNames = assetName;
		if (!Directory.Exists (Application.dataPath + "/StreamingAssets")) 
			Directory.CreateDirectory(Application.dataPath + "/StreamingAssets");
		BuildPipeline.BuildAssetBundles(Application.dataPath + "/StreamingAssets", build,
			BuildAssetBundleOptions.None, BuildTarget.iOS);
		AssetDatabase.Refresh();
	}
	[MenuItem("Custom Editor/Build Select AssetBundles In Android")]
	static void CreateSelectAssetBundlesInAndroid(){
		Object[] selects = Selection.GetFiltered (typeof(Object), SelectionMode.DeepAssets) ;
		AssetBundleBuild[] build = new AssetBundleBuild[1];
		build[0] = new AssetBundleBuild();
		build[0].assetBundleName = assetbundleName;
		string[] assetName = new string[selects.Length];
		for (int i = 0; i < selects.Length; ++i) {
			assetName [i] = AssetDatabase.GetAssetPath (selects [i]);
		}
		build[0].assetNames = assetName;
		if (!Directory.Exists (Application.dataPath + "/StreamingAssets")) 
			Directory.CreateDirectory(Application.dataPath + "/StreamingAssets");
		BuildPipeline.BuildAssetBundles(Application.dataPath + "/StreamingAssets", build,
			BuildAssetBundleOptions.None, BuildTarget.Android);
		AssetDatabase.Refresh();
	}

	[MenuItem("Custom Editor/Build Select AssetBundles In OSX")]
	static void CreateSelectAssetBundlesInOsx(){
		Object[] selects = Selection.GetFiltered (typeof(Object), SelectionMode.DeepAssets) ;
		AssetBundleBuild[] build = new AssetBundleBuild[1];
		build[0] = new AssetBundleBuild();
		build[0].assetBundleName = assetbundleName;
		string[] assetName = new string[selects.Length];
		for (int i = 0; i < selects.Length; ++i) {
			assetName [i] = AssetDatabase.GetAssetPath (selects [i]);
		}
		build[0].assetNames = assetName;
		if (!Directory.Exists (Application.dataPath + "/StreamingAssets")) 
			Directory.CreateDirectory(Application.dataPath + "/StreamingAssets");
		BuildPipeline.BuildAssetBundles(Application.dataPath + "/StreamingAssets", build,
			BuildAssetBundleOptions.None, BuildTarget.StandaloneOSXIntel64);
		AssetDatabase.Refresh();
	}




	[MenuItem("Custom Editor/Build AssetBundles In Iphone")]
	static void CreateAssetBundlesInIphone(){
		AssetBundleBuild[] build = new AssetBundleBuild[1];
		build[0] = new AssetBundleBuild();
		build[0].assetBundleName = "carmodelios.assetbundle";
		build[0].assetNames = new string[1] { "Assets/jeep.prefab" };
		BuildPipeline.BuildAssetBundles(Application.dataPath + "/StreamingAssets", build,
			BuildAssetBundleOptions.None, BuildTarget.iOS);
		AssetDatabase.Refresh();
	}
	[MenuItem("Custom Editor/Build AssetBundles In Android")]
	static void CreateAssetBundlesInAndroid(){
		AssetBundleBuild[] build = new AssetBundleBuild[1];
		build[0] = new AssetBundleBuild();
		build[0].assetBundleName = "carlungu_android";
		build[0].assetNames = new string[1] { "Assets/lungu.prefab" };
		BuildPipeline.BuildAssetBundles(Application.dataPath + "/StreamingAssets", build,
			BuildAssetBundleOptions.None, BuildTarget.Android);
		AssetDatabase.Refresh();
	}
	[MenuItem("Custom Editor/Build AssetBundles In OSX")]
	static void CreateAssetBundlesInOSX(){
		AssetBundleBuild[] build = new AssetBundleBuild[1];
		build[0] = new AssetBundleBuild();
		build[0].assetBundleName = "carmodelosx";
		build[0].assetNames = new string[1] { "Assets/jeep.prefab" };
		//BuildPipeline.BuildAssetBundles(Application.dataPath + "/StreamingAssets");
		BuildPipeline.BuildAssetBundles(Application.dataPath + "/StreamingAssets", build,
			BuildAssetBundleOptions.None, BuildTarget.StandaloneOSXIntel64);
		AssetDatabase.Refresh();
	}

	[MenuItem("Custom Editor/Create Directory")]
	public static void CreateDirectory(){
		if (!Directory.Exists (Application.dataPath + "/Directory")) 
			Directory.CreateDirectory(Application.dataPath + "/Directory");
		AssetDatabase.Refresh ();
	}
}
