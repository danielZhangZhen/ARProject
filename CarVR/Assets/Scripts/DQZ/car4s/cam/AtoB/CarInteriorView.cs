using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CarInteriorView : AtoBpoint
{

#region 所有要设的参数
   
    public Transform myT = null, myC = null, fromPoint = null;//前面是两个追随者，一个是被追随的目标点，一个是要去追随的点，到达点，一个是中间点，返回点
    public Transform toPoint = null, midPoint = null;
    public float waitTimeInMiddle = 1;
    public myTool.MouseFollowRotation myMouse;
    public myTool.autoRotating myAutoRun;
    public turnInterorOfCar myTurn;
    public Button AtoB_Button = null, BtoA_Button = null;
    private static bool goingToTarget=false;
#endregion
    void Start()
    {
        base.smooth = 5;//追上去的效果，越高越快，但不细致
        base.MaxDistance = .02f;//这个是表示接近时就停下来
        AtoB_Button.SetOnClick(playToThis);
        BtoA_Button.SetOnClick(playToThat);
        
    }
    IEnumerator myInRoom(Transform Middle, Transform NexttoPoint, bool isIn)//进入时，两个都关,mouse 与auto,isIn是 进去
    {
        if (isIn)//返回有大问题，原因在前面记录点
        {

            myTurn.enabled = false;
            goingToTarget = true;
            myT = Middle;
            yield return new WaitForSeconds(waitTimeInMiddle);
            myT = NexttoPoint;
            yield return new WaitForSeconds(waitTimeInMiddle);
            myMouse.enabled = true;
            myAutoRun.enabled = true;
            goingToTarget = false;
        }
        else//这是进入车内
        {
            fromPoint.position = myC.position;//记一下从来那里来的，返回时用,不要用等号
            fromPoint.rotation = myC.rotation;
            goingToTarget = true;
            myMouse.enabled = false;
            myAutoRun.enabled = false;
            myT = Middle;
            yield return new WaitForSeconds(waitTimeInMiddle);
            myT = NexttoPoint;
            yield return new WaitForSeconds(waitTimeInMiddle);
            goingToTarget = false;
            myTurn.enabled = true;
            myTurn.ResetXY();
        }
    }

    protected sealed override void AtoB(Transform myca, Transform mytr)//相机+目标点
    {   
     
     
    }

    void LateUpdate()
    {
        if (goingToTarget)//当goingToTarget为true时就进入
        {
            AtoB(myC, myT);
        }
    }
    public void playToThis()//进入
    {
        if (!goingToTarget)
            StartCoroutine(myInRoom(midPoint, toPoint, false));
    }
    public void playToThat()//返回
    {
        if (!goingToTarget)
            StartCoroutine(myInRoom(midPoint, fromPoint, true));
    }

}
