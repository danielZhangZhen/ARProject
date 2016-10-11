using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class forMe : MonoBehaviour
{

    private Image[] thisUi;

    [ContextMenu("BuildMap")]
    void BuildMap()
    {
        thisUi = this.GetComponentsInChildren<Image>();
        for (int i = 0; i < thisUi.Length; i++)
        {
            Vector2 vec = new Vector2(90f, 90f);
            thisUi[i].rectTransform.sizeDelta = vec;
        }
    }


}
