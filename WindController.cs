using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindController : MonoBehaviour {

	public GameObject wind;
	public float windEnableTime;

	[HideInInspector]
	public float timeEnabled;
	[HideInInspector]
	public bool activateTime;


	// Use this for initialization
	void Start () {
		wind.SetActive(false);
	}

	void Update(){
		if (activateTime){
			timeEnabled += Time.deltaTime;
		}
		if (timeEnabled >= windEnableTime){
			DeactivateWind();
		}
	}

	public void ActivateWind(){
		wind.SetActive(true);
		activateTime = true;
	}
	void DeactivateWind(){
		wind.SetActive(false);
		activateTime = false;
		timeEnabled = 0;
	}
}
