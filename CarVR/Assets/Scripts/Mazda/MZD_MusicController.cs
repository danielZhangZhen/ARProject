using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MZD_MusicController : MonoBehaviour {

	private Button btnPlay;
	private Button btnStop;
	private Button btnMusic;

	void Awake(){
		btnPlay = transform.Find ("MusicPlay").GetComponent<Button> ();
		btnStop = transform.Find ("MusicStop").GetComponent<Button> ();
		btnMusic = GetComponent<Button> ();
		btnPlay.onClick.AddListener (delegate {
			IsOpenSound(false);
			PlayerPrefs.SetString ("isPlayMusic","false");
		});
		btnStop.onClick.AddListener (delegate {
			IsOpenSound(true);
			PlayerPrefs.SetString ("isPlayMusic","true");
		});
		btnMusic.onClick.AddListener (delegate{
			if(btnPlay.gameObject.activeSelf){
				IsOpenSound(false);
				PlayerPrefs.SetString ("isPlayMusic","false");
			}else{
				IsOpenSound(true);
				PlayerPrefs.SetString ("isPlayMusic","true");
			}
		});
	}

	void Start(){
		if(string.Compare(PlayerPrefs.GetString ("isPlayMusic") ,"true") == 0){
			IsOpenSound (true);
		} else {
			IsOpenSound (false);
		}
	}

	private void IsOpenSound(bool play){
		if (play) {
			btnStop.gameObject.SetActive (false);
			btnPlay.gameObject.SetActive (true);
			BackController.Instance.GetComponent<AudioSource> ().UnPause ();
		} else {
			btnStop.gameObject.SetActive (true);
			btnPlay.gameObject.SetActive (false);
			BackController.Instance.GetComponent<AudioSource> ().Pause ();
		}
	}



	void OnDestroy(){
		btnPlay.onClick.RemoveListener (delegate {
			IsOpenSound(false);
		});
		btnStop.onClick.RemoveListener (delegate {
			IsOpenSound(true);
		});
	}
}
