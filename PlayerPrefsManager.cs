using UnityEngine;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour {

	const string MASTER_VOLUME_KEY ="master_volume";
	const string AIM_POSITION ="aim_position";

	public static void SetMasterVolume (float volume){
		if (volume >= 0f && volume <= 1f){	
			PlayerPrefs.SetFloat (MASTER_VOLUME_KEY, volume);
		}else{
			Debug.LogError ("Master volume out of range");
		}
	}
	
	public static float GetMasterVolume(){
		return PlayerPrefs.GetFloat (MASTER_VOLUME_KEY);
	}

	public static void InvertAim(){
		float aim = PlayerPrefs.GetFloat(AIM_POSITION);
		if (aim == 0)
			PlayerPrefs.SetFloat(AIM_POSITION, 1f);
		else 
			PlayerPrefs.SetFloat(AIM_POSITION, 0f);	
	}

	public static float AimPosition(){
		return PlayerPrefs.GetFloat(AIM_POSITION);
	}
}
