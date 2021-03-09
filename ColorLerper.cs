using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLerper : MonoBehaviour {

	
	private float colorValue = 1f;
	public Color32 Color1;
	public Color32 Color2;
	public Color32 Color3;
	public Color32 Color4;

	void Update () {
		int hits = GetComponent<Brick>().hitsLeft;

		colorValue = Mathf.Sin (hits * 0.01f);
		if (colorValue < 0.20f){
			GetComponent<SpriteRenderer>().material.color = Color.Lerp (Color1, Color2, colorValue / 0.20f);
		}else if (colorValue < 0.66f){
			GetComponent<SpriteRenderer>().material.color = Color.Lerp (Color2, Color3, (colorValue - 0.20f) / 0.20f);
		}else{
			GetComponent<SpriteRenderer>().material.color = Color.Lerp (Color3, Color4, (colorValue - 0.66f) / 0.66f);
		}
		
	}

}
