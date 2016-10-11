using UnityEngine;
using UnityEngine.UI;
public class shoppingInfo : UICtrler
{
    [SerializeField]
    private string frontInfo;
    [SerializeField]
    private GameObject info;
    [SerializeField]
    private Button buyButton,quitButton,yesButton,noButton;
    [SerializeField]
    private Collider buttonCollider;
    void Awake()
    {
        buyButton.SetOnClick(pressBuyBtn);
        quitButton.SetOnClick(quitUI);
        yesButton.SetOnClick(confirmFun);
        noButton.SetOnClick(quitFun);
    }
    private void pressBuyBtn()
    {
        useF(true, false);
    }
    private void confirmFun()//确定
    {
        Debug.Log("你点确定了");
        useF(false, false);
    }
    private void quitFun()
    {
        useF(false, false);
    }
     private void quitUI()//返回ui时
    {
        useF(false, false);
    }
    protected sealed override void changeFunction(string name)
    {
        base.changeFunction(name);
        if (!buttonCollider.enabled) { buttonCollider.enabled = true; }
        Debug.Log(frontInfo + getIndex.ToString());

    }
   

    private void useF(bool setAct,bool collider)
    {
        info.SetActive(setAct);
        buttonCollider.enabled = collider;
    }
}
