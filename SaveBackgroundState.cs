using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveBackgroundState : MonoBehaviour {

	public static SaveBackgroundState instance;
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
		FileStream file = File.Create(Application.persistentDataPath + "/buttonBackgroundInfo.dat");

		BackgroundButtonsState data = new BackgroundButtonsState();
		data.bools = bools;
		bf.Serialize(file,data);
		file.Close();
	}

	public void Load(){
		if (File.Exists(Application.persistentDataPath + "/buttonBackgroundInfo.dat")){
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/buttonBackgroundInfo.dat",FileMode.Open);
			BackgroundButtonsState data = (BackgroundButtonsState)bf.Deserialize(file);
			file.Close();
			bools =data.bools;

		}
	}
	public void DeleteSaves(){
		File.Delete(Application.persistentDataPath + "/buttonBackgroundInfo.dat");
	}
}
[Serializable]
class BackgroundButtonsState{
	public bool[] bools;

}