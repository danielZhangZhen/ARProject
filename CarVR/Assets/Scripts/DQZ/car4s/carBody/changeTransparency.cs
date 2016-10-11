using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class changeTransparency : UICtrler
{//隔热膜
    protected override void changeFunction(string name)
    {
        base.changeFunction(name);
        Color color = myUIButton[int.Parse(name)].GetComponent<Image>().color;

        shareMat.DOColor(new Color(color.r, color.g, color.b, 0.58f), 0.8f).SetEase(Ease.InOutCubic);
    }
}
