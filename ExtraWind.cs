using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraWind : MonoBehaviour {

	private WindController wind;
	private bool activatedWind;
	public AudioClip sound;

	void Start(){
		wind = GameObject.FindObjectOfType<WindController>();
	}
	void Update () {
		transform.Translate(Vector2.down * Time.deltaTime, Space.World);
	}

	void OnTriggerEnter2D (Collider2D collision){
		if(collision.gameObject.tag == "Ball"){
			activatedWind = wind.activateTime;
			Ball ball = collision.gameObject.GetComponent<Ball>();
			var isMoovin = ball.isMooving;
			if (isMoovin){
				if(!activatedWind){
					AudioSource.PlayClipAtPoint(sound, transform.position);
					wind.ActivateWind();
					Destroy(gameObject);
				}else if (activatedWind){
					wind.timeEnabled -= wind.windEnableTime;
					Destroy(gameObject);
				}
			}else{
				Destroy(gameObject);
			}
		}
	}
}
