using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiPanel : MonoBehaviour {


	void OnEnable(){
			BallShooter.instance.DeadOrNot();
	}
	void OnDisable(){
			BallShooter.instance.DeadOrNot();
	}
}

