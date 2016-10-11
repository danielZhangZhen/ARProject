//*************************************************
//** 姓名 ：张振
//** 日期 ：2015年12月28日
//** 类作用： 扩展方法公用方法
//*************************************************
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public static class ExtendTool
{
    /// <summary>
    /// 设置点击事件 音效默认开启
    /// </summary>
    /// <param name="btn"></param>
    /// <param name="call"></param>
    /// <param name="isClickSound"></param>
    public static void SetOnClick(this Button btn, UnityAction call, bool isClickSound = true)
    {
        if (btn != null)
        {
            btn.onClick.AddListener(call);
            if (isClickSound)
            {
                //点击音效
            }
        }
    }
    /// <summary>
    /// 添加并且实例化对象
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="prefab"></param>
    /// <returns></returns>
    public static GameObject AddChild(Transform parent, GameObject prefab)
    {
        GameObject go = GameObject.Instantiate(prefab) as GameObject;
        if (go != null && parent != null)
        {
            Transform t = go.transform;
            t.SetParent(parent, false);
            go.layer = prefab.gameObject.layer;
        }
        return go;
    }

    static public T GeCompontentForMiss<T>(this GameObject go) where T : Component
    {
        T comp = go.GetComponent<T>();
        if (comp == null)
            comp = go.AddComponent<T>();
        return comp;
    }

    /// <summary>
    /// 删除物体
    /// </summary>
    /// <param name="obj"></param>
    public static void DestroyFunc(UnityEngine.Object obj)
    {
        if (obj != null)
        {
            if (Application.isPlaying)
            {
                if (obj is GameObject)
                {
                    GameObject go = obj as GameObject;
                    go.transform.SetParent(null, false);
                }
                GameObject.Destroy(obj);
            }
            else
                GameObject.DestroyImmediate(obj);
        }
    }
}
