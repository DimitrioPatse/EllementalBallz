using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBrick : MonoBehaviour {

	private StoneBrick[] bricks;
	// Use this for initialization
	void Start () {
		bricks = GameObject.FindObjectsOfType<StoneBrick>();
		if (bricks.Length >= 4)
			Destroy(gameObject);
	}
}
