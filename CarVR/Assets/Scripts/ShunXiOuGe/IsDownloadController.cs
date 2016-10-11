using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IsDownloadController : MonoBehaviour
{

    public string nextSceneName = "";//下一个场景名称
    public string downloadAssetBundleName = "";//从服务器上下载的AB包名称
#if UNITY_ANDROID
    private string downloadURL = "http://lmo.oss-cn-shenzhen.aliyuncs.com/2B/DW_ShunXiOuGe/AssetBundles/Android/sxog-android.unity3d";//服务器下载地址
#else 
	private string downloadURL = "http://lmo.oss-cn-shenzhen.aliyuncs.com/2B/DW_ShunXiOuGe/AssetBundles/Android/sxog-ios.unity3d";//服务器下载地址
#endif
    public GameObject tipsUI;
    public GameObject loadingUI;
    public Image loadingPercent;
    private int version = 16; //版本号

    private Button yesBtn;
    private Button noBtn;
    private WWW m_www = null;
    private bool isBeginDownload = false;
    private string clickArrow = ""; //判断点击的箭头
    void Awake()
    {
        yesBtn = GameObject.Find("Canvas").transform.Find("Download-TipsUI/Tips/Yes").GetComponent<Button>();
        yesBtn.onClick.AddListener(delegate
        {
            OnYesBtnClick();
        });



        noBtn = GameObject.Find("Canvas").transform.Find("Download-TipsUI/Tips/No").GetComponent<Button>();
        noBtn.onClick.AddListener(delegate
        {
            OnNoBtnClick();
        });
        Debug.Log(Application.persistentDataPath);
    }

    // Update is called once per frame
    void Update()
    {

        if (isBeginDownload && m_www != null)
        {

            loadingPercent.fillAmount = m_www.progress;

        }


#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0) && !loadingUI.activeSelf)
        {
            Debug.Log("11");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("22");
                if (string.Compare(nextSceneName, hit.transform.parent.name) == 0)
                {
                    clickArrow = hit.transform.parent.name;
                    LoadScene(nextSceneName);
                }
            }
        }
#elif UNITY_IPHONE || UNITY_ANDROID
		if (Input.GetMouseButtonDown (0) && !loadingUI.activeSelf) {
		Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
		RaycastHit hit;
		if(Physics.Raycast(ray,out hit)){
		if(hit.collider.name.Contains("Cube")){
		if(string.Compare(nextSceneName, hit.transform.parent.name) == 0){
		clickArrow = hit.transform.parent.name;
		LoadScene(nextSceneName);
		}
		}
		}
		}

#endif
    }

    public void LoadScene(string loadSceneName)
    {
        //bool isDownload = IsFileInFolder(downloadAssetBundleName);
        if (PlayerPrefs.GetInt("IsDownload") != version)
        {
            tipsUI.SetActive(true);

        }
        else
        {
            StartCoroutine(DownloadSceneAssetBundle());
            //			V1_Loading.LocalSceneName = nextSceneName;
            //			SceneManager.LoadScene ("V1_Loding_wide");
        }
        //		if (isDownload) {
        //			//从本地load Assetbundle
        //			StartCoroutine(LoadAssetBundleFromLocal());
        //
        //		} else { 
        //			//loadSceneName Scene 不可用，表示未下载资源 则弹出UI界面
        //			tipsUI.SetActive(true);
        //		}
    }

    private void OnYesBtnClick()
    {
        if (string.Compare(clickArrow, "SXOG_02") == 0)
        {
            tipsUI.SetActive(false);
            loadingUI.SetActive(true);
            StartCoroutine(DownloadSceneAssetBundle());
        }
        else if (string.Compare(clickArrow, "SXOG_10") == 0)
        { //为了防止两个箭头上的脚本同时下载
            tipsUI.SetActive(false);
            loadingUI.SetActive(true);
            StartCoroutine(DownloadSceneAssetBundle());
        }
    }

    private void OnNoBtnClick()
    {
        tipsUI.SetActive(false);

    }

    //判断文件是否保存在文件夹内
    private bool IsFileInFolder(string fileName)
    {
        if (System.IO.File.Exists(Application.persistentDataPath + "/" + fileName))
            return true;
        else
            return false;
    }

    IEnumerator DownloadSceneAssetBundle()
    {
        // Wait for the Caching system to be ready
        while (!Caching.ready)
            yield return null;
        //WWW www = WWW.LoadFromCacheOrDownload (downloadURL,3);
        //use using will call www.Dispose in the end
        using (WWW www = WWW.LoadFromCacheOrDownload(downloadURL, version))
        {
            m_www = www;
            isBeginDownload = true;
            Debug.Log("开始下载" + "下载大小为： ");
            //			SetClickState ();//按钮被点击,设置isClickState参数
            //disableObj.SetActive (true);//将按钮设置为不可点击
            //ShowSliderbar ();
            //downloadState = DownloadState.Downloading;
            //BackController.Instance.isDownloading++;
            yield return www;
            if (www.error != null)
            {
                Debug.LogError("www.error :" + www.error);
            }
            else
            {
                //PlayerPrefs.SetInt (gameObject.name, 1);//用于控制下载提示框是否显示
                isBeginDownload = false;
                m_www = null;
                Debug.Log("下载完成");
                //text.text += "｜下载完成";
                //AssetBundle ab = www.assetBundle;
                //BackController.Instance.isDownloading--;
                //SceneManager.LoadSceneAsync (nextSceneName);
                //if(BackController.Instance.isDownloading==0){	

                //				string cachedAssetBundle = Application.persistentDataPath + "/" + downloadAssetBundleName;
                //				System.IO.FileStream cache = new System.IO.FileStream(cachedAssetBundle, System.IO.FileMode.Create);
                //				cache.Write(www.bytes, 0, www.bytes.Length);
                //				cache.Close(); 
                //PlayerPrefs.SetInt("IsDownload",1);
                PlayerPrefs.SetInt("IsDownload", version);
                BackController.Instance.bundle = www.assetBundle;
                V1_Loading.LocalSceneName = nextSceneName;
                SceneManager.LoadScene("V1_Loding_wide");
            }
        }
        //www.Dispose ();
        //清除缓存
        //Caching.CleanCache();
    }

    //从本地load Assetbundle
    IEnumerator LoadAssetBundleFromLocal()
    {
#if UNITY_EDITOR
        string url = "file://" + Application.persistentDataPath + "/" + downloadAssetBundleName;
#else
		string url = "jar:file://" + Application.persistentDataPath + "/" + downloadAssetBundleName;
#endif
        using (WWW www = new WWW(url))
        {
            yield return www;
            if (www.error != null)
            {
                Debug.LogError("www.error :" + www.error);
            }
            else
            {
                Debug.Log("loadLocalAB");
                BackController.Instance.bundle = www.assetBundle;

                V1_Loading.LocalSceneName = nextSceneName;
                SceneManager.LoadScene("V1_Loding_wide");
            }
        }
    }


    void OnDestroy()
    {
        yesBtn.onClick.RemoveAllListeners();
        noBtn.onClick.RemoveAllListeners();
    }
}
