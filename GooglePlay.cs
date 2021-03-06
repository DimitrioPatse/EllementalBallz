using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GooglePlay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
		PlayGamesPlatform.InitializeInstance(config);
		PlayGamesPlatform.Activate();

		SignIn();
	}
	
	// Update is called once per frame
	void SignIn() {
		Social.localUser.Authenticate(success => {});

	}

	#region Achievements
	public static void UnlockAchievement(string id){
		Social.ReportProgress(id, 100, success => {});
	}

	public static void IncrementAchievement(string id, int stepsToIcrement){
		PlayGamesPlatform.Instance.IncrementAchievement(id, stepsToIcrement, success => {});
	}

	public static void ShowAchievementsUI(){
		Social.ShowAchievementsUI();
	}
	#endregion /Achievements

	#region Leaderboards
	public static void AddScoreToLeaderboard(string leaderboardId, long score){
		Social.ReportScore(score, leaderboardId, success => {});
	}
	public static void ShowLeaderboards(){
		Social.ShowLeaderboardUI();
	}
	#endregion /Leaderboards
}
