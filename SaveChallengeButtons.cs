using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveChallengeButtons : MonoBehaviour {

	public static SaveChallengeButtons instance;
	[HideInInspector]
	public bool[] bools = new bool[500];
	[HideInInspector]
	public bool[] star_Two = new bool[500];
	[HideInInspector]
	public bool[] star_Three = new bool[500];



	// Use this for initialization
	void Awake () {
		if (instance == null){
			instance = this;
			DontDestroyOnLoad(gameObject);
		}else if (instance !=this){
			Destroy(gameObject);
		}
		Load();
	}
	public void SetCompleted(int i){
		if (bools[i] == false){
			bools[i] = true;
		}
	}
	public void SetStarTwo(int i){
		if (star_Two[i] == false){
			star_Two[i] = true;
		}
	}

	public void SetStarThree(int i){
		if (star_Three[i] == false){
			star_Three[i] = true;
		}
	}

	public void Save () {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/challengeInfo.dat");

		AvailableChallengeButtons data = new AvailableChallengeButtons();
		data.bools = bools;
		data.star_Two = star_Two;
		data.star_Three = star_Three;
		bf.Serialize(file,data);
		file.Close();
	}

	public void Load(){
		if (File.Exists(Application.persistentDataPath + "/challengeInfo.dat")){
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/challengeInfo.dat",FileMode.Open);
			AvailableChallengeButtons data = (AvailableChallengeButtons)bf.Deserialize(file);
			file.Close();
			bools = data.bools;
			star_Two = data.star_Two;
			star_Three = data.star_Three;

		}
	}

	public void DeleteSaves(){
		File.Delete(Application.persistentDataPath + "/challengeInfo.dat");
	}
}
[Serializable]
class AvailableChallengeButtons{
	public bool[] bools;
	public bool[] star_Two;
	public bool[] star_Three;
}