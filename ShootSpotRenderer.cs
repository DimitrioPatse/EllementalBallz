using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSpotRenderer : MonoBehaviour {

	public Sprite[] sprites;
	SpriteRenderer myRenderer;
	int savedBallID;
	[HideInInspector] public Vector2 newPos = new Vector2(3.5f , 1.2f);

	// Use this for initialization
	void Start () {
		myRenderer = GetComponent<SpriteRenderer>();
		savedBallID = SaveManager.instance.f3;
		myRenderer.sprite = sprites[savedBallID];
	}
	void Update(){
		transform.position = new Vector2 (newPos.x , 1.2f);
	}
}
