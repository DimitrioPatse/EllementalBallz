using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToShootInfo : MonoBehaviour {

	[SerializeField] int timesClickForDisapear = 1;
	int timesClicked;

	void Update () {
		if (Input.GetMouseButtonUp(0)){
			timesClicked ++;
			}
		if (timesClicked >= timesClickForDisapear){
			Destroy(gameObject);
		}
	}
}
