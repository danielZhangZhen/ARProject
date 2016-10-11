using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Xml;

public  class AnalysisProfile
{

	string path;

	public AnalysisProfile (string Path)
	{
		path = Path;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <returns>The ZT bundle.</returns>
	/// <param name="Ptf">平台信息.</param>
	/// <param name="version">此处可以传入“1.1.1”，也可传入“111”.</param>
	public BundleInstance GetZTBundle (Platform Ptf, string version)
	{
		BundleInstance bundleInstce;
		Debug.Log ("Start to get  ZhanTing instance");
		if (File.Exists (path)) {
			XmlDocument xmlDoc = new XmlDocument ();
			xmlDoc.Load (path);
			XmlNodeList nodeList = xmlDoc.SelectSingleNode ("root").ChildNodes;
			foreach (XmlElement mainList  in nodeList) {
				if (mainList.Name == "Home") {
					foreach (XmlElement obj in mainList.ChildNodes) {
						if (obj.Name == "ZhanTingInfo") {

							switch (Ptf) {
							case Platform.iOS:
								string zturlios = obj.GetAttribute ("iOSURL");
								zturlios = zturlios.Insert (zturlios.LastIndexOf ('.'), version.Replace (".", ""));
								string ztnameios = obj.GetAttribute ("iOSName");
								int ztbundleIDios =int.Parse (obj.GetAttribute ("iOSBundle"));
								bundleInstce = new BundleInstance (zturlios, ztnameios,ztbundleIDios);
								Debug.Log ("Get ios  zhanting bundle instance");
								return bundleInstce;

								break;
							case Platform.Android:
								string zturlandroid = obj.GetAttribute ("AndroidURL");
								zturlandroid = zturlandroid.Insert (zturlandroid.LastIndexOf ('.'), version.Replace (".", ""));
								string ztnameandroid = obj.GetAttribute ("AndroidName");
								int ztbundleIDandroid = int.Parse (obj.GetAttribute ("AndroidBundle"));
								bundleInstce = new BundleInstance (zturlandroid, ztnameandroid, ztbundleIDandroid);
								Debug.Log ("Get Andorid zhanting bundle instance");
								return bundleInstce;
								break;
							case Platform.UnityEditor:
								Debug.Log("pft is UnityEditor, return null instance");
								bundleInstce=null;
								return bundleInstce;
									break;
							default:
								break;
							}

						}
					}
				}
			}

		}
		Debug.Log ("profile doesn't exist, return null instance");
		bundleInstce=null;
		return bundleInstce;
	}

	public string GetWebShopURL ()
	{
		Debug.Log ("Start to get  Web sop url");
		if (File.Exists (path)) {
			XmlDocument xmlDoc = new XmlDocument ();
			xmlDoc.Load (path);
			XmlNodeList nodeList = xmlDoc.SelectSingleNode ("root").ChildNodes;
			foreach (XmlElement mainList  in nodeList) {
				if (mainList.Name == "Home") {
					foreach (XmlElement obj in mainList.ChildNodes) {
						if (obj.Name == "WebShopURL") {
							Debug.Log ("Get web shop url:" + obj.GetAttribute ("WebShopURL"));
							return obj.GetAttribute ("WebShopURL");
						}
							
					}
				}
			}
		}
	
		Debug.Log ("didn't get web shop url");
		return null;

	}

	public string GetProductURL (string productKey)
	{
		Debug.Log ("Start to get Product url");
		if (File.Exists (path)) {
			
			XmlDocument xmlDoc = new XmlDocument ();
			xmlDoc.Load (path);
			Debug.Log (xmlDoc.InnerText);
			XmlNodeList nodeList = xmlDoc.SelectSingleNode ("root").ChildNodes;
			foreach (XmlElement mainList  in nodeList) {
				if (mainList.Name == "Web") {
					foreach (XmlElement obj in mainList.ChildNodes) {
						if (obj.GetAttribute("Name") == productKey) {
							Debug.Log ("Get web shop url:" + obj.GetAttribute ("URL"));
							return obj.GetAttribute ("URL");
						}

					}
				}
			}
		}

		Debug.Log ("didn't get web shop url");
		return null;

	}

	public BundleInstance GetCarBundle (string RequestName, Platform Ptf, string version)
	{
		Debug.Log ("Start to get  car bundle instance");
		BundleInstance bundleInstce;
		if (File.Exists (path)) {
			XmlDocument xmlDoc = new XmlDocument ();
			xmlDoc.Load (path);
			XmlNodeList nodeList = xmlDoc.SelectSingleNode ("root").ChildNodes;
			foreach (XmlElement mainList  in nodeList) {
				if (mainList.Name == "ZhanTing") {
					foreach (XmlElement carinfoElement in mainList.ChildNodes) {
						if (carinfoElement.GetAttribute ("RequestCarName") == RequestName) {

							foreach (XmlElement bundleinfoXmlElement in carinfoElement) {
								switch (Ptf) {
								case Platform.iOS:
									if (bundleinfoXmlElement.Name == "IOS") {
										string carurlios = bundleinfoXmlElement.GetAttribute ("URL");
										carurlios = carurlios.Insert (carurlios.LastIndexOf ('.'), version.Replace (".", ""));
										string carnameios = bundleinfoXmlElement.GetAttribute ("Name");
										int carbundleIDios = int.Parse (bundleinfoXmlElement.GetAttribute ("BundleID"));
										bundleInstce = new BundleInstance (carurlios, carnameios, carbundleIDios);     
										Debug.Log ("Get ios car bundle instance");
										return bundleInstce;
									}
									break;
								case Platform.Android:
									if (bundleinfoXmlElement.Name == "Android") {
										string carurlandroid = bundleinfoXmlElement.GetAttribute ("URL");
										carurlandroid = carurlandroid.Insert (carurlandroid.LastIndexOf ('.'), version.Replace (".", ""));
										string carnameandroid = bundleinfoXmlElement.GetAttribute ("Name");
										int carbundleIDandroid = int.Parse (bundleinfoXmlElement.GetAttribute ("BundleID"));
										bundleInstce = new BundleInstance (carurlandroid, carnameandroid, carbundleIDandroid);
										Debug.Log ("Get android car bundle instance");
										return bundleInstce;
									}
									break;
								case Platform.UnityEditor:
									Debug.Log("pft is UnityEditor, return null instance");
									bundleInstce=null;
									return bundleInstce;
									break;
								default:
									break;
								}
							}
						}
					}
				}
			}
		}
		Debug.Log ("profile doesn't exist, return null instance");
		bundleInstce=null;
		return bundleInstce;
	}


	public ShareInstance GetShareInfo (string RequsetName)
	{
		Debug.Log ("Start to get  share info instance");
		ShareInstance shareInstance;
		if (File.Exists (path)) {
			XmlDocument xmlDoc = new XmlDocument ();
			xmlDoc.Load (path);
			XmlNodeList nodeList = xmlDoc.SelectSingleNode ("root").ChildNodes;
			foreach (XmlElement mainList  in nodeList) {
				if (mainList.Name == "Share") {

					foreach (XmlElement shareInsXMLElement in mainList) {
						if (shareInsXMLElement.GetAttribute ("RequsetName") == RequsetName) {
							string shareName = shareInsXMLElement.GetAttribute ("Name");
							string shareContent = shareInsXMLElement.GetAttribute ("Content");
							string shareImagee = shareInsXMLElement.GetAttribute ("Image");
							string shareTitle = shareInsXMLElement.GetAttribute ("Title");
							string shareDescription = shareInsXMLElement.GetAttribute ("Description");
							string shareURL = shareInsXMLElement.GetAttribute ("URL");
							shareInstance = new ShareInstance (shareName, shareContent, shareImagee, shareTitle, shareDescription, shareURL);
							return shareInstance;
						}
					
					}
				}
			}
		}
		Debug.Log ("profile doesn't exist, return null instance");
		shareInstance=null;
		return shareInstance;

	}


}

public enum Platform
{
	iOS,
	Android,
	UnityEditor,
};

public class BundleInstance
{
public string url, name;
public 	int bundleID;

	public BundleInstance (string Url, string Name, int BundleID)
	{
		url = Url;
		name = Name;
		bundleID = BundleID;
	}
}

public class ShareInstance
{
public 	string name, content, image, title, description, url;

	public ShareInstance (string Name, string Content, string Image, string Title, string Description, string URL)
	{
		name = Name;
		content = Content;
		image = Image;
		title = Title;
		description = Description;
		url = URL;
	}
}