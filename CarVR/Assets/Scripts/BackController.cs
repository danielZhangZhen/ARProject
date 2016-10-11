using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//此脚本为返回控制器
//	实现流程：
//		Home场景产生唯一实例
//		生成ArryList列表
//		Home场景产生实例的同时，存入Home
//		每当进入Loading场景，则存入即将进入的场景信息
//		每当点击返回，则读取list最后的一个数据，传给Loading并删除该数据
//		进入Web场景同理，但是进入Web不经过Loading，也就不会保存到List
//		从Web返回调用特有的方法WebBackBtn，不删除最后一个数据
public class BackController : MonoBehaviour
{

	public static BackController Instance;
	static bool CanEnter = false;
	public ArrayList list = new ArrayList ();

    public Vector3 cameraPos = Vector3.zero;//用于记录展厅进入车型时摄像机位置
    public Vector3 eulerAngles = Vector3.zero; //用于记录展厅进入车型时摄像机角度

	[HideInInspector]
	public AssetBundle bundle = null;
	[HideInInspector]
	public AssetBundle bundleInCar = null;
	//	public  string BundleURL,BundleName;
	//	public  int BundleID;



	static BackController ()
	{
		CanEnter = true;
	}

	void Start ()
	{
		if (CanEnter) {
			DontDestroyOnLoad (this.gameObject);
			Instance = this.gameObject.GetComponent<BackController> ();
			LocalScene homeScene = LocalScene.Home;
			Instance.list.Add (homeScene);
			CanEnter = false;
			Application.LoadLevel (1);
        }
		Debug.Log ("This is a Start function in BackController~ List Count:" + list.Count);

		PlayerPrefs.SetString ("isPlayMusic","true");
	}



	public void AddScene (DownloadedScene ds)
	{
		list.Add (ds);
	}

	public void AddScene (LocalScene ls)
	{
		list.Add (ls);
	}

	public void BackBtn ()
	{

		Debug.Log ("进入返回上一层的方法");
		if (list.Count >= 2) {

		} else {
			Debug.Log ("List Count错误，返回主页");
			LoadLocalScene ("V1_Home");
			return;
		}



		if (list [list.Count - 2] is LocalScene) {
			LocalScene scene = (LocalScene)list [list.Count - 2];
			switch (scene) {
			case LocalScene.AR:
				LoadLocalScene ("V1_MoYan");
				break;
			case LocalScene.Home:
				LoadLocalScene ("V1_Home");
				break;
			case LocalScene.Audi_ZhanTing:
				LoadLocalScene ("Audi_ZhanTing");
				break;
			case LocalScene.Audi_A1:
				LoadLocalScene ("Audi_A1");
				break;
			case LocalScene.Audi_A3:
				LoadLocalScene ("Audi_A3");
				break;
			case LocalScene.Audi_A4:
				LoadLocalScene ("Audi_A4");
				break;
			case LocalScene.Audi_A5:
				LoadLocalScene ("Audi_A5");
				break;
			case LocalScene.Audi_A6:
				LoadLocalScene ("Audi_A6");
				break;
			case LocalScene.Audi_A7:
				LoadLocalScene ("Audi_A7");
				break;
			case LocalScene.Audi_A8:
				LoadLocalScene ("Audi_A8");
				break;
			case LocalScene.Audi_Q3:
				LoadLocalScene ("Audi_Q3");
				break;
			case LocalScene.Audi_Q5:
				LoadLocalScene ("Audi_Q5");
				break;
			case LocalScene.Audi_Q7:
				LoadLocalScene ("Audi_Q7");
				break;
			case LocalScene.Web:
                    //this will not happen
				break;
			default:
				break;
			}
		} else if (list [list.Count - 2] is DownloadedScene) {

			DownloadedScene scene = list [list.Count - 2] as DownloadedScene;


			V1_Loading.DownloadURL = scene.url;
			V1_Loading.DownloadSceneName = scene.name;
			V1_Loading.DownloadBundleID = scene.bundleID;
			Debug.Log ("即将加载的场景：" + scene.name);


			list.RemoveAt (list.Count - 1);
			Debug.Log ("Count:" + list.Count);


			if (Screen.orientation == ScreenOrientation.Portrait) {
				Debug.Log ("当前场景竖屏，载入竖屏加载");
				Application.LoadLevel (2);

			} else {
				Application.LoadLevel (3);
				//		returnTo.loadingSceneIndex=3;
				//				returnTo.Load=true;
				Debug.Log ("当前场景横屏，载入横屏加载");

			}

		}
	}

