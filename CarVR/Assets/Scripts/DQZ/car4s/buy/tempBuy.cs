using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class tempBuy : MonoBehaviour
{
    public static string buyWedName = "homePage";
    // Use this for initialization
    void Start()
    {
		this.GetComponent<Button>().SetOnClick(() => {
			BtnScripts.Instance.GetProductURL(CarBuyCompMgr.Ins.GetList()[0]);
		}
		);
    }


}



