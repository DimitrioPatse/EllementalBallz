using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraEarthBar : MonoBehaviour {

	private EarthBarController earthBarController;
	private EarthBar bar;
	public AudioClip sound;

	void Start(){
		earthBarController = GameObject.FindObjectOfType<EarthBarController>();
	}
	void Update () {
		transform.Translate(Vector2.down * Time.deltaTime, Space.World);
	}

	void OnTriggerEnter2D (Collider2D collision){
		if(collision.gameObject.tag == "Ball"){
			bar = GameObject.FindObjectOfType<EarthBar>();
			Ball ball = collision.gameObject.GetComponent<Ball>();
			var isMoovin = ball.isMooving;
			if (isMoovin){
				if (bar == null){
					AudioSource.PlayClipAtPoint(sound, transform.position);
					earthBarController.EarthBarActivate();
					Destroy(gameObject);
				}else if(bar.gameObject.activeInHierarchy){
					bar.timeToLive += 7;
					Destroy(gameObject);
				}
			}else{
				Destroy(gameObject);
			}
		}
	}
}
