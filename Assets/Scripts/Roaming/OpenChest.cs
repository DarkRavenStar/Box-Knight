using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenChest : MonoBehaviour
{
    public Mesh closedChest;
    public Mesh openChest;
    public int myId;
    string chestID;

    void Update()
    {
        chestID = SceneManager.GetActiveScene().name + "/" + myId;
        if (PlayerPrefs.GetInt(chestID) != 0)
        {
            GetComponent<MeshFilter>().sharedMesh = openChest;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Mesh current = GetComponent<MeshFilter>().sharedMesh;

        if (other.gameObject.name == "PlayerModel")
        { 
            if(PlayerPrefs.GetInt(chestID) == 0)
            {
                PlayerPrefs.SetInt(chestID, 1);
            }
        }
    }
}
