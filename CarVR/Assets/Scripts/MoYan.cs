using UnityEngine;
using System.Collections;

public class MoYan : MonoBehaviour {

public void Back()
	{
		BackController.Instance.BackBtn ();
	}

	public void ShareTest()
	{
		BtnScripts.Instance.ShareBtn ("MoYan");
	}
}
