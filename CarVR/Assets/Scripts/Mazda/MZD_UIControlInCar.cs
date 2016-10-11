using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using MojingSample.CrossPlatformInput;
using MojingSample.CrossPlatformInput.MojingInput;
using UnityEngine.EventSystems;

public class MZD_UIControlInCar : MonoBehaviour {

    private Button startVRBtn;
    private Button endVRBtn;
    
    private Cardboard cardboard;
    private CardboardHead cardboardHead;

    private CameraControlInCar camCtlInCar;

    private Canvas canvas;
    private GameObject cardboardReticle;

	//在VR模式需要隐藏的对象
	public GameObject interaction;
	public GameObject interactionUI;

    void Awake()
    {
        

        startVRBtn = transform.Find("StartVR").GetComponent<Button>();
        endVRBtn = transform.Find("Normal").GetComponent<Button>();
        cardboard = Camera.main.transform.Find("Cardboard").GetComponent<Cardboard>();
        
        cardboardHead = Camera.main.GetComponent<CardboardHead>();
        startVRBtn.onClick.AddListener(() => { OnStartVR(); });
        endVRBtn.onClick.AddListener(() => { OnEndVR(); });

        camCtlInCar = Camera.main.GetComponent<CameraControlInCar>();

        canvas = GetComponent<Canvas>();
        cardboardReticle = Camera.main.transform.Find("CardboardReticle").gameObject;
    }

    void Start()
    {
        //cardboard.VRModeEnabled = false;
        StartCoroutine(SyncCameraPosition());
    }

    void Update()
    {
        if ((CrossPlatformInputManager.GetButtonDown("C")&&canvas.enabled == false)|| cardboard.VRModeEnabled == true&&Input.GetMouseButtonDown(0))
        {
            OnEndVR();
        }
    }

    void OnStartVR()
    {
        cardboard.VRModeEnabled = true;
        //stereoCtl.enabled = true;
        //cardboard.gameObject.SetActive(true);
        cardboardHead.enabled = true;
        startVRBtn.gameObject.SetActive(false);
        ////使用状态AttachState.Disconnected 判断会出错，所以用state_down变量来判断
        //if (MojingInputManager.Instance.state_down.Contains("Mojing"))
        //{
            canvas.enabled = false;
        //}
        endVRBtn.gameObject.SetActive(true);
        cardboardReticle.SetActive(true);
        //StartCoroutine(SyncCameraPosition());
		if (interaction.activeSelf)
			interaction.SetActive (false);
		if (interactionUI.activeSelf)
			interactionUI.SetActive (false);
    }

    void OnEndVR()
    {
        cardboard.VRModeEnabled = false;
        cardboardHead.enabled = false;
        //stereoCtl.enabled = false;
        //cardboard.gameObject.SetActive(false);        
        endVRBtn.gameObject.SetActive(false);
        startVRBtn.gameObject.SetActive(true);
        //if (MojingInputManager.Instance.state_down.Contains("Mojing"))
        //{
            canvas.enabled = true;
        //}
        cardboardReticle.SetActive(false);
        Camera.main.transform.eulerAngles = Vector3.zero;

		if (!interaction.activeSelf)
			interaction.SetActive (true);
    }

    IEnumerator SyncCameraPosition()
    {
        yield return 20;
        cardboard.VRModeEnabled = false;
    }
}
