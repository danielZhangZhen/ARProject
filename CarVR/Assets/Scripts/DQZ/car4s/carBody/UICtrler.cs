using UnityEngine;
using UnityEngine.UI;
public abstract class UICtrler : MonoBehaviour
{

    private int intUINumbers;//总数
    public Material shareMat;
    [SerializeField]
    private GameObject haveButtonObject;
    protected Button[] myUIButton;


    void Awake()
    {
        if (!haveButtonObject)
            return;

        intUINumbers = haveButtonObject.transform.childCount;
        if (intUINumbers > 0)
        {
            myUIButton = new Button[intUINumbers];
            myUIButton = haveButtonObject.GetComponentsInChildren<Button>();
            for (int i = 0; i < intUINumbers; i++)
            {
                // EventDelegate.Add(myUIButton[i].onClick, changeFunction);
                string name = myUIButton[i].name;
                myUIButton[i].SetOnClick(() =>
                {    
                    changeFunction(name);
                });
            }
        }

    }
    protected virtual void changeFunction(string name)
    {
        _getIndex = int.Parse(name);
    }
    private int _getIndex;
    //获取当前最后一个被点击按钮的索引
    protected int getIndex
    {
        get
        {
            return _getIndex;
        }
    }
}
