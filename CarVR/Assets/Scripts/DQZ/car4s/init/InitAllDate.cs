using UnityEngine;
using UnityEngine.UI;

public class InitAllDate : MonoBehaviour {
    [SerializeField]
    private Button[] myUIButton;
    void Start()
    {
        init();
    }
    public void init()
    {
        for (int i = 0; i < myUIButton.Length; i++)
        {
            myUIButton[i].onClick.Invoke();
        }
    }
}
