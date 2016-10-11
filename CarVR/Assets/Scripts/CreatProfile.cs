using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;

public class CreatProfile : MonoBehaviour
{
	public string ShopName, ProfileVersion;
	public string iOSZhanTingURL, iOSZhanTingName, iOSZhanTingBundle, AndroidZhanTingURL, AndroidZhanTingName,AndroidZhanTingBundle, WebShopURL;
	public string[] RequestCarName, IOSCarURLs, IOSCarNames, AndroidCarURLS, AndroidCarNames;
	public int[] IOSCarBundles, AndroidCarBundles;
	public string[] WebRequestNames, WebURLS;
	public string[]ShareRequsetNames, ShareNames, ShareContents, ShareImages, ShareTitles, ShareDescription, ShareURL;

	void OnGUI ()
	{
		if (GUILayout.Button ("CreatProfile")) {
			CreatProfileToXML ();

		}
	}

	void CreatProfileToXML ()
	{
		string path = Application.dataPath + "/profile.xml";

		if (File.Exists (path))
			File.Delete (path);
	
		
		XmlDocument xmlDocument = new XmlDocument ();
		// 创建XML属性
		XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration ("1.0", "utf-8", null);
		xmlDocument.AppendChild (xmlDeclaration);
		// 创建XML根标志
		XmlElement rootXmlElement = xmlDocument.CreateElement ("root");
		// 创建Home标志
		XmlElement homeXmlElement = xmlDocument.CreateElement ("Home");
//		sceneXmlElement.SetAttribute("sceneName", sceneName);

		XmlElement ztInfoXmlElement = xmlDocument.CreateElement ("ZhanTingInfo");
		ztInfoXmlElement.SetAttribute ("iOSURL", iOSZhanTingURL.Remove(iOSZhanTingURL.LastIndexOf('.')-3,3));
		ztInfoXmlElement.SetAttribute ("iOSName", iOSZhanTingName);
		ztInfoXmlElement.SetAttribute ("iOSBundle",iOSZhanTingBundle);
		
		ztInfoXmlElement.SetAttribute ("AndroidURL", AndroidZhanTingURL.Remove(AndroidZhanTingURL.LastIndexOf('.')-3,3));
		ztInfoXmlElement.SetAttribute ("AndroidName", AndroidZhanTingName);
		XmlElement webShopxmlElement = xmlDocument.CreateElement ("WebShopURL");
		ztInfoXmlElement.SetAttribute ("AndroidBundle",AndroidZhanTingBundle);
		
		webShopxmlElement.SetAttribute ("WebShopURL", WebShopURL);

		homeXmlElement.AppendChild (ztInfoXmlElement);
		homeXmlElement.AppendChild (webShopxmlElement);


		XmlElement zhanTingXmlElement = xmlDocument.CreateElement ("ZhanTing");

		for (int i = 0; i < RequestCarName.Length; i++) {
			XmlElement carInfo = xmlDocument.CreateElement ("CarInfo");
			carInfo.SetAttribute ("RequestCarName", RequestCarName [i]);
			XmlElement iOSInfo = xmlDocument.CreateElement ("IOS");
			iOSInfo.SetAttribute ("URL", IOSCarURLs [i].Remove(IOSCarURLs[i].LastIndexOf('.')-3,3));
			iOSInfo.SetAttribute ("Name", IOSCarNames [i]);
			iOSInfo.SetAttribute ("BundleID", IOSCarBundles [i].ToString ());

			XmlElement AndroidInfo = xmlDocument.CreateElement ("Android");
			AndroidInfo.SetAttribute ("URL", AndroidCarURLS [i].Remove(AndroidCarURLS[i].LastIndexOf('.')-3,3));
			AndroidInfo.SetAttribute ("Name", AndroidCarNames [i]);
			AndroidInfo.SetAttribute ("BundleID", AndroidCarBundles [i].ToString ());

			carInfo.AppendChild (iOSInfo);
			carInfo.AppendChild (AndroidInfo);
			zhanTingXmlElement.AppendChild (carInfo);
			
		}

		XmlElement webSiteXmlElement = xmlDocument.CreateElement ("Web");
		for (int i = 0; i < WebRequestNames.Length; i++) {
			XmlElement webXmlElement = xmlDocument.CreateElement ("WebInfo");
			webXmlElement.SetAttribute ("Name", WebRequestNames [i]);
			webXmlElement.SetAttribute ("URL", WebURLS [i]);
			webSiteXmlElement.AppendChild (webXmlElement);
		}

	
		XmlElement shareInfoXmlElement = xmlDocument.CreateElement ("Share");
		for (int i = 0; i < ShareRequsetNames.Length; i++) {
			XmlElement shareXmlElement = xmlDocument.CreateElement ("ShareInfo");
			shareXmlElement.SetAttribute ("RequsetName", ShareRequsetNames [i]);
			shareXmlElement.SetAttribute ("Name", ShareNames [i]);
			shareXmlElement.SetAttribute ("Content", ShareContents [i]);
			shareXmlElement.SetAttribute ("Image", ShareImages [i]);
			shareXmlElement.SetAttribute ("Title", ShareTitles [i]);
			shareXmlElement.SetAttribute ("Description", ShareDescription [i]);
			shareXmlElement.SetAttribute ("URL", ShareURL [i]);

			shareInfoXmlElement.AppendChild (shareXmlElement);
		}




		rootXmlElement.AppendChild (homeXmlElement);
		rootXmlElement.AppendChild (zhanTingXmlElement);
		rootXmlElement.AppendChild (webSiteXmlElement);
		rootXmlElement.AppendChild (shareInfoXmlElement);


		xmlDocument.AppendChild (rootXmlElement);
		// 保存场景数据
		xmlDocument.Save (path);
		print ("Creat Profile Sucess");
	}






}

public class CarInfo
{
	string iOSURL, iOSName, androidURL, androidName;
	int iOSbundleID, androidBundleID;

	public CarInfo (string IOSURL, string IOSName, string AndroidURL, string AndroidName, int IOSBundleID, int AndroidBundleID)
	{
		iOSURL = IOSURL;
		iOSName = IOSName;
		androidURL = AndroidURL;
		androidName = AndroidName;
		iOSbundleID = IOSBundleID;
		androidBundleID = AndroidBundleID;
	}
}