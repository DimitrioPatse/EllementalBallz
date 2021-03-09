using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HexRotation : MonoBehaviour {

	public RectTransform myRect;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0,0,-20 * Time.fixedDeltaTime);
		myRect.Rotate(0,0,20 * Time.fixedDeltaTime);
	}
}
