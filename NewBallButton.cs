using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBallButton : MonoBehaviour {

	private Button myButton;
	private int coinsIHave;
	public int ballIDNumber;
	public Sprite mySprite;
	private bool purchased;
	public int ballValue;
	private Text myText;
	//public GameObject activeShit;

	void Start () {
		myButton = GetComponent<Button>();
		myText = myButton.GetComponentInChildren<Text>();
	//	if (ballIDNumber == SaveManager.instance.f3){
	//		activeShit.SetActive(true);
	//	}
		if (myText == null){
			if (ballIDNumber == 0){
				return;
			}
			Debug.LogWarning("Can find MyText: Ball Button" + ballIDNumber);
			return;
		}
		myText.text = ballValue.ToString();

	}
	
	// Update is called once per frame
	void Update () {
		if (purchased){
		Destroy(myText);
		}
		purchased = SaveButtonsState.instance.bools[ballIDNumber];
		coinsIHave = SaveManager.instance.f2;
		IsPurchased();
		IsInteractable();
	}
	public void Purchase(){
		if (!purchased){
			SaveManager.instance.SetCoins(-ballValue);
			SaveButtonsState.instance.SetPurchase(ballIDNumber);
			SaveButtonsState.instance.Save();

		}
	}
	void IsPurchased(){
		if (purchased){
			Image spr = GetComponent<Image>();
			spr.sprite = mySprite;
		}
	}
	void IsInteractable(){
		if (purchased){
			myButton.interactable = true;
		}
		if (coinsIHave <= ballValue - 1 && !purchased){
			myButton.interactable = false;
		}
		if(coinsIHave >= ballValue){
			myButton.interactable = true;
		}
	}

	public void SetButtonBallID(){
		SaveManager.instance.SetBallID(ballIDNumber);
		SaveManager.instance.Save();
		SaveButtonsState.instance.SetPurchase(ballIDNumber);
		SaveButtonsState.instance.Save();
	}
}
