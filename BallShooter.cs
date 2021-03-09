using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallShooter : MonoBehaviour {

	public static BallShooter instance = null;

	[Header("Ball Sellection")]
	public int balls = 1;
	public int fireballs = 0;
	public int iceballs = 0;
	[SerializeField] GameObject ballPrefab;
	[SerializeField] GameObject fireBallPrefab;
	[SerializeField] GameObject iceBallPrefab;
	[HideInInspector] public bool fireBallOn = false;
	[HideInInspector] public bool iceBallOn = false;
	[HideInInspector] public List<GameObject> stockBalls;
	ExtraBall[] extraBalls;
	GameObject fireBall;
	GameObject iceBall;
	bool ballOn = true;
	[HideInInspector] public bool dead = false;
	AudioSource myAudio;

	[Header("Aiming")]
	public Vector2 basePos;
	public float aimOffset = 270;
	[HideInInspector] public bool aimWait = false;
	Vector2 mouseStartPos;
	Transform firePoint;
	LineRenderer lineRenderer;
	int ballLayerMask;
	float timeToFire;
	float aimY;
	bool aimCorrect = false; 

	[Header("Shooting")]
	[SerializeField] float firerate;
	[SerializeField] float forceForShooting;
	public GameObject shootSpot;
	[SerializeField] GameObject activeBallPool;
	[SerializeField] GameObject inactiveBallPool;
	[HideInInspector] public bool hasStarted = false;
	Vector3 hitPos;
	float aimRotate;
	AudioSource fireAudio;

	// Use this for initialization
	void Awake () {
		if (instance == null){
			instance = this;
		}else if (instance !=this){
			Destroy(gameObject);
		}
	}
	void Start ()  {
		stockBalls = new List<GameObject>();
		for (int i = 0; i < balls; i++) {
			stockBalls.Add((GameObject)Instantiate(ballPrefab,Vector2.zero,Quaternion.identity,transform));
		}
		fireBall = Instantiate(fireBallPrefab,Vector2.zero,Quaternion.identity,inactiveBallPool.transform) ;
		iceBall = Instantiate(iceBallPrefab,Vector2.zero,Quaternion.identity,inactiveBallPool.transform) ;
		myAudio = gameObject.GetComponent<AudioSource>();
		lineRenderer = GetComponent<LineRenderer>();

		SetBalls();
		fireAudio = fireBall.GetComponent<AudioSource>();
		SetFrames();
	}
	
	// Update is called once per frame
	void Update () {
		CheckMouseInput ();
		CheckForBalls();
		MousePosAim();
	}

	void CheckMouseInput (){
		if (Input.GetMouseButtonDown (0)) {
			mouseStartPos = new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y);
		}
		if (Input.GetMouseButton (0) && !hasStarted && aimCorrect && !dead) {
			AimOn ();
		}else if (Input.GetMouseButtonUp (0) && !hasStarted && aimCorrect && !dead) {
			if (aimY >= 1.5f) {
				lineRenderer.enabled = false;
				if (ballOn) {
					myAudio.Play();
					LoseCollider.instance.ballTouches = 0;
					hasStarted = true;
					StartCoroutine ("DoShoot");
				}else if (fireBallOn) {
					myAudio.Play();
					fireAudio.Play();
					Shoot (fireBall);
					fireballs --;
					hasStarted = true;
					GameManager.instance.hasStarted = true;
					GameManager.instance.specialBallOn = true;
					LoseCollider.instance.ballTouches = 0;
					ballOn = true;
					fireBallOn = false;
				}else if (iceBallOn) {
					myAudio.Play();
					Shoot (iceBall);
					iceballs--;
					hasStarted = true;
					GameManager.instance.hasStarted = true;
					GameManager.instance.specialBallOn = true;
					LoseCollider.instance.ballTouches = 0;
					ballOn = true;
					iceBallOn = false;
				}
			}else if (aimY < 1.7f) {
					lineRenderer.enabled = false;
				}
		}
	}

	IEnumerator DoShoot(){
		int i=0;
		while (i<stockBalls.Count){
			Shoot(stockBalls[i]);
			i++;
			if (i >= stockBalls.Count){
				hasStarted = true;
				GameManager.instance.hasStarted = true;
				i = 0;
				StopCoroutine("DoShoot");
			} 	
			yield return new WaitForSeconds(firerate);
		}
	}

	public void SetBalls(){
		//Debug.Log("Set Balls");
		if (ballOn){
			foreach(GameObject ball in stockBalls){
				ball.transform.position = shootSpot.transform.position;
				ball.transform.parent = this.gameObject.transform;
				}
			fireBall.transform.position = inactiveBallPool.transform.position;
			fireBall.transform.parent = inactiveBallPool.transform;
			fireBall.SetActive(false);
			iceBall.transform.position = inactiveBallPool.transform.position;
			iceBall.transform.parent = inactiveBallPool.transform;
			iceBall.SetActive(false);
		}else if (fireBallOn){
			foreach(GameObject ball in stockBalls){
				ball.transform.position = inactiveBallPool.transform.position;
				ball.transform.parent = inactiveBallPool.gameObject.transform;
				}
			fireBall.transform.position = shootSpot.transform.position;
			fireBall.transform.parent = this.gameObject.transform;
			fireBall.SetActive(true);
			iceBall.transform.position = inactiveBallPool.transform.position;
			iceBall.transform.parent = inactiveBallPool.transform;
			iceBall.SetActive(false);
		}else if (iceBallOn){
			foreach(GameObject ball in stockBalls){
				ball.transform.position = inactiveBallPool.transform.position;
				ball.transform.parent = inactiveBallPool.gameObject.transform;
				}
			fireBall.transform.position = inactiveBallPool.transform.position;
			fireBall.transform.parent = inactiveBallPool.transform;
			fireBall.SetActive(false);
			iceBall.transform.position = shootSpot.transform.position;
			iceBall.transform.parent = this.gameObject.transform;
			iceBall.SetActive(true);
		}
	}

	public void SetExtraBall(){
		stockBalls.Add((GameObject)Instantiate(ballPrefab,transform.position,Quaternion.identity,transform));
		balls ++;
	}

	public int SetFireballs(int extraFireballs){
		fireballs += extraFireballs;
		return fireballs;
	}

	public int SetIceballs(int extraIceballs){
		iceballs += extraIceballs;
		return iceballs;
	}

	public void FireballChoose(){
		if (!fireBallOn){
			ballOn = false;
			iceBallOn = false;
			fireBallOn = true;
			SetFrames();
			if (!hasStarted){
				SetBalls();
			}
		}else if (fireBallOn){
			ballOn = true;
			fireBallOn = false;
			SetFrames();
			if (!hasStarted){
				SetBalls();
			}
		}
	}
	public void IceballChoose(){
		if(!iceBallOn){
			ballOn = false;
			fireBallOn = false;
			iceBallOn = true;
			SetFrames();
			if (!hasStarted){
				SetBalls();
			}
		}else if (iceBallOn){
			ballOn = true;
			iceBallOn = false;
			SetFrames();
			if (!hasStarted){
				SetBalls();
			}
		}
	}

	public void SetFrames(){
		if (fireBallOn){
			UiCanvas.instance.Fire();
		}else if (iceBallOn){
			UiCanvas.instance.Ice();
		}else{
			UiCanvas.instance.CloseFxPanels();
		}
	}

	void CheckForBalls(){
		int ballsToCount = activeBallPool.transform.childCount;
		if (ballsToCount <= 0 && hasStarted){
			hasStarted = false;
			aimWait = false;
		}else if (ballsToCount >= 1){
			aimWait = true;
		}
	}

	void AimOn(){
		if(fireBallOn || iceBallOn){
			ballLayerMask = 1<<11;
		}else if(ballOn){
			ballLayerMask = 1<<10;
		}

		ballLayerMask = ~ballLayerMask;
		lineRenderer.SetPosition(1, new Vector3(basePos.x,11,0));

		if(!aimWait){
			Vector2 mousePos = new Vector2 (Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y );
			Vector2 screenAfairesi = new Vector2 (7f,12.5f);

			RaycastHit2D hit = Physics2D.Raycast (basePos,screenAfairesi -(mousePos * 2),100,ballLayerMask);
			RaycastHit2D hit2 = Physics2D.Raycast (mouseStartPos,mousePos - mouseStartPos ,100,ballLayerMask);
			Debug.DrawLine(basePos,hit.point);
			Debug.DrawLine(mouseStartPos,hit2.point);
			lineRenderer.enabled = true;
			lineRenderer.SetPosition(0,basePos);
			lineRenderer.SetPosition(1,hit.point);
			hitPos = hit.point;
			aimY = hitPos.y;
		if (aimY < 1.5f){
			lineRenderer.enabled = false;
			}
		}
	}

	void Shoot (GameObject exball){
		Vector3 basePos3 = new Vector3 (basePos.x, basePos.y, 0f);
		Vector3 diff =  hitPos - basePos3;								//Camera.main.ScreenToWorldPoint(Input.mousePosition)- transform.position;
		diff.Normalize(); 												// Κανει Normalize το Vector... ολες οι τιμες του εχου αθρισμα 1
		float rotZ = Mathf.Atan2(diff.y,diff.x) * Mathf.Rad2Deg; 		// Briskei tin gonia se moires

		exball.transform.parent = activeBallPool.transform;
		exball.transform.rotation = Quaternion.Euler(new Vector3( 0f, 0f, rotZ + aimOffset));
		Ball ball =exball.GetComponent<Ball>();
		ball.isMooving = true;
			
		Rigidbody2D ballRigy = exball.GetComponent<Rigidbody2D>();
		ballRigy.AddRelativeForce(new Vector2 (0f,forceForShooting),ForceMode2D.Impulse);
		ballRigy.velocity = exball.transform.up * forceForShooting;
		}

	void MousePosAim(){
		float yAxis = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
		if (yAxis >=1.5f && yAxis <= 11f){
			aimCorrect = true;
		}else{
			aimCorrect = false;
			lineRenderer.enabled = false;
		}
	}
	public void DeadOrNot(){
		if(dead){
			dead = false;
		}else if (!dead){
			dead = true;
		}
	}
}
