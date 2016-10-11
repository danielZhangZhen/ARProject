using UnityEngine;
using System.Collections;
using System;

public class DataTest : MonoBehaviour {

	public string url;

	DateTime time;
	WWW www;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator StartToRqst(WWW www)
	{
		yield return www;
		if (www.isDone)
		{
			Debug.Log("======"+www.text);
			time=Convert.ToDateTime( www.text);
			Debug.Log(time);
			Debug.Log(time.Date);//+time.Hour.ToString());
		}
	}

	void OnGUI()
	{
		GUILayout.Label ("");
		if (GUILayout.Button("Start")) {
			www =new WWW(url);
			StartCoroutine(StartToRqst(www));
		}
	}
}
