using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupDestroier : MonoBehaviour {


	void Update () {
		if (transform.childCount <=0)
			Destroy(gameObject);
		
	}
}
