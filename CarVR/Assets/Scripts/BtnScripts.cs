using UnityEngine;
using System.Collections;
using cn.sharesdk.unity3d;
using System;

public class BtnScripts : MonoBehaviour
{
    public static BtnScripts Instance;
    AnalysisProfile analysisProfile;
    string filePath;
    public string VersionCode;
    private ShareSDK ssdk;

	private string errorInfo;

    public LocalScene localScene;

    void Start()
    {

        //获取ssdk组件
        ssdk = GameObject.Find("BackControllerObj").GetComponent<ShareSDK>();
        //注册回调函数
        ssdk.shareHandler = ShareResultHandler;


        filePath = Application.persistentDataPath + "/profile.xml";
//		filePath = Application.dataPath + "/profile.xml";

        analysisProfile = new AnalysisProfile(filePath);
        Instance = GetComponent<BtnScripts>();

        Caching.maximumAvailableDiskSpace = 1024 * 1024 * 400;//400M
        Caching.expirationDelay = 604800;//7天
        //
        //

        //


        //		#if UNITY_ANDROID
        //		ShareSDK.open("d271ad8710f5");
        //		#endif
        ////
        //		#if UNITY_IPHONE
        //		ShareSDK.open("d27156cf1600");
        //		#endif

        #region ShareSDK 编辑区
        //Sina Weibo**************************************************************************************************
        //		Hashtable sinaWeiboConf = new Hashtable ();
        //		sinaWeiboConf.Add ("app_key", "568898243");
        //		sinaWeiboConf.Add ("app_secret", "38a4f8204cc784f81f9f0daaf31e02e3");
        //		sinaWeiboConf.Add ("redirect_uri", "http://.sharesdk.cn");
        //		ShareSDK.setPlatformConfig (PlatformType.SinaWeibo, sinaWeiboConf);

        ////Tencent Weibo
        //Hashtable tcConf = new Hashtable();
        //tcConf.Add("app_key", "801307650"); 
        //tcConf.Add("app_secret", "ae36f4ee3946e1cbb98d6965b0b2ff5c");
        //tcConf.Add("redirect_uri", "http://.sharesdk.cn");
        //ShareSDK.setPlatformConfig(PlatformType.TencentWeibo, tcConf);

        //SMS**************************************************************************************************
        //		ShareSDK.setPlatformConfig(PlatformType.SMS, null);

        //QZone**************************************************************************************************
        //		Hashtable qzConf = new Hashtable ();
        //		qzConf.Add ("app_id", "100371282");
        //		qzConf.Add ("app_key", "aed9b0303e3ed1e27bae87c33761161d");
        //		ShareSDK.setPlatformConfig (PlatformType.QZone, qzConf);

        //WeChat**************************************************************************************************
        //Hashtable wcConf = new Hashtable();
        //wcConf.Add("app_id", "wx2d14235b2703abfa");
        //ShareSDK.setPlatformConfig(PlatformType.WeChatSession, wcConf);
        //ShareSDK.setPlatformConfig(PlatformType.WeChatTimeline, wcConf);
        //ShareSDK.setPlatformConfig(PlatformType.WeChatFav, wcConf);

        //QQ**************************************************************************************************
        //		Hashtable qqConf = new Hashtable ();
        //		qqConf.Add ("app_id", "100371282");
        //		ShareSDK.setPlatformConfig (PlatformType.QQ, qqConf);
        //		
        ////Facebook
        //Hashtable fbConf = new Hashtable();
        //fbConf.Add("api_key", "107704292745179");
        //fbConf.Add("app_secret", "38053202e1a5fe26c80c753071f0b573");
        //ShareSDK.setPlatformConfig(PlatformType.Facebook, fbConf);

        ////Twitter
        //Hashtable twConf = new Hashtable();
        //twConf.Add("consumer_key", "mnTGqtXk0TYMXYTN7qUxg");
        //twConf.Add("consumer_secret", "ROkFqr8c3m1HXqS3rm3TJ0WkAJuwBOSaWhPbZ9Ojuc");
        //twConf.Add("redirect_uri", "http://.sharesdk.cn");
        //ShareSDK.setPlatformConfig(PlatformType.Twitter, twConf);

        ////Google+
        //Hashtable gpConf = new Hashtable();
        //gpConf.Add("client_id", "232554794995.apps.googleusercontent.com");
        //gpConf.Add("client_secret", "PEdFgtrMw97aCvf0joQj7EMk");
        //gpConf.Add("redirect_uri", "http://localhost");
        //ShareSDK.setPlatformConfig(PlatformType.GooglePlus, gpConf);

        //RenRen**************************************************************************************************
        //		Hashtable rrConf = new Hashtable();
        //		rrConf.Add("app_id", "226427");
        //		rrConf.Add("app_key", "fc5b8aed373c4c27a05b712acba0f8c3");
        //		rrConf.Add("secret_key", "f29df781abdd4f49beca5a2194676ca4");
        //		ShareSDK.setPlatformConfig(PlatformType.Renren, rrConf);
        //		
        //		//KaiXin
        //		Hashtable kxConf = new Hashtable();
        //		kxConf.Add("api_key", "358443394194887cee81ff5890870c7c");
        //		kxConf.Add("secret_key", "da32179d859c016169f66d90b6db2a23");
        //		kxConf.Add("redirect_uri", "http://.sharesdk.cn/");
        //		ShareSDK.setPlatformConfig(PlatformType.Kaixin, kxConf);

        //Mail**************************************************************************************************
        //		ShareSDK.setPlatformConfig (PlatformType.Mail, null);

        ////Print
        //ShareSDK.setPlatformConfig(PlatformType.Print, null);

        ////Copy
        //ShareSDK.setPlatformConfig(PlatformType.Copy, null);

        //Sohu Weibo
        //Hashtable shwbConf = new Hashtable();
        //shwbConf.Add("consumer_key", "SAfmTG1blxZY3HztESWx");
        //shwbConf.Add("consumer_secret", "yfTZf)!rVwh*3dqQuVJVsUL37!F)!yS9S!Orcsij");
        //shwbConf.Add("callback_uri", "http://.sharesdk.cn");
        //ShareSDK.setPlatformConfig(PlatformType.SohuWeibo, shwbConf);

        //NetEase Weibo
        //Hashtable netConf = new Hashtable();
        //netConf.Add("consumer_key", "T5EI7BXe13vfyDuy");
        //netConf.Add("consumer_secret", "gZxwyNOvjFYpxwwlnuizHRRtBRZ2lV1j");
        //netConf.Add("redirect_uri", "http://.shareSDK.cn");
        //ShareSDK.setPlatformConfig(PlatformType.NetEaseWeibo, netConf);

        ////Dropbox
        //Hashtable dbConf = new Hashtable();
        //dbConf.Add("api_key", "02e2cbe5ca06de5908a863b15e149b0b");
        //dbConf.Add("secret", "9f1e7b4f71304f2f");
        //dbConf.Add("redirect_uri", "http://.sharesdk.cn");
        //ShareSDK.setPlatformConfig(PlatformType.DouBan, dbConf);

        ////Evernote
        //Hashtable enConf = new Hashtable();
        //enConf.Add("consumer_key", "sharesdk-7807");
        //enConf.Add("consumer_secret", "d05bf86993836004");
        //enConf.Add("host_type", "0");
        //ShareSDK.setPlatformConfig(PlatformType.Evernote, enConf);

        ////LinkedIn
        //Hashtable liConf = new Hashtable();
        //liConf.Add("api_key", "ejo5ibkye3vo");
        //liConf.Add("secret_key", "cC7B2jpxITqPLZ5M");
        //liConf.Add("redirect_url", "http://sharesdk.cn");
        //ShareSDK.setPlatformConfig(PlatformType.LinkedIn, liConf);

        ////Pinterest
        //Hashtable pinConf = new Hashtable();
        //pinConf.Add("client_id", "1432928");
        //ShareSDK.setPlatformConfig(PlatformType.Pinterest, pinConf);

        ////Pocket
        //Hashtable pocketConf = new Hashtable();
        //pocketConf.Add("consumer_key", "11496-de7c8c5eb25b2c9fcdc2b627");
        //pocketConf.Add("redirect_uri", "pocketapp1234");
        //ShareSDK.setPlatformConfig(PlatformType.Pocket, pocketConf);

        ////Instapaper
        //Hashtable ipConf = new Hashtable();
        //ipConf.Add("consumer_key", "4rDJORmcOcSAZL1YpqGHRI605xUvrLbOhkJ07yO0wWrYrc61FA");
        //ipConf.Add("consumer_secret", "GNr1GespOQbrm8nvd7rlUsyRQsIo3boIbMguAl9gfpdL0aKZWe");
        //ShareSDK.setPlatformConfig(PlatformType.Instapaper, ipConf);

        //YouDaoNote
        //Hashtable ydConf = new Hashtable();
        //ydConf.Add("consumer_key", "dcde25dca105bcc36884ed4534dab940");
        //ydConf.Add("consumer_secret", "d98217b4020e7f1874263795f44838fe");
        //ydConf.Add("oauth_callback", "http://.sharesdk.cn/");
        //ydConf.Add("host_type", "1");
        //ShareSDK.setPlatformConfig(PlatformType.YouDaoNote, ydConf);

        ////Sohu SuiShenKan
        //Hashtable shkConf = new Hashtable();
        //shkConf.Add("app_key", "e16680a815134504b746c86e08a19db0");
        //shkConf.Add("app_secret", "b8eec53707c3976efc91614dd16ef81c");
        //shkConf.Add("redirect_uri", "http://sharesdk.cn");
        //ShareSDK.setPlatformConfig(PlatformType.SohuKan, shkConf);

        ////Flickr
        //Hashtable flickrConf = new Hashtable();
        //flickrConf.Add("api_key", "33d833ee6b6fca49943363282dd313dd");
        //flickrConf.Add("api_secret", "3a2c5b42a8fbb8bb");
        //ShareSDK.setPlatformConfig(PlatformType.Flickr, flickrConf);

        ////Tumblr
        //Hashtable tumblrConf = new Hashtable();
        //tumblrConf.Add("consumer_key", "2QUXqO9fcgGdtGG1FcvML6ZunIQzAEL8xY6hIaxdJnDti2DYwM");
        //tumblrConf.Add("consumer_secret", "3Rt0sPFj7u2g39mEVB3IBpOzKnM3JnTtxX2bao2JKk4VV1gtNo");
        //tumblrConf.Add("callback_url", "http://sharesdk.cn");
        //ShareSDK.setPlatformConfig(PlatformType.Tumblr, tumblrConf);

        ////Dropbox
        //Hashtable dropboxConf = new Hashtable();
        //dropboxConf.Add("app_key", "7janx53ilz11gbs");
        //dropboxConf.Add("app_secret", "c1hpx5fz6tzkm32");
        //ShareSDK.setPlatformConfig(PlatformType.Dropbox, dropboxConf);

        ////Instagram
        //Hashtable instagramConf = new Hashtable();
        //instagramConf.Add("client_id", "ff68e3216b4f4f989121aa1c2962d058");
        //instagramConf.Add("client_secret", "1b2e82f110264869b3505c3fe34e31a1");
        //instagramConf.Add("redirect_uri", "http://sharesdk.cn");
        //ShareSDK.setPlatformConfig(PlatformType.Instagram, instagramConf);

        ////VK
        //Hashtable vkConf = new Hashtable();
        //vkConf.Add("application_id", "3921561");
        //vkConf.Add("secret_key", "6Qf883ukLDyz4OBepYF1");
        //ShareSDK.setPlatformConfig(PlatformType.VKontakte, vkConf);
        #endregion
    }

