using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour {

	void OnCollisionEnter2D (Collision2D collider){
		if (collider.gameObject.tag == "Breakable"){
		Destroy(collider.gameObject);
		}
	}
}
