using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour {



	public void ShowAd(){
		print("AdRequested");
		if(Advertisement.IsReady()){
			Advertisement.Show("rewardedVideo", new ShowOptions(){resultCallback = HandleAdResult});
		}
	}

	public void ShowAdCoins(){
		print("AdRequested");
		if(Advertisement.IsReady()){
			Advertisement.Show("rewardedVideo", new ShowOptions(){resultCallback = HandleAdResultCoins});
		}
	}

	private void HandleAdResult(ShowResult result){
		switch (result){
			case ShowResult.Finished:
				GameManager.instance.OneMoreTry();
				break;
			case ShowResult.Skipped:
				UiCanvas.instance.SkipAd();
				break;
			case ShowResult.Failed:
				UiCanvas.instance.FailAd();
				break;
		}
	}

	private void HandleAdResultCoins(ShowResult result){
		switch (result){
			case ShowResult.Finished:
				SaveManager.instance.SetCoins(20);
				SaveManager.instance.Save();
				UiManager.instance.UpdateCoinsText();
				break;
			case ShowResult.Skipped:
				UiCanvas.instance.SkipAd();
				break;
			case ShowResult.Failed:
				UiCanvas.instance.FailAd();
				break;
		}
	}
}
