using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveManager : MonoBehaviour {

	public static SaveManager instance;

	[HideInInspector] public int f1;	//HighScore
	[HideInInspector] public int f2;	//Coins
	[HideInInspector] public int f3; 	//Ball ID
	[HideInInspector] public int f4;	//Background ID
	[HideInInspector] public bool f5;	//He Knows.. "Hides the tutor from start"

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

	public int SetHighScore(int newf1){
		if (newf1 >= f1){
			f1 = newf1;
			}
		return f1;
	}

	public int SetCoins(int newCoins){
	f2 += newCoins;
	return f2;
	}

	public int SetBallID(int newf3){
		f3 = newf3;
		return f3;
	}

	public int SetBackgroundID(int newf4){
		f4 = newf4;
		return f4;
	}

	public void HeKnowsBool(){
		f5 = true;
	}


	public void Save () {
		Debug.Log("Save");
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

		PlayerData data = new PlayerData();
		data.f1 = f1;
		data.f2 = f2;
		data.f3 = f3;
		data.f4 = f4;
		data.f5 = f5;

		bf.Serialize(file,data);
		file.Close();
	}

	public void Load(){
		if (File.Exists(Application.persistentDataPath + "/playerInfo.dat")){
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat",FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize(file);
			file.Close();

			f1 = data.f1;
			f2 = data.f2;
			f3 = data.f3;
			f4 = data.f4;
			f5 = data.f5;
		}
	}
	public void DeleteSaves(){
		File.Delete(Application.persistentDataPath + "/playerInfo.dat");
	}
}

[Serializable]
class PlayerData{
	public int f1;
	public int f2;
	public int f3;
	public int f4;
	public bool f5;
}