using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsController : MonoBehaviour {
	
	[SerializeField] GameObject invertYes, invertNo;

	Slider musicSlider;
	MusicPlayer musicManager;
	

	// Use this for initialization
	void Start () {
		musicSlider = GetComponentInChildren<Slider>();
		musicManager = GameObject.FindObjectOfType<MusicPlayer>();
		musicSlider.value = PlayerPrefsManager.GetMasterVolume();
		InvertButtonFx ();

	}
	
	// Update is called once per frame
	void Update () {
		AudioListener.volume = musicSlider.value;

	}
	
	public void SaveAndExit(){
		PlayerPrefsManager.SetMasterVolume (musicSlider.value);
		AudioListener.volume = musicSlider.value;
		gameObject.SetActive(false);
	}

	public void SaveAndHome(){
		PlayerPrefsManager.SetMasterVolume (musicSlider.value);
		AudioListener.volume = musicSlider.value;
		LevelManager.instance.LoadLevel("01_Start");

	}

	public void AimInvert(){
		PlayerPrefsManager.InvertAim();
		InvertButtonFx ();
	}

	void InvertButtonFx (){
		float f = PlayerPrefsManager.AimPosition();
		if (f == 1) {
			invertYes.SetActive (true);
			invertNo.SetActive (false);
		}
		else {
			invertYes.SetActive (false);
			invertNo.SetActive (true);
		}
	}
	public void Achievements(){
		GooglePlay.ShowAchievementsUI();
	}
	public void Leaderboard(){
		GooglePlay.ShowLeaderboards();
	}
}
