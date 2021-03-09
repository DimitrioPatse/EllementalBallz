using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {

	
	void OnTriggerEnter2D(Collider2D collider){
		if(collider.gameObject.tag == "Breakable"){
			Brick brick = collider.GetComponent<Brick>();
			brick.BrickFire();
			Destroy(collider.gameObject);
		}
	}

}