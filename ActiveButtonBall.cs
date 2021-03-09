using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveButtonBall : MonoBehaviour {


	[SerializeField] GameObject[] buttons;

	[SerializeField] Color32 color;
	[SerializeField] float speedColorChange;
	float startTime;
	Image myImage;


	void Start(){
		myImage = gameObject.GetComponent<Image>();
		startTime = Time.time;
		Check();
	}

	void Update(){
		float t = (Mathf.Sin(Time.time - startTime) * speedColorChange);
		myImage.color = Color.Lerp (color, Color.black,t);
	}

	public void Check(){
		foreach (GameObject button in buttons ){
			NewBallButton i = button.GetComponent<NewBallButton>();
			int id = i.ballIDNumber;
			if (id == SaveManager.instance.f3){
				transform.position = button.transform.position;
			}
		}
	}
}
