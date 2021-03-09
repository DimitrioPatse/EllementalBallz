using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IceballButton : MonoBehaviour {
	 private int iceballs;
	 private Button myButton;
	 private Text myText;
	 bool hasStarted;

	// Use this for initialization
	void Start () {
	myButton = GetComponent<Button>();
	myText = GetComponentInChildren<Text>();

	}	
	// Update is called once per frame
	void Update () {
		hasStarted = BallShooter.instance.hasStarted;
		iceballs = BallShooter.instance.iceballs;
		if (iceballs >=2){
			myText.gameObject.SetActive(true);
			myText.text = iceballs.ToString();
		}else{
			myText.gameObject.SetActive(false);
		}
		if (iceballs >= 1 && !hasStarted){
			myButton.interactable = true;
		}else if(iceballs <= 0 || hasStarted){
			myButton.interactable = false;
			myText.gameObject.SetActive(false);
		}
	}
}