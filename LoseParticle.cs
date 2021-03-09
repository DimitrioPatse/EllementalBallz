using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseParticle : MonoBehaviour {

	[SerializeField] float timeForHide;
	bool count;
	float timeSet;

	void Start(){
		timeSet = timeForHide;
	}

	void Update(){
		timeForHide -= Time.smoothDeltaTime;
		if (timeForHide <= 0){
			gameObject.SetActive(false);			
		}
	}

	void OnDisable(){
		timeForHide = timeSet;
	}

}
