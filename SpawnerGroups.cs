using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerGroups: MonoBehaviour {

	public static SpawnerGroups instance;

	public GameObject[] groupPrefabArray;

	public bool isTimeToSpawn = true;
	private int spawnposChoose;

	
	// Use this for initialization
	void Start () {
		if (instance == null){
			instance = this;
		}else if (instance !=this){
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
			if(isTimeToSpawn){
				Spawn ();
			}
		}

	 void Spawn (){
		spawnposChoose = Random.Range(0,groupPrefabArray.Length);

		GameObject myAttacker = Instantiate (groupPrefabArray[spawnposChoose])as GameObject;
		myAttacker.transform.parent = transform;
		myAttacker.transform.position = new Vector3 (0,0,0);
		isTimeToSpawn = false;
		}
	}
	

