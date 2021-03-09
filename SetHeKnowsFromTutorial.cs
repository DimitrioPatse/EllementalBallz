using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHeKnowsFromTutorial : MonoBehaviour {


	// Use this for initialization
	void OnEnable () {
		SaveManager.instance.HeKnowsBool();
		}
}
