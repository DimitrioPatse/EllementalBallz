using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour {

	public static UiManager instance = null;

	[SerializeField] Text hspointsText;
	[SerializeField] Text levelText;
	[SerializeField] Text maxRoundsText;
	[SerializeField] Text coinsText;
	[SerializeField] Text ballText;
	private int coins;


	// Use this for initialization
	void Start () {
		if (instance == null){
			instance = this;
		}else if (instance !=this){
			Destroy(gameObject);
			}
		//GameManager.toNextTurn += MakeUpdates;
		MakeUpdates();
		}

	public void MakeUpdates(){
		//Debug.Log("Make Updates");
		UpdateCoinsText();
		UpdateBallsText();
		UpdateLevelText();
		UpdateMaxRoundsText();
		SetHighScore();
		UpdateHighScore();
	}
	
	void UpdateHighScore() {
		if (hspointsText == null){
			return;
		}else{
			hspointsText.text = SaveManager.instance.f1.ToString();
		}
	}

	void SetHighScore(){
		if (GameManager.instance == null){
			return;
		}else{
			int levelnow = GameManager.instance.level - 1;
			SaveManager.instance.SetHighScore(levelnow);
			}
	}
	void UpdateLevelText(){
		if (levelText == null){
			Debug.Log("null level text");
			return;
		}else{
			levelText.text = GameManager.instance.level.ToString();
		}
	}
	void UpdateMaxRoundsText(){
		if (maxRoundsText == null){
			return;
		}else{
			maxRoundsText.text = GameManager.instance.maxRoundsForLevel.ToString();
		}
	}

	public void UpdateCoinsText (){
		if (coinsText == null){
			return;
		}else{
			coinsText.text = SaveManager.instance.f2.ToString ();
		}
	}

	void UpdateBallsText(){
		if (ballText == null){
			return;
		}else{
		int i = BallShooter.instance.balls;
		ballText.text = i.ToString();
		}
	}
}
