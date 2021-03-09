using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveButtonsState : MonoBehaviour {

	public static SaveButtonsState instance;
	[HideInInspector]
	public bool[] bools = new bool[500];


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
	public void SetPurchase(int i){
		 if (bools[i] == false){
			bools[i] = true;
		}
	}

	public void Save () {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/buttonInfo.dat");

		BallButtonsState data = new BallButtonsState();
		data.bools = bools;
		bf.Serialize(file,data);
		file.Close();
	}

	public void Load(){
		if (File.Exists(Application.persistentDataPath + "/buttonInfo.dat")){
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/buttonInfo.dat",FileMode.Open);
			BallButtonsState data = (BallButtonsState)bf.Deserialize(file);
			file.Close();
			bools =data.bools;

		}
	}

	public void DeleteSaves(){
		File.Delete(Application.persistentDataPath + "/buttonInfo.dat");
	}
}
[Serializable]
class BallButtonsState{
	public bool[] bools;

}