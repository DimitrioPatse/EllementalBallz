using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameLocker : MonoBehaviour {

	bool heKnows;


	void Update () {
		heKnows = SaveManager.instance.f5;
		if (heKnows == true){
			Destroy(gameObject);
		}
	}
}
