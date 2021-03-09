using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraIceBall : MonoBehaviour {

	[Tooltip("Poses mpales dinei extra me 8etiko ari8mo, Poses mpales soy xanei me arnitik;o ari8mo")] 
	public int ballDoValue = 1;
	public AudioClip sound;

	void Update(){
		transform.Translate(Vector2.down * Time.deltaTime, Space.World);
	}

	void OnTriggerEnter2D (Collider2D collision){
		if (collision.gameObject.tag == "Ball"){
			Ball ball = collision.gameObject.GetComponent<Ball>();
			var isMoovin = ball.isMooving;
			if (isMoovin){
				BallShooter.instance.SetIceballs(ballDoValue);
				AudioSource.PlayClipAtPoint(sound, transform.position);
				Destroy(gameObject);
			}
		}
	}
}