using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallsShowCount : MonoBehaviour {

	private Text myText;


	// Use this for initialization
	void Start () {
	myText = GetComponent<Text>();
		
	}
	
	// Update is called once per frame
	public void BallsToShowUpdate () {
		myText.text = BallShooter.instance.balls.ToString();
	}
}
