using UnityEngine;
using System.Collections;
using System;

public class OpenWeb : MonoBehaviour
{

    UniWebView _webView;
	public static string URL = "http://m.lmoar.com/";
    public GameObject UniWebViewObj;
    void Awake()
    {
        _webView = UniWebViewObj.GetComponent<UniWebView>();
        // Listen to some event of UniWebView
        _webView.OnLoadComplete += OnLoadComplete;
        _webView.OnReceivedMessage += OnReceivedMessage;

    }

    void Start()
    {
		
//		#if !UNITY_IOS
//
//		int size = 0;
//
//		size = Screen.height > Screen.width ? Screen.height : Screen.width;
//
//		_webView.insets.top = Convert.ToInt32(size * (120 / 1920f));
//
//		#endif

        if (String.IsNullOrEmpty(URL))
        {
            Debug.Log("URL 为空，打开默认地址");
			_webView.url = "http://m.lmoar.com/";
            URL = null;
            _webView.Show();
            _webView.Load();
        }
        else
        {
            _webView.url = URL;
            URL = null;
            _webView.Show();
            _webView.Load();
        }

    }

    public void ExsitBtn()
    {
        Debug.Log("Web Exist Btn Click!");
        _webView.Hide();
        //		BackController.Instance.WebBackBtn ();


    }

    void OnLoadComplete(UniWebView webView, bool success, string errorMessage)
    {
        if (success)
        {
            // Great, everything goes well. Show the web view now.
            webView.Show();
        }
        else
        {
            // Oops, something wrong.

            Debug.LogError("Something wrong in webview loading: " + errorMessage);
            //			ExsitBtn ();
        }
    }

    void OnReceivedMessage(UniWebView webView, UniWebViewMessage message)
    {
        Debug.Log(message.rawMessage);
		if (string.Equals(message.path,"home")) {
			Debug.Log ("Message Recevied, Home!");
			_webView.Hide ();
			//_webView.CleanCache ();
		}

        if (string.Equals(message.path, "quit"))
        {
            Debug.Log("Listend form webview, Web Exist Function is called !");
            // It is time to move!

            // In this example:
            // message.args["direction"] = "up"
            // message.args["distance"] = "1"
        }
    }

    //void OnGUI()
    //{
    //    GUIStyle text = new GUIStyle();
    //    text.alignment = TextAnchor.MiddleCenter;
    //    text.normal.textColor = Color.green;
    //    text.fontSize = 100;
    //    GUILayout.BeginHorizontal();
    //    GUILayout.Label(Tmp.ToString(), text);
    //    GUILayout.EndHorizontal();
    //}



    private bool Tmp;
    void OnApplicationPause(bool tmp)
    {
        //if (tmp)
        //{

        //    Tmp = GameObject.Find("Back").activeInHierarchy;
        //    Debug.Log(Tmp + "!!!!");
        //}
        //else
        //{
        //    Tmp = GameObject.Find("Back").activeInHierarchy;
        //    Debug.Log(Tmp + "@@@@@");
        //}
    }
}
