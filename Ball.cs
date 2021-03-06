using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	[SerializeField] float speed;
	[SerializeField] float timeForPositionChange = 10f;
	public Sprite[] sprites;
	Rigidbody2D myRigy;
	float timeAlive;

	SpriteRenderer myRenderer;
	int savedBallID;
	public bool specialBall;

	GameObject shootSpot;
	GameObject ballShooter;
	[HideInInspector] public bool isMooving = false;

	// Use this for initialization
	void Start () {
		shootSpot = GameObject.FindGameObjectWithTag("ShootSpot");
		ballShooter = BallShooter.instance.gameObject;
		myRenderer = GetComponent<SpriteRenderer>();
		savedBallID = SaveManager.instance.f3;	
		myRigy = GetComponent<Rigidbody2D>();
		if (!specialBall){
		myRenderer.sprite = sprites[savedBallID];
		}
		ResetPos();
	}

	void FixedUpdate(){
		if (isMooving){
			timeAlive += Time.deltaTime;
			myRigy.velocity = speed * (myRigy.velocity.normalized);
		}
		if (timeAlive >= timeForPositionChange){
			myRigy.AddRelativeForce(new Vector2(Random.Range(1f,5f),Random.Range(-5f,-15f)),ForceMode2D.Impulse);
			timeAlive = 0;
		}
	}
	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Breakable"){
			timeAlive = 0f;
		}
		if (coll.gameObject.tag == "LoseCollider"){
			isMooving = false;
			myRigy.velocity = Vector3.zero;
			Invoke("ResetPos",0.05f);
		}
	}

	void ResetPos (){		
		transform.position = shootSpot.transform.position;
		transform.parent = ballShooter.transform;
		timeAlive = 0;
	}
}
