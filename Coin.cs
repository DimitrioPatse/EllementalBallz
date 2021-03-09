using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

	[SerializeField] int value;
	[SerializeField] AudioClip coinClip;

	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.up * Time.deltaTime * 100);
		transform.Translate(Vector2.down * Time.deltaTime, Space.World);
	}

	void OnTriggerEnter2D (Collider2D collision){
		if (collision.gameObject.tag == "Ball" || collision.gameObject.tag == "Fireball"){
		SaveManager.instance.SetCoins(value);
		UiManager.instance.UpdateCoinsText();
		AudioSource.PlayClipAtPoint(coinClip,transform.position);
		Destroy(gameObject);
		}
	}
}
