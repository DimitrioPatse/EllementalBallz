using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour {

	//Sprite
	public Sprite[]  sprites;
	private SpriteRenderer myRenderer;
	private int savedID;

	//Color
	public float colorValue;
	private float time;
	public Color32 Color1;
	public Color32 Color2;
	public Color32 Color3;
	public Color32 Color4;

	void Start(){
		myRenderer = GetComponent<SpriteRenderer>();
		savedID = SaveManager.instance.f4;	
		myRenderer.sprite = sprites[savedID];
	}

	void Update () {
		ColorLerp();
		
	}
	void ColorLerp(){
		time ++;
		colorValue = Mathf.Sin (time * 0.0001f);
		if (colorValue < 0.20f){
			GetComponent<SpriteRenderer>().material.color = Color.Lerp (Color1, Color2, colorValue / 0.20f);
		}else if (colorValue < 0.66f){
			GetComponent<SpriteRenderer>().material.color = Color.Lerp (Color2, Color3, (colorValue - 0.20f) / 0.20f);
		}else{
			GetComponent<SpriteRenderer>().material.color = Color.Lerp (Color3, Color4, (colorValue - 0.66f) / 0.66f);
		}
	}

}
