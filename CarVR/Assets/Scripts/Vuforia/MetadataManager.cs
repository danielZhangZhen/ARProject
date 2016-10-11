using UnityEngine;
using System.Collections;
/*
shareIns.content;

            content["image"] = shareIns.image;

            content["title"] = shareIns.title;

            content["description"] = shareIns.description;

            content["url"] = shareIns.url;


*/
public class Metadata{
	public string name = null;
	public string type = null;
	public string androidurl = null;
	public string iphoneurl = null;
	public string weburl = null;
	public string editorurl = null;
	public int version = -1;
	public int id = -1;
	public int only = -1;
	public bool useLight = false;
	//shareSDK 相关
	public string imageurlShare = null;
	public string contentShare = null;
	public string titleShare = null;
	public string descriptionShare = null;
	public string urlShare = null;

	//UI controller
	public bool isMoreUIShow = false;
}

public class MetadataManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Metadata meta = new Metadata ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