	public void WebBackBtn ()
	{
		Debug.Log ("进入Web返回上一层的方法");
		if (list [list.Count - 1] is LocalScene) {
			Debug.Log ("上一层是本地场景");
			LocalScene scene = (LocalScene)list [list.Count - 1];
			switch (scene) {
			case LocalScene.AR:
				LoadLocalSenceByWeb ("V1_MoYan");
				break;
			case LocalScene.Home:
				LoadLocalSenceByWeb ("V1_Home");
				break;
			case LocalScene.Audi_ZhanTing:
				LoadLocalSenceByWeb ("Audi_ZhanTing");
				break;
			case LocalScene.Audi_A1:
				LoadLocalSenceByWeb ("Audi_A1");
				break;
			case LocalScene.Audi_A3:
				LoadLocalSenceByWeb ("Audi_A3");
				break;
			case LocalScene.Audi_A4:
				LoadLocalSenceByWeb ("Audi_A4");
				break;
			case LocalScene.Audi_A5:
				LoadLocalSenceByWeb ("Audi_A5");
				break;
			case LocalScene.Audi_A6:
				LoadLocalSenceByWeb ("Audi_A6");
				break;
			case LocalScene.Audi_A7:
				LoadLocalSenceByWeb ("Audi_A7");
				break;
			case LocalScene.Audi_A8:
				LoadLocalSenceByWeb ("Audi_A8");
				break;
			case LocalScene.Audi_Q3:
				LoadLocalSenceByWeb ("Audi_Q3");
				break;
			case LocalScene.Audi_Q5:
				LoadLocalSenceByWeb ("Audi_Q5");
				break;
			case LocalScene.Audi_Q7:
				LoadLocalSenceByWeb ("Audi_Q7");
				break;
			case LocalScene.Web:
				//this will not happen
				break;
			default:
				break;
			}
		} else if (list [list.Count - 1] is DownloadedScene) {
			Debug.Log ("上一层是下载场景");
			DownloadedScene scene = list [list.Count - 1] as DownloadedScene;
			V1_Loading.DownloadURL = scene.url;
			V1_Loading.DownloadSceneName = scene.name;
			V1_Loading.DownloadBundleID = scene.bundleID;
			Debug.Log ("Web场景进入竖屏加载");
			Application.LoadLevel ("V1_Loding_tall");

		}
	}

	void LoadLocalSenceByWeb (string localSceneName)
	{
     
		foreach (var item in list) {
			Debug.Log (item.ToString ());
		}
		V1_Loading.LocalSceneName = localSceneName;
		if (Screen.orientation == ScreenOrientation.Portrait) {
			Application.LoadLevel ("V1_Loding_tall");

		} else {
			Application.LoadLevel ("V1_Loding_wide");

		}
	}


	void LoadLocalScene (string localSceneName)
	{
		Debug.Log ("Count:" + list.Count);
		Debug.Log ("Loading LocalScene:" + localSceneName);
		list.RemoveAt (list.Count - 1);
		foreach (var item in list) {
			Debug.Log ("剩余的场景："+item.ToString ());
		}
		V1_Loading.LocalSceneName = localSceneName;
		if (Screen.orientation == ScreenOrientation.Portrait) {
			Application.LoadLevel ("V1_Loding_tall");

		} else {
			Application.LoadLevel ("V1_Loding_wide");

		}
	}

}

public enum LocalScene
{
	Home,
	AR,
	Web,
	Audi_A1,
	Audi_A3,
	Audi_A4,
	Audi_A5,
	Audi_A6,
	Audi_A7,
	Audi_A8,
	Audi_Q3,
	Audi_Q5,
	Audi_Q7,
	Audi_ZhanTing,
}

public class DownloadedScene
{
	public string url, name;
	public int bundleID;


	public DownloadedScene (string URL, string Name, int BundleID)
	{
		url = URL;
		name = Name;
		bundleID = BundleID;
       
	}

}



