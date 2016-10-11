//*************************************************
//** 姓名 ：张振
//** 日期 ：2015年1月17日
//** 类作用： 摄像机路径点管理
//*************************************************
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathPointsMgr : MonoBehaviour
{
    private List<Vector3> listPoints = new List<Vector3>();      //路径座位点集合

    private List<Vector3> listInner = new List<Vector3>();      //车内座位点集合

    private int nMainIndex = 2;    //主驾驶索引
    private int nCurrent;       //当前座位索引
    void Awake()
    {
        Transform trans = transform;
        for (int i = 0; i < trans.childCount; i++)
        {
            listPoints.Add(trans.GetChild(i).position);
        }
        listInner = listPoints.GetRange(nMainIndex, listPoints.Count - nMainIndex);
        nCurrent = 0;
    }

    /// <summary>
    /// 取进入车路径
    /// </summary>
    public Vector3[] GetEnterCarPath()
    {
        return listPoints.GetRange(0, 3).ToArray();
    }

    List<Vector3> outListPath = new List<Vector3>();

    public Vector3[] GetOutCarPath()
    {
        nCurrent = 0;
        outListPath.Clear();
    
        //原构思用集合List的反序和取范围的接口来做处理，但发现不如直接集合add元素效率更高些，但这段代码很渣
        switch (nCurrent)
        {
            case 0:
                outListPath = listPoints.GetRange(0, 3);outListPath.Reverse();
                break;
            case 1:
                outListPath = listPoints.GetRange(0, 4);outListPath.Reverse();
                break;
            case 2:
                outListPath.Add(listPoints[4]); outListPath.Add(listPoints[2]); outListPath.Add(listPoints[1]); outListPath.Add(listPoints[0]);
                break;
            case 3:
                outListPath.Add(listPoints[5]); outListPath.Add(listPoints[4]); outListPath.Add(listPoints[2]); outListPath.Add(listPoints[1]); outListPath.Add(listPoints[0]);
                break;
            default:
                break;
        }
        return outListPath.ToArray();
    }

    /// <summary>
    /// 移动到下个座位
    /// </summary>
    /// <returns></returns>
    public Vector3[] GetNextSeat()
    {
        if (listInner.Count - 1 == nCurrent)
        {
            nCurrent = 0;
            return new Vector3[] { listInner[listInner.Count - 1], listInner[0] };
        }
        return listInner.GetRange(nCurrent++, 2).ToArray();
    }

}
