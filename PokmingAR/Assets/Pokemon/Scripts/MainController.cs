using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using System;
using LitJson;

/// <summary>
/// 捕捉脚本
/// </summary>
public class MainController : MonoBehaviour
{
    [SerializeField]
    private Image Img_CatchArea;

    [SerializeField]
    private AudioClip AudioClip_ReadyCatch;

    [SerializeField]
    private Button Btn_Catch;

    [SerializeField]
    private Text Text_ErrorMsg;

    [SerializeField]
    private GameObject Obj_Pannel_Info, Obj_Pannel_Bag, Obj_Pannel_FailNotice,Obj_Pannel_Sucess;

    [SerializeField]
    private GameObject[] Pokemons;

    private PokemonController mPoke;

    [SerializeField]
    private PokemonCloudRecoEvtHandler pokemonCloudRecoEvt;

    string timeOutMsg = "小妖出现后，只有<Color=red>3秒钟</color>的时间捕捉，下次抓紧时间哦！";

    Tweener tweener;

    string deviceUniqueIdentifier;

    bool b_PokemonExist;

    bool b_EnteredCatchState;//是否已经在圆圈内

    /*
     * 失败的情况：
     * 1.超时
     * 2.失败，返回结果并显示在界面上
     */
    void Start()
    {
        //设备唯一识别码
        deviceUniqueIdentifier = SystemInfo.deviceUniqueIdentifier;
        tweener = Btn_Catch.transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.5f).SetLoops(-1);//设置循环播放（-1为一直循环）
        tweener.SetEase(Ease.Linear);
        tweener.Rewind();

        b_EnteredCatchState = false;

        pokemonCloudRecoEvt.PokemonCloudRecoAction = CloudRecoSucess;


    }
    

    void Update()
    {
        if (b_PokemonExist)
        {
            RayDetection();
        }
        else
        {
            OutCatchState();
        }

    }

    void CloudRecoSucess(string data)
    {
        
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Pokemon");
        if (objs.Length==0)
        {
            WWWForm form = new WWWForm();
            form.AddField("photo_id", data);
            form.AddField("device_id", deviceUniqueIdentifier);

            Debug.Log("device_id："+ deviceUniqueIdentifier);
            Debug.Log("photo_id：" + data);

            //WWW www = new WWW("http://192.168.1.134:3000/api/ar_activity/scan?photo_id="+ data + "&device_id="+ deviceUniqueIdentifier);
			WWW www = new WWW("http://m.lmoar.com/api/ar_activity/scan",form);

            StartCoroutine(StrartScanRequest(www));           
        }
    }

    IEnumerator StrartScanRequest(WWW www)
    {
        yield return www;
        Debug.Log("ScanRequestResult:"+www.text);
        JsonData jd = JsonMapper.ToObject(www.text);

        if (((IDictionary)jd).Contains("result"))
        {
            if ((int)jd["result"] == -1)
            {
                string str = (string)jd["message"];
                ShowErrorMsg(str);
            }
            else
            {
                InitPokemon();
            }
        }
        else
        {
            ShowErrorMsg("服务器错误");
        }


          
  
    }

    void RayDetection()
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.name.StartsWith("Pokemon"))
            {
                EnterCatchState();
            }
        }
        else
        {
            OutCatchState();
        }
    }

    void EnterCatchState()
    {
        if (!b_EnteredCatchState)
        {
            b_EnteredCatchState = true;
        }
        else
        {
            return;
        }
        //将圆圈变绿
        if (Img_CatchArea.color != new Color(139 / 255f, 186 / 255f, 143 / 255f, 1))
        {
            Img_CatchArea.color = new Color(139 / 255f, 186 / 255f, 143 / 255f, 1);
        }
        //播放Ready提示音
        AudioSource.PlayClipAtPoint(AudioClip_ReadyCatch, Vector3.zero);
        //捕捉按钮播放缩放动画
        if (!tweener.IsPlaying())
        {
            tweener.Restart();
        }

    }

    void OutCatchState()
    {
        if (b_EnteredCatchState)
        {
            b_EnteredCatchState = false;
        }
        else
        {
            return;
        }
        //将圆圈变红
        Img_CatchArea.color = new Color(0.729f, 0.564f, 0.545f, 1);

        //捕捉按钮恢复原状
        tweener.Rewind();
        Debug.Log("Rewind");
    }
    private void PokemonTimeOut()
    {
        b_PokemonExist = false;
        ShowErrorMsg(timeOutMsg);
    }

  public   void ShowErrorMsg(string msg)
    {
        ClearNoticePannels();
        Obj_Pannel_FailNotice.gameObject.SetActive(true);
        Text_ErrorMsg.text = msg;
    }

    void InitPokemon()
    {
        int i = Pokemons.Length;
        System.Random ran = new System.Random();
        int n = ran.Next(0, i);
        Pokemons[n].SetActive(true);
        b_PokemonExist = true;


        Pokemons[n].GetComponent<PokemonController>().PokemonTimeOutAction = PokemonTimeOut;
        Pokemons[n].GetComponent<PokemonController>().PokemonPauseRecoAction = PauseCloudReco;

    }
 


    #region Bag、Info和捕捉按钮
   public void BtnBag()
    {
        ClearNoticePannels();

        Obj_Pannel_Bag.SetActive(true);

    }
    public void BtnInfo()
    {
        ClearNoticePannels();

        Obj_Pannel_Info.SetActive(true);

    }
    public void BtnCatch()
    {
        if (b_EnteredCatchState)
        {
            tweener.Rewind();
            CatchSucess();
        }  
        else
        {
            ShowErrorMsg("请将手机对准海报，出现小精灵后再点击捕捉按钮");
        }
    }
    #endregion

  
    void CatchSucess()
    {
        ClearNoticePannels();
        GameObject[] Pokemons = GameObject.FindGameObjectsWithTag("Pokemon");
        if (Pokemons.Length!=0)
        {
            foreach (GameObject item in Pokemons)
            {
                item.SetActive(false);
            }
        }
        Obj_Pannel_Sucess.SetActive(true);
        string name = PlayerPrefs.GetString("ActivityName");
        int i = PlayerPrefs.GetInt(name);
        PlayerPrefs.SetInt(name, i + 1);

    }



    private void ClearNoticePannels()
    {
        GameObject[] NoticePanels = GameObject.FindGameObjectsWithTag("NoticePannel");

        //避免混乱，显示UI前，先禁用掉其他的通知界面
        if (NoticePanels.Length != 0)
            foreach (GameObject item in NoticePanels)
            {
                item.SetActive(false);
            }
    }
/// <summary>
/// 暂停云识别
/// </summary>
/// <param name="b_stopCam">是否停止摄像头</param>
    public void PauseCloudReco(bool b_stopCam)
    {
        Debug.Log("暂停云识别");
        pokemonCloudRecoEvt.PauseCloudReco(b_stopCam);
    }
    /// <summary>
    /// 恢复云识别
    /// </summary>
    public void ResumeCloudReco()
    {
        Debug.Log("恢复云识别");
        pokemonCloudRecoEvt.CuntinueCloudReco();
    }
}
