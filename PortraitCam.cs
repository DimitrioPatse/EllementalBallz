using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortraitCam : MonoBehaviour {


	void Awake () {
		Screen.SetResolution(480,854,true);
		QualitySettings.vSyncCount = 0; // Vsync must be disabled
		Application.targetFrameRate = 60;
		float width = Screen.width;
		float heigh = Screen.height;
		float ar = Mathf.RoundToInt((width/heigh)*100f)/100f;
		if(ar == 0.75f){
			ar = 0.65f;
		}
		gameObject.GetComponent<Camera>().aspect = ar;
	}

	void Start(){
	Time.timeScale = 1;
	}
}
