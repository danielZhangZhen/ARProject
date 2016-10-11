//*************************************************
//** 姓名 ：张振
//** 日期 ：2016年1月4日
//** 类作用： 车身颜色更换
//*************************************************
using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class ColorTool : MonoBehaviour
{
    public Material shareMat;
    public Transform _Grid;
    private Button[] arryBtn;
    
    private Color Vtor(float r, float g, float b, float a)
    {
        r = r / 255;
        g = g / 255;
        b = b / 255;
        a = a / 255;
        return new Color(r, g, b, a);
    }

    void Awake()
    {

        arryBtn = _Grid.GetComponentsInChildren<Button>();
        if (arryBtn != null)
        {
            for (int i = 0; i < arryBtn.Length; i++)
            {
                Button btn = arryBtn[i];
                if (btn != null)
                {
                    btn.SetOnClick(() => ChangeColor(btn.GetComponent<Image>().color));
                }
            }
        }
    }
    private void ChangeColor(Color selectColor)
    {
        shareMat.DOColor(selectColor, 0.8f).SetEase(Ease.InOutCubic);
    }

}
