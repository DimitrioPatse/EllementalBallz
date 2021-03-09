using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarContoller : MonoBehaviour {

	[SerializeField] AudioClip oneStarClip, twoStarClip, threeStarClip;
	[SerializeField] Sprite starFullSprite;
	[SerializeField] GameObject starOne, starTwo, starThree, starParticle;

	float animClipDelay = 1;
	public float roundsDone;
	public float maxRounds;
	public float percent;
	AudioSource myAudio;
	int lvlId;

	// Use this for initialization
	void OnEnable() {
		myAudio = GetComponent<AudioSource>();
		myAudio.volume = 0.3f;
		roundsDone = GameManager.instance.level;
		maxRounds = GameManager.instance.maxRoundsForLevel;
		Time.timeScale = 1;
		lvlId = GameManager.instance.challengeID;
		myAudio.volume = 0.2f;
		percent = roundsDone / maxRounds;
		if (percent <= 0.69f){
			SaveChallengeButtons.instance.SetStarTwo(lvlId);
			if (percent <= 0.4f){
				SaveChallengeButtons.instance.SetStarThree(lvlId);
			}
		}
		Invoke("SetStarOne", animClipDelay);
	}

	void SetStarOne(){
		SpriteRenderer sprOne = starOne.GetComponent<SpriteRenderer>();
		myAudio.Stop();
		myAudio.clip = oneStarClip;
		myAudio.Play();
		sprOne.sprite = starFullSprite;
		GameObject smokePuff1 = Instantiate (starParticle,sprOne.transform.position,Quaternion.identity)as GameObject;
		Destroy(smokePuff1,1f);
		Invoke("SetStarTwo",animClipDelay);
	}

	void SetStarTwo(){
		SpriteRenderer sprTwo = starTwo.GetComponent<SpriteRenderer>();
		myAudio.Stop();
		if (percent <= 0.69f){
			myAudio.clip = twoStarClip;
			myAudio.Play();
			sprTwo.sprite = starFullSprite;
			GameObject smokePuff2 = Instantiate (starParticle,sprTwo.transform.position,Quaternion.identity)as GameObject;
			Destroy(smokePuff2,1f);
			Invoke("SetStarThree", animClipDelay);
		}
	}
	void SetStarThree(){
		SpriteRenderer sprThree = starThree.GetComponent<SpriteRenderer>();
		myAudio.Stop();
		if (percent <= 0.4f){
			myAudio.clip = threeStarClip;
			myAudio.Play();
			sprThree.sprite = starFullSprite;
			GameObject smokePuff3 = Instantiate (starParticle,sprThree.transform.position,Quaternion.identity)as GameObject;
			Destroy(smokePuff3,1f);
		}
	}
}
