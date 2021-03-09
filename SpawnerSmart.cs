using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSmart : MonoBehaviour {
	public GameObject extraBallPrefab;
	public float extraBallSpawnPositionX;
	public GameObject[] spawningItems;
	public GameObject[] spawnPositions;


	private int brickChoose;

	// Use this for initialization
	void Start () {
		ExtraBallSpawn();
		ForeachSpawn();
	}
	void ForeachSpawn(){
		foreach (GameObject position in spawnPositions){
			brickChoose = Random.Range(0,spawningItems.Length);
			if (spawningItems[brickChoose]== null){
				break;
			}else{
				GameObject myAttacker = Instantiate (spawningItems[brickChoose])as GameObject;
				myAttacker.transform.SetParent(position.transform, false);
				myAttacker.transform.position = position.transform.position;
				}
		}
	}

	void ExtraBallSpawn(){
		GameObject extraBall = Instantiate(extraBallPrefab) as GameObject;
		extraBall.transform.parent = transform;
		extraBall.transform.position = new Vector3 (extraBallSpawnPositionX,9.5f,0);
	}
}
