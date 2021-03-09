using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCollider : MonoBehaviour {

	public static LoseCollider instance;
	[SerializeField] GameObject loseParticles;
	[HideInInspector] public int ballTouches;
	Vector2 collisionPoint;
	GameObject shootRenderer;
	ShootSpotRenderer srr;
	Vector2 sr;
	bool dead;

	void Start () {
		if (instance == null){
			instance = this;
		}else if (instance !=this){
			Destroy(gameObject);
		}
		shootRenderer = GameObject.FindGameObjectWithTag("ShootSpot");
		srr = shootRenderer.GetComponent<ShootSpotRenderer>();
	}


	void OnCollisionEnter2D (Collision2D collider){
		if (collider.gameObject.tag == "Ball") {
			if (ballTouches < 1){
				ContactPoint2D contact = collider.contacts[0];
				collisionPoint = contact.point;
				sr.x = collisionPoint.x;
				srr.newPos.x = collisionPoint.x;
				shootRenderer.SetActive(true);
				ballTouches++;
				}
		}else if (collider.gameObject.tag == "Breakable" && !dead){
			dead = true;
			BallShooter.instance.dead = true;
			var losePos = collider.transform.position.x;
			loseParticles.SetActive(true);
			var newPos = new Vector2(losePos, 1f);
			loseParticles.transform.position = newPos;
			Invoke("CallDeath",3f);
		}else if (collider.gameObject.tag == "UnbreakableBricks"){
			Destroy(collider.gameObject);
		}
	}
	
	 public void SetPositionForShoot(){
	 	if (BallShooter.instance == null){
	 		Debug.LogWarning ("BallShooter Script is not assinged");
	 		return;
	 	}else if (BallShooter.instance != null){
	 		BallShooter.instance.basePos.x = collisionPoint.x;
	 	}
	}

	void CallDeath(){
		GameManager.instance.Death();
		BallShooter.instance.DeadOrNot();
		dead = false;
	}

	public void ParticleReset(){
		loseParticles.SetActive(false);
	}
}
