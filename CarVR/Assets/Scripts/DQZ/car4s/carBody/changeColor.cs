using UnityEngine;


public class changeColor : UICtrler
{
    protected override void changeFunction(string name)
    {
        base.changeFunction(name);
        Color myColor = myUIButton[getIndex].colors.normalColor;
        shareMat.color = myColor;
    
    }

}
