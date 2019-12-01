using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameSystemManager : MonoBehaviour {

	public static GameSystemManager system;
	public SaveManager saveManager;
	public PlayerData playerData = ScriptableObject.CreateInstance<PlayerData>();
	public SaveData saveData;

	public EnemyData enemy;
	public GameObject enemyObject;

	void Awake()
	{
		if (system == null) {
			DontDestroyOnLoad (gameObject);
			system = this;
		}
		else if (system != this) {
			Destroy (gameObject);
		}
	}

	public void SaveAllData(PlayerData playerDataRef, DeckData inventoryRef, DeckData playerDeckRef)
	{
		SaveData saveData = new SaveData();

		saveData.player = playerDataRef;
		saveData.inventory = inventoryRef;
		saveData.playerDeck = playerDeckRef;

		string invString = JsonUtility.ToJson (saveData, true);
		string path = Path.Combine (Application.persistentDataPath, "SaveData.json");
		Debug.Log (path);
		Debug.Log (invString);
		File.WriteAllText (path, invString);

	}

	void LoadAllData(PlayerData playerDataRef, DeckData inventoryRef, DeckData playerDeckRef)
	{
		string path = Path.Combine (Application.persistentDataPath, "SaveData.json");
		if (File.Exists (path) == true) {
			string invString = File.ReadAllText (path);
			SaveData saveData = new SaveData();
			JsonUtility.FromJsonOverwrite (invString, saveData);

			playerDataRef = saveData.player;
			playerDeckRef = saveData.playerDeck;
			inventoryRef = saveData.inventory;
		}
	}


}

[System.Serializable]
public class SaveData : ScriptableObject
{
	public PlayerData player;
	public DeckData inventory;
	public DeckData playerDeck;

}
