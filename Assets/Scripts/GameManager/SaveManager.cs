using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager: MonoBehaviour {

	public DeckData inventory;
	public DeckData activeInventory;
	public DeckData playerDeck;

	// Use this for initialization
	[ContextMenu("Start")]
	void Start () {
		string path = Path.Combine (Application.persistentDataPath, "Inventory");
		activeInventory = ScriptableObject.CreateInstance<DeckData> ();
		if (File.Exists (path) == true) { LoadInventory (); }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	[ContextMenu("Save")]
	void SaveInventory()
	{
		string invString = JsonUtility.ToJson (activeInventory, true);
		string path = Path.Combine (Application.persistentDataPath, "Inventory");
		Debug.Log (path);
		Debug.Log (invString);
		File.WriteAllText (path, invString);

		for (int i = 0; i < activeInventory.cards.Count; i++) {
			Debug.Log (activeInventory.cards [i].name);
		}
	}

	[ContextMenu("Load")]
	void LoadInventory()
	{
		string path = Path.Combine (Application.persistentDataPath, "Inventory");
		if (File.Exists (path) == true) {
			string invString = File.ReadAllText (path);
			JsonUtility.FromJsonOverwrite(invString, activeInventory);

			for (int i = 0; i < activeInventory.cards.Count; i++) {
				Debug.Log (activeInventory.cards [i].name);
			}
		}
	}

	void SavePlayerDeck()
	{
		
	}
}
