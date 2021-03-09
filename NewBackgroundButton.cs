using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBackgroundButton : MonoBehaviour {

	private Button myButton;
	private int coinsIHave;
	public int backgroundIDNumber;
	public Sprite mySprite;
	private bool purchased;
	public int backgroundValue;
	private Text myText;

	void Start () {
		myButton = GetComponent<Button>();
		myText = myButton.GetComponentInChildren<Text>();
		if (myText == null){
			if (backgroundIDNumber == 0){
				return;
			}
			Debug.LogWarning("Can find MyText: Background Button" + backgroundIDNumber);
			return;
		}else{
			myText.text = backgroundValue.ToString();
		}
	}

	void Update () {
		if (purchased){
		Destroy(myText);
		}
		purchased = SaveBackgroundState.instance.bools[backgroundIDNumber];
		coinsIHave = SaveManager.instance.f2;
		IsPurchased();
		IsInteractable();

	}
	public void Purchase(){
		if (!purchased){
			SaveManager.instance.SetCoins(-backgroundValue);
			SaveBackgroundState.instance.SetPurchase(backgroundIDNumber);
			SaveBackgroundState.instance.Save();
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
		}else if (coinsIHave <= backgroundValue - 1 && !purchased){
			myButton.interactable = false;
		}else if(coinsIHave >= backgroundValue){
			myButton.interactable = true;
		}
	}

	public void SetButtonBackgroundID(){
		SaveManager.instance.SetBackgroundID(backgroundIDNumber);
		SaveManager.instance.Save();
		SaveBackgroundState.instance.SetPurchase(backgroundIDNumber);
		SaveBackgroundState.instance.Save();
	}
}
