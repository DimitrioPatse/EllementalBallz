using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBall : MonoBehaviour {

	
	void OnCollisionEnter2D(Collision2D collider){
		if(collider.gameObject.tag == "Breakable"){
			Destroy(collider.gameObject,0.06f);
		}
	}

}