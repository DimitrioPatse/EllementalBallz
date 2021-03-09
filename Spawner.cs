using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject[] objectPrefabArray;
	public GameObject[] spawnGivenPos;

	int spawnposChoose;
	bool hasStarted;
	BallShooter shooter;
	
	// Use this for initialization
	void Start () {
		shooter = BallShooter.instance;
	}
	
	// Update is called once per frame
	void Update () {
		hasStarted = shooter.hasStarted;
		if (hasStarted){
			spawnposChoose = Random.Range(0,spawnGivenPos.Length);
			foreach (GameObject thisItem in objectPrefabArray){
				if(isTimeToSpawn(thisItem)){
					Spawn (thisItem);
				}
			}
		}
	}

	void Spawn (GameObject myGameObject){
		
		GameObject myAttacker = Instantiate (myGameObject)as GameObject;
		myAttacker.transform.parent = transform;
		myAttacker.transform.position = spawnGivenPos[spawnposChoose].transform.position;
		}
	
	
	bool isTimeToSpawn (GameObject itemGameObject){
		ItemsForSpawn items = itemGameObject.GetComponent<ItemsForSpawn>();
		
		float meanSpawnDelay = items.seenEverySeconds;
		float spawnsPerSecond = 1 / meanSpawnDelay;
		
		if (Time.deltaTime > meanSpawnDelay){
			Debug.LogWarning ("Spawn rate capped by frame rate");
		}
		float threshold = spawnsPerSecond * Time.deltaTime;
		
		return (Random.value < threshold);
	}
}
