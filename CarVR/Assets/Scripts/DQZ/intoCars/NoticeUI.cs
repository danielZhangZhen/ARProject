using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class NoticeUI : MonoBehaviour
{
	public  Text NoticeMsg;
    public Transform cameraPos;
    public void EnterBtn ()
	{
        if (NoticeMsg.text == "详细查看 奥迪A1?")
        {
			BtnScripts.Instance.EnterCar("Audi_A1");
        }
        else if (NoticeMsg.text == "详细查看 奥迪A3?")
        {
			BtnScripts.Instance.EnterCar("Audi_A3");
		}
        else if (NoticeMsg.text == "详细查看 奥迪A4?")
        {
			BtnScripts.Instance.EnterCar("Audi_A4");
        }
        else if (NoticeMsg.text == "详细查看 奥迪A5?")
        {
			BtnScripts.Instance.EnterCar("Audi_A5");
        }
        else if (NoticeMsg.text == "详细查看 奥迪A6?")
        {
			BtnScripts.Instance.EnterCar("Audi_A6");
        }
        else if (NoticeMsg.text == "详细查看 奥迪A7?")
        {
			BtnScripts.Instance.EnterCar("Audi_A7");
        }
        else if (NoticeMsg.text == "详细查看 奥迪A8?")
        {
			BtnScripts.Instance.EnterCar("Audi_A8");
        }
        else if (NoticeMsg.text == "详细查看 奥迪Q3?")
        {
			BtnScripts.Instance.EnterCar("Audi_Q3");
        }
        else if (NoticeMsg.text == "详细查看 奥迪Q5?")
        {
			BtnScripts.Instance.EnterCar("Audi_Q5");
        }
        else if (NoticeMsg.text == "详细查看 奥迪Q7?")
        {
			BtnScripts.Instance.EnterCar("Audi_Q7");
        }

        else
        {
            CancleBtn();
        }
        //记录摄像机胶囊体坐标
        BackController.Instance.cameraPos = cameraPos.position;
        BackController.Instance.eulerAngles = cameraPos.eulerAngles;
        //Debug.Log("cameraPos.position" + cameraPos.position);
    }

	public void CancleBtn ()
	{
		
        StartCoroutine(canPre());
	}

    IEnumerator canPre()//先让其不能按，再执行不显示
    {
        TouchToEnter.canPress = false;
        yield  return new  WaitForSeconds(.1f);
        TouchToEnter.canPress = true;
        this.gameObject.SetActive(false);
    }

}