    public void SingleBtn()
    {
        Hashtable content = new Hashtable();

        content["content"] = "广西南奥汽车销售服务有限公司";

        content["image"] = "http://upload.17u.net/uploadpicbase/2010/04/07/aa/2010040714551817545.jpg";

        content["title"] = "广西南奥汽车销售服务有限公司";

        content["description"] = "广西南奥汽车销售服务有限公司";

        content["url"] = "http://m.lmoar.com:18001/index.php";

        content["siteUrl"] = "http://m.lmoar.com:18001/index.php";

        content["site"] = "广西南奥汽车销售服务有限公司";

        content["musicUrl"] = "http://mp3.mwap8.com/destdir/Music/2009/20090601/ZuiXuanMinZuFeng20090601119.mp3";

        ssdk.ShowShareMenu(null, content, 100, 100);
    }

	public void MoYanShareBtn(Hashtable content){
		ssdk.ShowShareMenu(null, content, 100, 100);
	}


    public void ShareBtn(string ShareRequsetName)
    {


        if (analysisProfile.GetShareInfo(ShareRequsetName) == null)
        {
            print("share ins is null");
            Hashtable content = new Hashtable();

            content["content"] = "广西南奥汽车销售服务有限公司";

            content["image"] = "http://upload.17u.net/uploadpicbase/2010/04/07/aa/2010040714551817545.jpg";

            content["title"] = "广西南奥汽车销售服务有限公司";

            content["description"] = "广西南奥汽车销售服务有限公司";

            content["url"] = "http://m.lmoar.com:18001/index.php";

            content["siteUrl"] = "http://m.lmoar.com:18001/index.php";

            content["site"] = "广西南奥汽车销售服务有限公司";

            content["musicUrl"] = "http://mp3.mwap8.com/destdir/Music/2009/20090601/ZuiXuanMinZuFeng20090601119.mp3";

            ssdk.ShowShareMenu(null, content, 100, 100);
        }
        else
        {
            ShareInstance shareIns = analysisProfile.GetShareInfo(ShareRequsetName);
            Debug.Log("SharInfo Get!");
            Debug.Log(shareIns.url);
            Debug.Log(shareIns.name);
            Debug.Log(shareIns.title);
            Debug.Log(shareIns.image);
            Debug.Log(shareIns.description);

            Hashtable content = new Hashtable();

            content["content"] = shareIns.content;

            content["image"] = shareIns.image;

            content["title"] = shareIns.title;

            content["description"] = shareIns.description;

            content["url"] = shareIns.url;

            content["siteUrl"] = shareIns.url;

            content["site"] = "灵墨汽车";

			content["type"] = ContentType.Webpage;

            content["musicUrl"] = "http://mp3.mwap8.com/destdir/Music/2009/20090601/ZuiXuanMinZuFeng20090601119.mp3";

            ssdk.ShowShareMenu(null, content, 100, 100);
        }
        //creat share content 

        //		name = Name;
        //		content = Content;
        //		image = Image;
        //		title = Title;
        //		description = Description;
        //		url = URL;
    }

