using UnityEngine;
using System.Collections;

public class SucessController : PannelController
{
    public override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(WaitToCloseWindow(4f));
    }
    IEnumerator WaitToCloseWindow(float time)
    {
        yield return new WaitForSeconds(time);
        base.CloseWindow();
    }
    
}
