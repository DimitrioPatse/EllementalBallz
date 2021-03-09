using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour {

	public GameObject hand;
	public float timeForHelp;

	private float timePassed;
	public int clicksToDestroy;
	private int clicks;


	// Update is called once per frame
	void Update () {
		timePassed += Time.deltaTime;
		if(timePassed >= timeForHelp){
			hand.SetActive(true);
		}
		if (Input.GetMouseButtonDown(0)){
			clicks++;
		}
		if (clicks >= clicksToDestroy){
			Destroy(gameObject);
		}
	}
}
