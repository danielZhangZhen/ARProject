using UnityEngine;


public class shareDQZ : MonoBehaviour {

    [SerializeField]
    private string shareName=null;
    public void share()
    {
        BtnScripts.Instance.ShareBtn(shareName);
    }

}
