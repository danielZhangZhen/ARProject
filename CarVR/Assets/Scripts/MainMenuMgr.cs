using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MainMenuMgr : MonoBehaviour
{
    public Transform carColorGrid;      //车漆
    public Transform carHubGrid;        //车毂
    public Transform carFootPadGrid;    //车脚垫
    public Transform carWindowFilmGrid; //隔热膜
    public Transform carSeatGrid;       //座椅
    void Awake()
    {
        Transform mtran = transform;
        //菜单按钮
        Button[] menuBtns = mtran.GetComponentsInChildren<Button>();
        if (menuBtns != null)
        {
            for (int i = 0; i < menuBtns.Length; i++)
            {
                string name = menuBtns[i].name;
                menuBtns[i].SetOnClick(() => CarBuyCompMgr.Ins.SetCarItemName(name));
            }
        }

        //汽车零件索引按钮
        InitCall(carColorGrid);
        InitCall(carHubGrid);
        InitCall(carFootPadGrid);
        InitCall(carWindowFilmGrid);
        InitCall(carSeatGrid);
    }


    private void InitCall(Transform parent)
    {
        Button[] btns = parent.GetComponentsInChildren<Button>();
        if (btns != null)
        {
            for (int i = 0; i < btns.Length; i++)
            {
                string name = btns[i].name;
                int index;
                if (int.TryParse(name, out index))
                    btns[i].SetOnClick(() => CarBuyCompMgr.Ins.SetCompIndex(index));
            }
        }
    }



}
