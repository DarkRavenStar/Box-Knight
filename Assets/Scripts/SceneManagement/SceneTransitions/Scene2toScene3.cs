using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene2toScene3 : MonoBehaviour
{
    public GameObject player;
    Vector3 startPos = new Vector3(-0.2f, -4.92f, 1.02f);

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PlayerModel")
        {
            player.transform.position = startPos;
            SceneManager.LoadScene("Level03Scene");
        }
    }
}