    public void OpenWebURL()
    {
        OpenWeb.URL = analysisProfile.GetWebShopURL();
		Debug.Log ( analysisProfile.GetWebShopURL());
        Application.LoadLevel("V1_Web");
    }

	public void GetProductURL(string WebRequsetName)
	{
		OpenWeb.URL = analysisProfile.GetProductURL(WebRequsetName);

		if (String.IsNullOrEmpty(analysisProfile.GetProductURL(WebRequsetName))) {
			//如果找不到该Key
			//使用通用的Key来解析地址
			if (WebRequsetName.Contains("CarHub")) {
				BtnScripts.Instance.GetProductURL ("CarHub");
			}
			else if (WebRequsetName.Contains("CarFootPad")) {
				BtnScripts.Instance.GetProductURL ("CarFootPad");
			}
			else if (WebRequsetName.Contains("CarWindowFilm")) {
				BtnScripts.Instance.GetProductURL ("CarWindowFilm");
			}
			else if (WebRequsetName.Contains("CarSeat")) {
				BtnScripts.Instance.GetProductURL ("CarSeat");
			}
			else if (WebRequsetName.Contains("CarColor")) {
				BtnScripts.Instance.GetProductURL ("CarColor");
			} else {
				BtnScripts.Instance.OpenWebURL ();
			}
		} else {
			Screen.orientation = ScreenOrientation.Portrait;
			Application.LoadLevel("V1_Web");
		}

//		       
	}

