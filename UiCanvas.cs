using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiCanvas : MonoBehaviour {

	public static UiCanvas instance = null;


	[SerializeField] GameObject winPanel, infoPanel, pausePanel, deathPanel,settingsPanel, failAdPanel, skipAdPanel, fireballPanel, iceballPanel, fireFrame, iceFrame;
	[SerializeField] Button fastButton;


	void Awake () {
		if (instance == null){
			instance = this;
		}else if (instance !=this){
			Destroy(gameObject);
		}
	}


	public void Win(){
		winPanel.SetActive(true);
	}
	public void Info(GameObject panel){
		infoPanel.SetActive(true);
		panel.SetActive(false);
	}
	public void Pause(){
		pausePanel.SetActive(true);
		GameManager.instance.PauseTime();
	}
	public void Death(bool dead){
		if(dead)
			deathPanel.SetActive(true);
		else
			deathPanel.SetActive(false);
	}
	public void Settings(GameObject panel){
		settingsPanel.SetActive(true);
		GameManager.instance.PauseTime();
		panel.SetActive(false);
	}
	public void Fire(){
		fireballPanel.SetActive(true);
		iceballPanel.SetActive(false);
		fireFrame.SetActive(true);
		iceFrame.SetActive(false);
	}
	public void Ice(){
		iceballPanel.SetActive(true);
		fireballPanel.SetActive(false);
		fireFrame.SetActive(false);
		iceFrame.SetActive(true);
	}
	public void CloseFxPanels(){
		fireballPanel.SetActive(false);
		iceballPanel.SetActive(false);
		fireFrame.SetActive(false);
		iceFrame.SetActive(false);
	}
	public void FastButton(bool fast){
		if(fastButton == null){
			return;
		}else{
			if (fast)
				fastButton.interactable = true;
			else 
				fastButton.interactable = false;
			}
	}
	public void FastTime(){
		GameManager.instance.FastTime();
	}
	public void ChooseFire(){
		BallShooter.instance.FireballChoose();
	}
	public void ChooseIce(){
		BallShooter.instance.IceballChoose();
	}
	public void SkipAd(){
		skipAdPanel.SetActive(true);
	}
	public void FailAd(){
		failAdPanel.SetActive(true);
	}
	public void Levels(){
		LevelManager.instance.LoadLevel("03_Ch_00");
	}

	public void Restart(){
		LevelManager.instance.RestartLevel();
	}

	public void NextLevel(){
		LevelManager.instance.LoadNextLevel();
	}
	public void Achievements(){
		GooglePlay.ShowAchievementsUI();
	}
	public void Leaderboard(){
		GooglePlay.ShowLeaderboards();
	}
	public void Help(GameObject panel){
		GameManager.instance.PauseTime();
		infoPanel.SetActive(true);
		panel.SetActive(false);
	}
	public void Quit(){
		LevelManager.instance.QuitRequest();
	}
	public void Resume(GameObject panel){
		GameManager.instance.ResumeTime();
		panel.SetActive(false);
	}
	public void Home(){
		LevelManager.instance.LoadLevel("01_Start");
	}
}
