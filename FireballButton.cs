using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireballButton : MonoBehaviour {
	 int fireballs;
	 Button myButton;
	 Text myText;
	 bool hasStarted;

	// Use this for initialization
	void Start () {
	myButton = GetComponent<Button>();
	myText = GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		fireballs = BallShooter.instance.fireballs;
		hasStarted = BallShooter.instance.hasStarted;
		if (fireballs >=2){
			myText.gameObject.SetActive(true);
			myText.text = fireballs.ToString();
		}else{
			myText.gameObject.SetActive(false);
		}
		if (fireballs >= 1 && !hasStarted){
			myButton.interactable = true;
		}else if(fireballs <= 0 || hasStarted){
			myButton.interactable = false;
			myText.gameObject.SetActive(false);

		}
	}
}