    public void EnterCar(string CarRequestName)
    {
		V1_Loading.LocalSceneName = CarRequestName;
		if (Screen.orientation == ScreenOrientation.Portrait)
		{
			Application.LoadLevel("V1_Loding_tall");
		}
		else
		{
			Application.LoadLevel("V1_Loding_wide");
		}
		return;

		#region 所有汽车场景在版本1.0中不再下载使用
//        BundleInstance bundle;
//#if UNITY_IOS
//		bundle=	analysisProfile.GetCarBundle (CarRequestName, Platform.iOS,VersionCode);
//#endif
//#if	UNITY_ANDROID
//		bundle=	analysisProfile.GetCarBundle (CarRequestName, Platform.Android,VersionCode);
//#elif UNITY_EDITOR
//        bundle = analysisProfile.GetCarBundle(CarRequestName, Platform.UnityEditor, VersionCode);
//#endif
//        //download scene
//        V1_Loading.DownloadURL = bundle.url;
//        V1_Loading.DownloadSceneName = bundle.name;
//        V1_Loading.DownloadBundleID = bundle.bundleID;
//
//        //		BackController.Instance.BundleID=bundle.bundleID;
//        //		BackController.Instance.BundleName=bundle.name;
//        //		BackController.Instance.BundleURL=bundle.url;
//
//
//        if (Screen.orientation == ScreenOrientation.Portrait)
//        {
//            Application.LoadLevel("V1_Loding_tall");
//        }
//        else
//        {
//            Application.LoadLevel("V1_Loding_wide");
//        }
		#endregion
    }



    void ShareResultHandler(ResponseState state, PlatformType type, Hashtable shareInfo, Hashtable error, bool end)
    {
        if (state == ResponseState.Success)
        {
            print("share result :");
            print(MiniJSON.jsonEncode(shareInfo));
        }
        else if (state == ResponseState.Fail)
        {
            print("fail! error code = " + error["error_code"] + "; error msg = " + error["error_msg"]);
			errorInfo = "fail! error code = " + error ["error_code"] + "; error msg = " + error ["error_msg"];
        }
        else if (state == ResponseState.Cancel)
        {
            print("cancel !");
        }
    }

    //分享回调
    void ShareResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable result)
    {
        if (state == ResponseState.Success)
        {
            print("share successfully - share result :");
            print(MiniJSON.jsonEncode(result));
        }
        else if (state == ResponseState.Fail)
        {
#if UNITY_ANDROID
            print("fail! throwable stack = " + result["stack"] + "; error msg = " + result["msg"]);
#elif UNITY_IPHONE
			print ("fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
#endif
        }
        else if (state == ResponseState.Cancel)
        {
            print("cancel !");
        }
    }

//	void OnGUI(){
//		GUI.TextArea (new Rect (0, 100, Screen.width, 100), errorInfo);
//	}
}
