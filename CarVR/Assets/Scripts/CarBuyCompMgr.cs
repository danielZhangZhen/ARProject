//*************************************************
//** 姓名 ：张振
//** 日期 ：2015年1月13日
//** 类作用： 汽车零件购买管理类
//*************************************************
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarBuyCompMgr
{
    private string strCarName = "Audi_A5";              //车名
    private string strCarComponentName = string.Empty;     //车部件名
    private int componentIndex;    //具体选取部件哪一个索引

    private List<string> listItemIds = new List<string>();

    private static CarBuyCompMgr mInstance;
    public static CarBuyCompMgr Ins
    {
        get
        {
            if (mInstance == null)
                mInstance = new CarBuyCompMgr();
            return mInstance;
        }
    }

    public void Init()
    {
        listItemIds = new List<string>();
    }

    /// <summary>
    /// 加载某车的场景时调用赋值
    /// </summary>
    /// <param name="carName"></param>
    public void SetCarName(string carName)
    {
		Init ();
        strCarName = carName;
        strCarComponentName = string.Empty;
        componentIndex = 0;
    }

    /// <summary>
    /// 设置车零件名称
    /// </summary>
    /// <param name="compName"></param>
    public void SetCarItemName(string compName)
    {
        strCarComponentName = compName;
        componentIndex = 0;
    }
    /// <summary>
    /// 设置车选取的零件索引
    /// </summary>
    /// <param name="compIndex"></param>
    public void SetCompIndex(int compIndex)
    {
        componentIndex = compIndex;
    }

    /// <summary>
    /// 获取物品索引ID
    /// </summary>
    /// <returns></returns>

    public void AddItem()
    {
        if (IsSelectItem())
            listItemIds.Add(GetItemId());
        else
        {
            //请选择购买部件的提示框
        }
    }

    private string GetItemId()
    {
        return  strCarName + "_" + strCarComponentName + "_" + componentIndex;
    }

    /// <summary>
    /// 获取物品清单列表
    /// </summary>
    /// <returns></returns>
    public List<string> GetList()
    {
        //没有选中任何部件，直接点击购买传入车名即可
        if (listItemIds.Count == 0)
        {
            List<string> list = new List<string>();
            if (IsSelectItem())
            {
                list.Add(GetItemId());
                return list;
            }
            list.Add(strCarName);
            return list;
        }
        return listItemIds;
    }

    //true为选择了部件,默认选择部件样式的第一个
    private bool IsSelectItem()
    {
        if (strCarComponentName != string.Empty)
            return true;
        return false;
    }
}
