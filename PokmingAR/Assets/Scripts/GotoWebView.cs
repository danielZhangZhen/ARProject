using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GotoWebView : MonoBehaviour {


	private Button zhuoXiaoYaoBtn;
	private Button cangBaoTuBtn;
	private Button duoBaoBtn;
	private Button gameDesBtn;

	private Button shoppingMallBtn;


	void Awake()
	{
		//shoppingMallBtn = transform.Find("Bg/ShoppingMall").GetComponent<Button>();
		//shoppingMallBtn.onClick.AddListener(delegate { GotoWebUrl("http://m.lmoar.com/"); });//注册商城点击事件
	}

	public void LoadWideScene(string sceneName){
		Loading.nextSceneName = sceneName;
		SceneManager.LoadScene ("Loading_Wide");
	}

	public void GotoWebUrl(string url)
	{
		var webViewGameObject = GameObject.Find("WebView");
		if (webViewGameObject == null)
		{
			webViewGameObject = new GameObject("WebView");
		}
		var webView = webViewGameObject.AddComponent<UniWebView>();
		webView.OnLoadComplete += OnLoadComplete;//注册Load事件
		webView.OnReceivedMessage += OnReceivedMessage;//注册网页返回事件
		webView.InsetsForScreenOreitation += InsetsForScreenOreitation;//注册屏幕方向事件
		webView.toolBarShow = true;
		webView.url = url;
		webView.Load();
	}

	void OnLoadComplete(UniWebView webView, bool success, string errorMessage)
	{
		if (success)
		{
			webView.Show();
		}
		else {
			Debug.Log("Something wrong in webview loading: " + errorMessage);
		}
	}

	void OnReceivedMessage(UniWebView webView, UniWebViewMessage message)
	{
		if (message.path == "home")
		{
			Destroy(webView);
			webView = null;
			//Loading.nextSceneName = "Main";
			SceneManager.LoadScene ("Main");
			//Controller.Instance.LoadScene("Main");
		}
	}
	UniWebViewEdgeInsets InsetsForScreenOreitation(UniWebView webView, UniWebViewOrientation orientation)
	{

		if (orientation == UniWebViewOrientation.Portrait)
		{
			return new UniWebViewEdgeInsets(5, 5, 5, 5);
		}
		else {
			return new UniWebViewEdgeInsets(5, 5, 5, 5);
		}
	}
}
