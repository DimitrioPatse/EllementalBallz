using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {

	static MusicPlayer instance = null;

	public AudioClip[] levelMusicChangeArray;
	private AudioSource myAudio;
	AudioListener listener;

	// Use this for initialization
	void Awake(){
		if(instance !=null){
			Destroy (gameObject);
			GameObject.DontDestroyOnLoad (gameObject);
		}else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
		}

		DontDestroyOnLoad (gameObject);
		myAudio = gameObject.GetComponent<AudioSource>();
		AudioListener.volume = PlayerPrefsManager.GetMasterVolume();
	}

	private void OnEnable() { SceneManager.sceneLoaded += OnSceneLoaded; }

    private void OnDisable() { SceneManager.sceneLoaded -= OnSceneLoaded; }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode){
        
		AudioClip thisLevelMusic = levelMusicChangeArray[scene.buildIndex];
		
		if (thisLevelMusic){
			myAudio.Stop ();
			myAudio.clip = thisLevelMusic;
			myAudio.loop = false;
			myAudio.loop = true;
			myAudio.Play ();
		}	
	}
	public void SetVolume(float volume){
		myAudio.volume = volume;
		
	}
}
