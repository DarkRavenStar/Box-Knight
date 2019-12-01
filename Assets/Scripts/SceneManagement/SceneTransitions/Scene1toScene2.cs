using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene1toScene2 : MonoBehaviour
{
    public GameObject player;
    Vector3 startPos = new Vector3(0.0f, -1.42f, 3.2f);

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PlayerModel")
        {
            player.transform.position = startPos;
            SceneManager.LoadScene("Level02Scene");
        }
    }
}
