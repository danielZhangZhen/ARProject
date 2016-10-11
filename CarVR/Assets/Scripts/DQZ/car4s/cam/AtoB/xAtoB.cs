using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class xAtoB : AtoBpoint
{
    public Transform myT = null, myC = null, fromPoint = null, Center = null;//前面是两个追随者，一个是被追随的目标点，一个是要去追随的点，到达点，一个是中间点，返回点
    public Transform[] toPoint = null, midPoint = null, MousePOV = null;
    public float waitTimeInMiddle = 1;
    public myTool.MouseFollowRotation myMouse;
    public Vector2[] myVxy;
    public myTool.autoRotating myAutoRun;
    public Button[] AtoB_Button = null, BtoA_Button = null;
    private static float myX;
    private static bool goingToTarget = false, inCarFloorOrChair = false;//一个是表示是否正在运动，另一个表示是否进入脚垫与座椅
    void Start()
    {
        inCarFloorOrChair = false;
        myMouse.enabled = true;
        myAutoRun.enabled = true;
        goingToTarget = false;
        base.smooth = 5;//追上去的效果，越高越快，但不细致
        base.MaxDistance = .02f;//这个是表示接近时就停下来
        myMouseInOut(Center, 2.8f, 5.5f, 4f, new Vector2(-129f, 0), new Vector2(-7.0f, 20), 25f);//初始化一下
        int toPointLength = toPoint.Length;
        for (int i = 0; i < AtoB_Button.Length; i++)
        {
            string nameA = AtoB_Button[i].name;
            string nameB = BtoA_Button[i].name;
            AtoB_Button[i].SetOnClick(() => playToThis(nameA));
            BtoA_Button[i].SetOnClick(() => playToThat(nameB));
        }

        // base.isToB
    }
    IEnumerator my(Transform POV, Transform Middle, Transform NexttoPoint, bool isMouse, Vector2 thisV2)
    {
        if (isMouse)//返回有大问题，原因在前面记录点
        {
            inCarFloorOrChair = false;
            goingToTarget = true;
            myMouse.enabled = false;
            myMouseInOut(Center, 2.8f, 7f, 2.8f, thisV2, new Vector2(-7.0f, 20), 25);
            myT = Middle;
            yield return new WaitForSeconds(waitTimeInMiddle);
            myT = NexttoPoint;
            yield return new WaitForSeconds(waitTimeInMiddle);
            myMouse.enabled = true;
            myAutoRun.enabled = true;
            goingToTarget = false;
            //解决快速点击座椅后再点击其他按钮后摄像机不能正常控制bug
            inCarFloorOrChair = false;
            myMouseInOut(Center, 2.8f, 7f, 2.8f, thisV2, new Vector2(-7.0f, 20), 25);//初始化一下
        }
        else//进入车没有问题
        {
            goingToTarget = true;
            //fromPoint.position = myC.position;//记一下从来那里来的，返回时用,不要用等号，这样会等于cam相机，关闭后,就会自动使用formA
            //fromPoint.rotation = myC.rotation;
            myMouse.enabled = false;//先关了，再执行

            myT = Middle;
            yield return new WaitForSeconds(waitTimeInMiddle);
            myT = NexttoPoint;
            yield return new WaitForSeconds(waitTimeInMiddle);//再等一下，又可以用mouse
            myMouse.enabled = true;
            myMouseInOut(POV, .5f, 1.5f, .8f, thisV2, new Vector2(0, 45), 5);
            myAutoRun.enabled = false;
            goingToTarget = false;
            inCarFloorOrChair = true;
        }
    }
    private void myMouseInOut(Transform myPov, float minD, float maxD, float Dis, Vector2 thisXY, Vector2 angleBound, float damping)
    {
        myX = thisXY.x;
      
        if (myMouse != null)
        {
            myMouse.target = myPov; //mouse中心轴改变
            myMouse.minDistance = minD;
            myMouse.maxDistance = maxD;
            myMouse.distance = Dis;

            myMouse.x = thisXY.x;
            myMouse.y = thisXY.y;

            myMouse.yMinLimit = angleBound.x;
            myMouse.yMaxLimit = angleBound.y;
            myMouse.damping = damping;
        }

    }
    protected sealed override void AtoB(Transform myca, Transform mytr)//相机+目标点
    {
        base.AtoB(myca, mytr);
    }

    void LateUpdate()
    {

        if (goingToTarget)//当MouseFollowRotation不动时就进入
        {
            AtoB(myC, myT);
        }
        else
        {
            //if (!EventSystem.current.currentSelectedGameObject)
            //{
            //    myMouse.enabled = true;
            //}
            //else
            //{
            //    myMouse.enabled = false;
            //}
            if (inCarFloorOrChair)//这是不可用时，就是进入座椅与脚垫时
            {
                if (myMouse.x < myX - 30f)
                {
                    myMouse.x = myX - 30f;
                }
                if (myMouse.x > myX + 30f)
                {
                    myMouse.x = myX + 30f;
                }
            }
        }
    }
    public void playToThis(string name)//进入
    {
        int index = name == "CarSeat" ? 0 : 1;
        StartCoroutine(my(MousePOV[index], midPoint[index], toPoint[index], false, myVxy[index]));
    }
    public void playToThat(string name)//返回
    {
        StartCoroutine(my(MousePOV[1], midPoint[1], fromPoint, true, myVxy[1]));
    }
}
