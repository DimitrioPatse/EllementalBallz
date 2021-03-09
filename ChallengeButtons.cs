using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeButtons : MonoBehaviour {

	private Button myButton;

	public int challengeIDNumber;
	public Sprite unlockedLvlSprite,lockedLvlSprite,completedLvlSprite;
	public Image star1;
	public Image star2;
	public Image star3;

	private bool available;
	private bool iscompleted;
	private bool star2bool;
	private bool star3bool;


	void Start () {
		myButton = GetComponent<Button>();
		}
	void Update(){
		available = SaveChallengeButtons.instance.bools[challengeIDNumber - 1];
		iscompleted = SaveChallengeButtons.instance.bools[challengeIDNumber];
		star2bool = SaveChallengeButtons.instance.star_Two[challengeIDNumber];
		star3bool = SaveChallengeButtons.instance.star_Three[challengeIDNumber];

		IsAvailable();

	}

	void IsAvailable(){
		Image spr = GetComponent<Image>();
		if (iscompleted){
			spr.sprite =completedLvlSprite;
			myButton.interactable = true;
			star1.gameObject.SetActive(true);
			if (star2bool){
				star2.gameObject.SetActive(true);
			}if (star3bool){
				star3.gameObject.SetActive(true);
			}
		}else if (available  && !iscompleted || challengeIDNumber == 1 && !iscompleted && !available){
			spr.sprite = unlockedLvlSprite;
			myButton.interactable = true;
		}else if (!available){
			myButton.interactable = false;
			spr.sprite = lockedLvlSprite;
		}
	}
}
