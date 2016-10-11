using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class TouchToEnter : MonoBehaviour
{
	public GameObject NoticeUI;
	public  Text NoticeMsg;
    private float myDistanceAB;
    private Vector2 DownPoint, UpPoint;
    public static bool canPress=true;
	public  void Update ()
	{
        if (Input.GetMouseButtonDown(0))
        {
            DownPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(0))
        {
            UpPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            myDistanceAB = Vector2.Distance(DownPoint, UpPoint);
            if (myDistanceAB< 30)
            {
                onClickFun();
            }
        }
        
	//&& SingleTapTarget()) // 这是重要点击后执行，再改Notice就行了
		
	}
    private void onClickFun()
    {

        if (canPress)
        { 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 7))
        {
				if (hit.collider.gameObject.name.Contains("a1"))
            {
                NoticeUI.SetActive(true);
                NoticeMsg.text = "详细查看 奥迪A1?";
            }
            else if (hit.collider.gameObject.name.Contains("a3"))
            {
                NoticeUI.SetActive(true);
                NoticeMsg.text = "详细查看 奥迪A3?";
            }
            else if (hit.collider.gameObject.name.Contains("a4"))
            {
                NoticeUI.SetActive(true);
                NoticeMsg.text = "详细查看 奥迪A4?";
            }
            else if (hit.collider.gameObject.name.Contains("a5"))
            {
                NoticeUI.SetActive(true);
                NoticeMsg.text = "详细查看 奥迪A5?";
            }
            else if (hit.collider.gameObject.name.Contains("a6"))
            {
                NoticeUI.SetActive(true);
                NoticeMsg.text = "详细查看 奥迪A6?";
            }
            else if (hit.collider.gameObject.name.Contains("a7"))
            {
                NoticeUI.SetActive(true);
                NoticeMsg.text = "详细查看 奥迪A7?";
            }
            else if (hit.collider.gameObject.name.Contains("a8"))
            {
                NoticeUI.SetActive(true);
                NoticeMsg.text = "详细查看 奥迪A8?";
            }
            else if (hit.collider.gameObject.name.Contains("q3"))
            {
                NoticeUI.SetActive(true);
                NoticeMsg.text = "详细查看 奥迪Q3?";
            }
            else if (hit.collider.gameObject.name.Contains("q5"))
            {
                NoticeUI.SetActive(true);
                NoticeMsg.text = "详细查看 奥迪Q5?";
            }
            else if (hit.collider.gameObject.name.Contains("q7"))
            {
                NoticeUI.SetActive(true);
                NoticeMsg.text = "详细查看 奥迪Q7?";
            }
        }
        }
    }
	Vector2 mTouchStartPos;
	bool mTouchMoved = false;
	float mTimeElapsed = 0.0f;
	bool mTapped = false;
	float mTimeElapsedSinceTap = 0.0f;
	bool bTappedObj = false;
	string TappedObjName;
	GameObject TappedObj;

	public  bool SingleTapTarget ( )
	{
		if (Input.touchCount > 0) {
			Touch touch = Input.touches [0];
			if (touch.phase == TouchPhase.Began) {
                
				mTouchStartPos = touch.position;
				mTouchMoved = false;
				mTimeElapsed = 0.0f;
				Ray ray = Camera.main.ScreenPointToRay (Input.GetTouch (0).position);
				RaycastHit hitInfo;
				if (Physics.Raycast (ray, out hitInfo)) {
					TappedObjName = hitInfo.collider.gameObject.name;
					TappedObj = hitInfo.collider.gameObject;
                    
					bTappedObj = true;
                    
				}
			} else {
				mTimeElapsed += Time.deltaTime;
			}
            
			if (touch.phase == TouchPhase.Moved) {
				if (Vector2.Distance (mTouchStartPos, touch.position) > 40) {
					mTouchMoved = true;
				}
			} else if (touch.phase == TouchPhase.Ended) {
				if (!mTouchMoved && mTimeElapsed < 0.6 && bTappedObj) {
					if (mTapped) {
                        
						mTapped = false;
						bTappedObj = false; 

						return false;
					} else {
						mTapped = true;
						mTimeElapsedSinceTap = 0.0f;
					}
				}
			}
		}
		if (mTapped) {
			if (mTimeElapsedSinceTap >= 0.5f && bTappedObj) {
				mTapped = false;
				bTappedObj = false;
				return true;
				
			} else {
				mTimeElapsedSinceTap += Time.deltaTime;
			}
		}
		return false;
	}


	

}
