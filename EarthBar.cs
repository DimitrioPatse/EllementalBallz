using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthBar : MonoBehaviour {

	private float timeAlive;
	public float timeToLive;
	public float speed;
	public float minX;
	public float maxX;
	private bool moveRight = true;
	private bool moveLeft;

	// Use this for initialization
	void Start () {
		timeAlive = 0;
	}
	
	// Update is called once per frame
	void Update () {
		LifeTime();
		CheckMovement();
		Movement();
	}

	void LifeTime(){
		timeAlive += Time.deltaTime;
		if (timeAlive >= timeToLive){
			gameObject.SetActive(false);
		}
	}
	void CheckMovement(){
		if (transform.position.x >= maxX){
			moveRight = false;
			moveLeft = true;
		}else if (transform.position.x <= minX){
			moveRight = true;
			moveLeft = false;
		}
	}

	void Movement(){
		if (moveRight){
			transform.Translate(Vector2.right * Time.deltaTime * speed);
		}else if(moveLeft){
			transform.Translate(Vector2.left * Time.deltaTime * speed);
		}
	}

	void OnEnable(){
		timeAlive = 0;
	}
}
