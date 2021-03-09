using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Brick : MonoBehaviour {

	public int timesToHit;
	public bool isOnChallenge;
	private int timesHit;
	private bool isBreakable;
	public int hitsLeft;
	private int randomDoubling;

	//Effects
	public AudioClip hitClip;	
	public GameObject smoke;
	public GameObject firePrefab;
	//private GameObject smokePuff;

	//PointSystem
	private Text mytext;

	// Use this for initialization
	void Start () {
		mytext = GetComponentInChildren<Text>();
		isBreakable = (this.tag == "Breakable");
		timesHit = 0;

		if (timesToHit == 0 ) {
			timesToHit = GameManager.instance.level;
			}
		if(!isOnChallenge){
		randomDoubling = Random.Range(1,10);
		}
		if (randomDoubling >= 7 && randomDoubling <= 9){
			timesToHit = timesToHit * 2;
		}else if(randomDoubling == 10){
			timesToHit = timesToHit * 3;
		}
	}	

	// Update is called once per frame
	void Update () {
		hitsLeft = timesToHit - timesHit;
		mytext.text = hitsLeft.ToString();
				
	}
	void OnCollisionEnter2D (Collision2D col){
		AudioSource.PlayClipAtPoint (hitClip, gameObject.transform.position,0.45f);
		if (isBreakable && col.gameObject.tag == "Ball"){
			HandleHits();
		} 
	}

	void HandleHits(){
		timesHit++;
		if (timesHit >= timesToHit){
		PuffSmoke();
		Destroy (gameObject);
		}
	}

	void PuffSmoke(){
		GameObject smokePuff = Instantiate (smoke,this.transform.position,Quaternion.identity)as GameObject;
		ParticleSystem ps = smokePuff.GetComponent<ParticleSystem>();
		ParticleSystem.MainModule psmain = ps.main;
		psmain.startColor = gameObject.GetComponent<SpriteRenderer>().material.color;
		Destroy(smokePuff,1f);
		}

	public void BrickFire(){
		GameObject firePuff = Instantiate (firePrefab,this.transform.position,Quaternion.identity)as GameObject;
		Destroy(firePuff,1f);
		}
}
