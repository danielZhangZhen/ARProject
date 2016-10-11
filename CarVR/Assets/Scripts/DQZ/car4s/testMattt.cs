using UnityEngine;
using System.Collections;

public class testMattt : MonoBehaviour {
    [SerializeField]
    private Material myMat;
    [ContextMenu("myMatPP")]
    void myMatPP()
    {
        Debug.Log("_Color"+" "+myMat.GetColor("_Color").ToString());
       // Debug.Log("_SpecColor"+" "+myMat.GetColor("_SpecColor").ToString());
       // Debug.Log("_AmbientColor" + " " + myMat.GetColor("_AmbientColor").ToString());
      //  Debug.Log("_AmbientColor2" + " " + myMat.GetColor("_AmbientColor2").ToString());
      //  Debug.Log("_ReflectionColor" + " " + myMat.GetColor("_ReflectionColor").ToString());


        //Debug.Log("_Shininess" + " " + myMat.GetFloat("_Shininess").ToString());
        //Debug.Log("_Reflect" + " " + myMat.GetFloat("_Reflect").ToString());
        //Debug.Log("_FresnelScale" + " " + myMat.GetFloat("_FresnelScale").ToString());
        //Debug.Log("_FresnelPower" + " " + myMat.GetFloat("_FresnelPower").ToString());
        //Debug.Log("_MetalicScale" + " " + myMat.GetFloat("_MetalicScale").ToString());
        //Debug.Log("_MetalicPower" + " " + myMat.GetFloat("_MetalicPower").ToString());
        //Debug.Log("_CandyScale" + " " + myMat.GetFloat("_CandyScale").ToString());
        //Debug.Log("_CandyPower" + " " + myMat.GetFloat("_CandyPower").ToString());

    }
}
