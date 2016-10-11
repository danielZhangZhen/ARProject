using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetMainHelpUISize : MonoBehaviour {


	private GridLayoutGroup scrollGrid;
	private GridLayoutGroup toggleGrid;
	private ScrollRect scrollRect;


	void Awake(){
		scrollGrid = transform.Find ("HelpPanel/HelpContent/Scroll View/Grid").GetComponent<GridLayoutGroup> ();
		scrollGrid.cellSize = new Vector2 (Screen.width,Screen.height);
		scrollGrid.transform.GetComponent<RectTransform> ().sizeDelta =
			new Vector2(2 * Screen.width,scrollGrid.transform.GetComponent<RectTransform> ().sizeDelta.y);

		toggleGrid = transform.Find ("HelpPanel/HelpContent/Toggle").GetComponent<GridLayoutGroup> ();

		toggleGrid.cellSize = new Vector2 (scrollGrid.cellSize.x / 200f, scrollGrid.cellSize.x / 200f);


		scrollRect = transform.Find ("HelpPanel/HelpContent/Scroll View").GetComponent<ScrollRect> ();

	}


	// Use this for initialization
	void Start () {
		scrollRect.horizontalNormalizedPosition = 0.001f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
