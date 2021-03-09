using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPanel : MonoBehaviour {

	public GameObject retryButton;

 	void OnDisable(){
 		Destroy(retryButton);
 	}
}
