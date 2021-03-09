using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoStartPanel : MonoBehaviour {

	void Start(){
		Invoke("NowStart",0.3f);
		}

	void NowStart(){
		Time.timeScale = 0;
		BallShooter.instance.DeadOrNot();
		Debug.Log("Panel Activated : " + gameObject.name);
	}

	void OnDisable(){
		GameManager.instance.fastTimer = false;
		Time.timeScale = 1;
		BallShooter.instance.DeadOrNot();
		Destroy(gameObject);
	}
}
