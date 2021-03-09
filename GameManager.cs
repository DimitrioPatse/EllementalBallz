using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	[Header("Challenge Options... /Skip if not a challenge")]
	[SerializeField] bool isChallenge;
	public int challengeID;
	public int maxRoundsForLevel;
	GameObject ballShooter;
	[SerializeField] GameObject loseFx;

	[Header("Set These Items")]
	[SerializeField] GameObject activeBallPool;
	[SerializeField] GameObject shredder;

	[Space]
	[Header("Time Options")]
	[SerializeField] int fastTimeScale = 3;
	[SerializeField] int timeToShowFastButton = 5;
	float timePastShoot;

	[HideInInspector] public bool hasStarted = false;
	[HideInInspector] public int level = 1;
	[HideInInspector] public bool fastTimer;
	[HideInInspector] public int newBalls;
	[HideInInspector] public bool specialBallOn;

	bool checkForTurn = true;
	bool lastChance = true;
	bool isWinner;
	Vector3 moveDist = new Vector3 (0,-1,0);
	GameObject[] onScreenObj;

	public delegate void NextTurn();
	public static event NextTurn toNextTurn;

	void Awake () {
		if (instance == null){
			instance = this;
		}else if (instance !=this){
			Destroy(gameObject);
		}
		ballShooter = GameObject.FindGameObjectWithTag("Shooter");
		toNextTurn += BoolReset;
		toNextTurn += SetNewBallsToShooter;
		toNextTurn += ResumeTime;
	}

	void Update () {
		CheckForPanel();
		if(checkForTurn){
			CheckForTurn();
		}
		if(isChallenge){
			StartCoroutine("CheckForWin");
			CheckForChallengeDeath();
		}
		if (hasStarted && !fastTimer) {
			timePastShoot += Time.deltaTime;
			if (timePastShoot >= timeToShowFastButton) {
				UiCanvas.instance.FastButton(true);
			}
		}
	}

	void BoolReset(){
		level++;
		fastTimer = false;
		hasStarted = false;
		specialBallOn = false;
		timePastShoot = 0;

	}

	public void LowerScreenObjects(){
		onScreenObj = GameObject.FindGameObjectsWithTag("Moovable");
		foreach (GameObject thing in onScreenObj){
			thing.transform.position += moveDist;
		}
		if (SpawnerGroups.instance != null){
			SpawnerGroups.instance.isTimeToSpawn = true;
		}
	}

	IEnumerator CheckForWin(){
		GameObject[] bricks;
		bricks = GameObject.FindGameObjectsWithTag("Breakable");

		if (bricks.Length == 0){
			isWinner = true;
			yield return new WaitForSeconds(0.2f);
			UiCanvas.instance.Win();
			checkForTurn = false;
			SaveChallengeButtons.instance.SetCompleted(challengeID);
			SaveChallengeButtons.instance.Save();
			StopCoroutine("CheckForWin");
		}
	}

	void CheckForChallengeDeath(){
		if (level > maxRoundsForLevel && !isWinner){
			BallShooter.instance.dead = true;
			loseFx.SetActive(true);
			Invoke("Death",3f);
		}
	}

	public void Death(){
		UiCanvas.instance.Death(true);
		GooglePlay.AddScoreToLeaderboard(EBGPSIds.leaderboard_elemental_ballz, level);
	}

	void SetNewBallsToShooter (){
		int i = 0;
		while (i < newBalls) {
			BallShooter.instance.SetExtraBall ();
			i++;
			}
		newBalls = 0;
	}

	void CheckForTurn(){
		int ballsToShooter = ballShooter.transform.childCount;
		int ballsInList = BallShooter.instance.stockBalls.Count;
		if (ballsToShooter >= ballsInList && hasStarted || ballsToShooter == 1 && hasStarted && specialBallOn ){
			if(toNextTurn != null){
				toNextTurn();
				LoseCollider.instance.SetPositionForShoot();
				BallShooter.instance.SetBalls();
				BallShooter.instance.SetFrames();
				LowerScreenObjects();
				UiManager.instance.MakeUpdates();
				SaveManager.instance.Save();
			}
		}
	}

	public void OneMoreTry(){
		if (!isChallenge){
			if (lastChance){
				LoseCollider.instance.ParticleReset();
				UiCanvas.instance.Death(false);
	 			shredder.SetActive(true);
	 			Destroy(shredder,0.1f);
				lastChance = false;
			}
		}else if(isChallenge){
			if (lastChance){
				UiCanvas.instance.Death(false);
				lastChance = false;
	 		}
	 	}		
	}

	void CheckForPanel(){
		UiPanel panel = GameObject.FindObjectOfType<UiPanel>();

		if (panel == null && !fastTimer){
			ResumeTime();
		}else if(fastTimer || panel.gameObject.tag == "WinPanel"){
			return;
		}
		else if(panel.isActiveAndEnabled){
			Invoke("PauseTime", 0.3f);
		}
	}

	public void FastTime(){
		fastTimer = true;
		Time.timeScale = fastTimeScale;
		UiCanvas.instance.FastButton(false);
	}
	public void PauseTime(){
		Time.timeScale = 0;	
	}

	public void ResumeTime(){
		Time.timeScale = 1;	
		fastTimer = false;
		UiCanvas.instance.FastButton(false);
	}
}
