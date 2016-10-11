//**********************************************************
//** 姓名 ：林宇敬
//** 日期 ：2016年03月22日
//** 类作用： 拖动后UI窗口内的Grid元素位置自适应控制；   
//**          
//**********************************************************
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollItemAdjust : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    public float moveSpeed = 4f;

    private ScrollRect scrollRect;
    private float startPositoin = 0f; //用于记录鼠标点下时滚动条位置
    private float targetPosition = 0f;
    private bool flag = false;
    private int childCount = 0; //item数量

    private bool isNeedGetChild = true;

    private int index = 0;//用于计数

    //是否启动关闭UI判断
    private bool isCheckClose = false;

    void Awake()
    {
        scrollRect = transform.GetComponent<ScrollRect>();
    }

    void Update()
    {
        if (flag)
        {
            scrollRect.horizontalNormalizedPosition = Mathf.Lerp(scrollRect.horizontalNormalizedPosition,
                targetPosition, Time.deltaTime * moveSpeed);
        }
        if (isCheckClose)
        {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                scrollRect.transform.parent.gameObject.SetActive(false);
            }
#elif UNITY_IPHONE || UNITY_ANDROID
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began 
                && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) 
            {
                scrollRect.transform.parent.gameObject.SetActive(false);
            }
#endif
        }
    }
    //鼠标开始拖动事件监听
    public void OnBeginDrag(PointerEventData eventData)
    {
        startPositoin = scrollRect.horizontalNormalizedPosition;
        flag = false;
    }
    //鼠标结束拖动事件监听
    public void OnEndDrag(PointerEventData eventData)
    {
        flag = true;
        targetPosition = GetTargetPosition();
        //Debug.Log(targetPosition);
    }
    //获得孙子物体的个数
    void GetChildCount()
    {
      
        childCount = transform.FindChild("Grid").childCount;
        //Debug.Log(childCount);
    }
    //计算自适应位置
    float GetTargetPosition()
    {
        if (isNeedGetChild)
        {
            GetChildCount();
            isNeedGetChild = false;
        }
        
        float posx = scrollRect.horizontalNormalizedPosition;//鼠标拖动到的位置
        /*float offset = posx;
        float temp2 = 1 / ((float)childCount - 1);//除去已经显示的格子其余的格子每个格子所占的长度百分比

        for (int i = 1; i < childCount; i++)
        {
            float offsetTemp = Mathf.Abs(posx - i * temp2);//用 鼠标的位置 减去 1倍、2倍、3倍数值
            if (offsetTemp < offset) //通过减来的值与 鼠标的位置比较，
            {
                index = i;
                offset = offsetTemp;
            }
        }*/
        if (posx - startPositoin > 0.01) //当拖动大于0.06时
        {
            if(index < childCount-1)
                index++;
        }
        else if (posx - startPositoin < -0.01)
        {
            if (index >= 1)
                index--;
        }
        SetToggleIsOn(index);
        //Debug.Log(index);
        return (float)index * (1 / ((float)childCount - 1)); //需要适配到的位置
    }

    void SetToggleIsOn(int index)
    {
        scrollRect.transform.parent.FindChild("Toggle/" + index).GetComponent<Toggle>().isOn = true;
        transform.parent.GetComponent< UpdateText>().UpdateTXT(index);
    }

    void OnEnable()
    {
        isCheckClose = true;
    }
    void OnDisable()
    {
        isCheckClose = false;
    }
}
