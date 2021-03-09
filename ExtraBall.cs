using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraBall : MonoBehaviour {

	[Tooltip("Poses mpales dinei extra me 8etiko ari8mo, Poses mpales soy xanei me arnitik;o ari8mo")] 
	public int ballDoValue = 1;
	public AudioClip sound;
	private bool hit;

	void FixedUpdate(){
		transform.Rotate(new Vector3(0,0,100) * Time.deltaTime);
	}

	void OnCollisionEnter2D (Collision2D collision){
		if(collision.gameObject.tag == "Ball"){
			Ball ball = collision.gameObject.GetComponent<Ball>();
			var isMoovin = ball.isMooving;
			if (!hit && isMoovin ){
				hit = true;
				Destroy(gameObject);
				AudioSource.PlayClipAtPoint(sound, transform.position);
				GameManager.instance.newBalls += ballDoValue;
			}
		}else{
			if(!hit){
				hit = true;
				Destroy(gameObject);
				AudioSource.PlayClipAtPoint(sound, transform.position);
				BallShooter.instance.SetExtraBall();
				UiManager.instance.MakeUpdates();
			}
		}
	}
}
